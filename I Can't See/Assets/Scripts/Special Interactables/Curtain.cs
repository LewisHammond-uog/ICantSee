using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : Interactable
{
    [SerializeField]
    private AudioSource interactableAudioSource;

    [SerializeField]
    private Cloth cloth;

    [SerializeField]
    private float maxDist;

    private void Start()
    {
        
    }

    public override void DoAction(VRHand hand)
    {
        //cloth.vertices;
    }
}
