using System.Collections;
using System.Collections.Generic;
using Lewis.MathUtils;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TapController : SoundInteractable
{
    //Minimum move amount before the tap counts
    //as on
    private float minMoveAmount;

    //The Seperate Tap Components (the physical thing that you twist) 
    //that have the twist interactable script attached
    private TwistInteractable[] tapComponents;

    //If tap is on or not
    private bool tapOn = false;

    private void Start()
    {
        //Get all of the twist interactables in the child objects
        tapComponents = GetComponentsInChildren<TwistInteractable>();
    }

    public override void DoAction(VRHand hand)
    {
        //Do Nothing
    }

    /// <summary>
    /// Checks if we should toggle the tap state on/off
    /// based on the rotation of the tap components
    /// </summary>
    void ToggleTapState()
    {
        //Loop through all of the tap components in this object
        foreach (TwistInteractable tapTwister in tapComponents)
        {
            //Check if tap has been moved enough to count as on
            Vector3 absMoveVector = MathUtils.Abs(tapTwister.rotatedAmount);
            if (absMoveVector.magnitude > minMoveAmount)
            {
                //Register Tap on and start sound
                tapOn = true;
                interactableAudioSource.Play();

                //Register Job
                JobManager.RegisterJobAction(jobInfo);
                break;
            }

            //No taps have moved enough - tap off
            //stop playing audio
            tapOn = false;
            interactableAudioSource.Stop();
        }
    }

    #region Event Subs/Unsubs
    private void OnEnable()
    {
        TwistInteractable.TwistMoved += ToggleTapState;
    }
    private void OnDisable()
    {
        TwistInteractable.TwistMoved -= ToggleTapState;
    }
    #endregion
}

//Lewis Hammond
