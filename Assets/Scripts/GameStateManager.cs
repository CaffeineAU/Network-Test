using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameStateManager : NetworkManager {


    private List<PlayerManager> Players = new List<PlayerManager>();

    public static BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.

    public int turn = 0;

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        boardScript.SetupScene();

        Debug.Log("got board manager object: " + boardScript.name);

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.T)) // change turn
        {
            ChangeTurn();

        }
    }

    
    private void ChangeTurn()
    {
        if (Players.Count > 0) // only on the server
        {
            turn++;
            turn = turn % Players.Count;
            foreach (PlayerManager player in Players)
            {
                player.isMyTurn = false;
            }
                Debug.Log("It's " + Players[turn].name + "'s turn");
            Players[turn].isMyTurn = true;

        }
    }

    // called when a new player is added for a client
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, RandomTradeSquareCentre(), playerPrefab.transform.rotation);
        Players.Add(player.GetComponent<PlayerManager>());
        Players[Players.Count - 1].name = Players.Count.ToString();
        Players[Players.Count - 1].myLocation = player.transform.position;
        Players[Players.Count - 1].myRotation = playerPrefab.transform.rotation; // manually sync rotation :(
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        foreach (var player in conn.playerControllers)
        {
            Debug.Log("removing player " + player.gameObject.GetComponent<PlayerManager>().name);
            Players.Remove(player.gameObject.GetComponent<PlayerManager>());
        }

        base.OnServerDisconnect(conn);
    }


    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-2, 2), 0.5f, Random.Range(-2, 2));
    }

    public static Vector3 RandomTradeSquareCentre()
    {
        return boardScript.TradeSquares[Random.Range(0, boardScript.TradeSquares.Count)].Centre;
    }
}
