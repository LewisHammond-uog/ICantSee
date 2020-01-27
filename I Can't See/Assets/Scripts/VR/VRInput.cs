using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Class stores all of the possible input actions that we could want in VR
/// (i.e Grab, Pinch, Start Telport)
/// </summary>
public class VRInput : MonoBehaviour
{

    [SerializeField]
    private SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean GrabAction { get { return grabAction; } }

    [SerializeField]
    private SteamVR_Action_Boolean moveAction;
    public SteamVR_Action_Boolean MoveAction { get { return moveAction; } }


    // generic action to be used when a special interation is available
    [SerializeField]
    private SteamVR_Action_Boolean specialDpadAction;

    public SteamVR_Action_Boolean SpecialDpadAction { get { return specialDpadAction; } }

    //Set to not destory on load
    private void Start()
    {
        //Don't Destory on Load
        DontDestroyOnLoad(this);
    }

}

//Lewis Hammond