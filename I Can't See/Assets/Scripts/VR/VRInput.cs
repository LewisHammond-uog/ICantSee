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

    [SerializeField]
    private SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean GrabAction { get { return grabAction; } }


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
