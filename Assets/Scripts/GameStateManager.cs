using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameStateManager : NetworkManager {


    private List<PlayerManager> Players = new List<PlayerManager>();


    public int turn = 0;

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
        var player = (GameObject)GameObject.Instantiate(playerPrefab, RandomPosition(), Quaternion.identity);
        Players.Add(player.GetComponent<PlayerManager>());
        Players[Players.Count-1].name = Players.Count.ToString();
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
    }

}
