using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : Interactable
{
    [SerializeField]
    private float toastTime = 5.0f;

    [SerializeField]
    private Vector3 toastInsertOffest;

    [SerializeField]
    private AudioSource toasterAudioSource;

    [SerializeField]
    private AudioClip toastReadyAudio;

    private Toast toast;

    private enum ToasterStates
    {
        IDLE,
        READY,
        TOASTING,
        FINISHED
    }

    private ToasterStates toasterState;

    private void Start()
    {
        toasterState = ToasterStates.IDLE;
        toasterAudioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (toasterState == ToasterStates.TOASTING)
        {
            if (toastTime <= 0.0f)
            {
                toasterState = ToasterStates.FINISHED;
                toasterAudioSource.PlayOneShot(toastReadyAudio);
                toastTime = 5.0f;

                //Register Job
                JobManager.RegisterJobAction(jobInfo);

            }
            else
            {
                toastTime -= Time.deltaTime;
            }
        }
    }

    public override void DoAction(VRHand hand)
    {
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction) && toasterState == ToasterStates.READY)
        {
            toasterState = ToasterStates.TOASTING;
            toast.PlaceInToaster(this.transform.position + toastInsertOffest, this.transform.rotation);
        }

        if(hand.GetActionState(hand.VRInputController.SpecialDpadAction) && toasterState == ToasterStates.FINISHED)
        {
            // eject toast
            toasterState = ToasterStates.TOASTING;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Toast>() != null && toasterState == ToasterStates.IDLE)
        {
            toasterState = ToasterStates.READY;
            toast = other.gameObject.GetComponent<Toast>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Toast>() != null)
        {
            toasterState = ToasterStates.IDLE;
            toast = null;
        }
    }
}

// connor done
