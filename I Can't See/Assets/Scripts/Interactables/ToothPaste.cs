using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPaste : Holdable
{
    [SerializeField]
    private AudioSource interactableAudioSource;
    [SerializeField]
    private Transform pasteSpawn;
    [SerializeField]
    private GameObject tpPaste;
    private Vector3 squirtLocation;

    //Time between blobs being created in seconds
    const float timeBetweenBlobs = 0.2f;
    //Time since the last blob was released
    float timeSinceBlob;

    public bool isDPadPressed = false;

    public override void DoAction(VRHand hand)
    {
        base.DoAction(hand);
        VRInput vr = hand.VRInputController;

        isDPadPressed = hand.GetActionState(vr.SpecialDpadAction);

        if(isDPadPressed)
        {
            //Check if audio source is not null
            if (interactableAudioSource != null)
            {
                if (!interactableAudioSource.isPlaying)
                {
                    //Play Interactable moving sound
                    interactableAudioSource.Play();
                }
            }

            //Increase time since blob
            timeSinceBlob += Time.deltaTime;

            //Make paste come out, if on
            if (timeSinceBlob > timeBetweenBlobs)
            {
                //Null Check Water Prefab
                if (!tpPaste) { return; }

                //Create paste object at creation point
                squirtLocation = new Vector3(pasteSpawn.position.x, pasteSpawn.position.y, pasteSpawn.position.z);
                Instantiate(tpPaste, squirtLocation, Quaternion.identity);

                //Register Job Action
                JobManager.RegisterJobAction(jobInfo);

                //Reset time since blob
                timeSinceBlob = 0.0f;
            }

            

        }
    }
}


//Rhys Wareham