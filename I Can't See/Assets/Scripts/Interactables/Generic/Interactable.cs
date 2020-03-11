using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectGenerator))]
public abstract class Interactable : MonoBehaviour
{
    public abstract void DoAction(VRHand hand);

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    public JobActionInfo jobInfo;

}

//Lewis Hammond