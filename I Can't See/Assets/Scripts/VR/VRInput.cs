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

    private SteamVR_Behaviour_Pose pose = null;
    public SteamVR_Behaviour_Pose CurrentPose { get { return pose; } }

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

        //Get VR Pose
        pose = GetComponent<SteamVR_Behaviour_Pose>();

    }

    /// <summary>
    /// Gets the state of a steam VR action
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool GetActionState(SteamVR_Action_Boolean action)
    {
        return action.GetState(pose.inputSource);
    }
}
