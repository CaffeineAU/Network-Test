using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {


    [SyncVar(hook = "OnSyncTurnHook")]
    public bool isMyTurn;


    void OnSyncTurnHook(bool value)
    {
        if (isMyTurn != value)
        {
            Debug.Log("I'm " + name + " and it's " + (value ? "my turn" : "not my turn"));

        }
            isMyTurn = value;
            GetComponentInParent<MeshRenderer>().material = (isMyTurn ? highlightMaterial : normalMaterial);
    }

    public Material highlightMaterial;
    public Material normalMaterial;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
