using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectGenerator))]
public abstract class Interactable : MonoBehaviour
{
    public abstract void DoAction(VRHand hand);

}

//Lewis Hammond