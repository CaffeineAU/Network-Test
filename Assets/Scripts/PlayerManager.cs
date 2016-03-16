using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{


    [SyncVar(hook = "OnSyncTurnHook")]
    public bool isMyTurn;

    [SyncVar(hook = "OnSyncmyLocationHook")]
    public Vector3 myLocation;

    [SyncVar]
    public Quaternion myRotation = Quaternion.identity;

    public float lerpTime = 5f;
    float currentLerpTime;

    public bool smooth = true;

    void OnSyncTurnHook(bool value)
    {
        if (isMyTurn != value)
        {
            Debug.Log("I'm " + name + " and it's " + (value ? "my turn" : "not my turn"));

        }
        isMyTurn = value;
        GetComponentInParent<MeshRenderer>().material = (isMyTurn ? highlightMaterial : normalMaterial);
    }

    void OnSyncmyLocationHook(Vector3 value)
    {
        if (myLocation != value)
        {
            Debug.Log("I'm " + name + " and i'm moving to " + value );
            currentLerpTime = 0;
            myLocation = value;
        }

    }

    public Material highlightMaterial;
    public Material normalMaterial;

    // Use this for initialization
    void Start()
    {

    }

    int i = 0;
    // Update is called once per frame
    void Update()
    {
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float t = currentLerpTime / lerpTime;
        if(smooth) t = t * t * (3f - 2f * t); // smoothstep

        if (GetComponentInParent<Transform>().rotation != myRotation)
        {
            GetComponentInParent<Transform>().rotation = myRotation;
        }

        if (GetComponentInParent<Transform>().position != myLocation)
        {
            GetComponentInParent<Transform>().position =  Vector3.Lerp(GetComponentInParent<Transform>().position, myLocation, t);
        }

        if (Input.GetKeyUp(KeyCode.M) && isLocalPlayer) // move
        {
            CmdMove(GameStateManager.RandomTradeSquareCentre());
        }
    }



    [Command]
    public void CmdMove(Vector3 location)
    {
        myLocation = location;
    }
}
