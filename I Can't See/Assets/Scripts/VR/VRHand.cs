using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Class for handiling player Hands in VR
/// Created by: Lewis
/// </summary>
public class VRHand : MonoBehaviour
{

    //VR Input abstracts away any VR Input management just get the actions by calling
    //vrInputController.GrabAction
    [SerializeField]
    private VRInput vrInputController;
    public VRInput VRInputController { get { return vrInputController; } }

    //Info about objects we are holding
    private Holdable heldObject = null;
    public Holdable HeldObject { get { return heldObject;  } }

    // Update is called once per frame
    void Update()
    {
        
    }
}
