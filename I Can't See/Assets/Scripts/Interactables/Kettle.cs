using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle : PourableHoldable
{
    /// States that the kettle can be in
    private enum KettleState
    {
        EMPTY,
        FILLED,
        FILLED_BOILING,
        FILLED_BOILED
    }
    private KettleState currentKettleState;

    //Amount of water blobs required to be filled
    private int requiredBlobsForFilled = 10;

    //Time it takes for the kettle to boil
    private const float timeToBoil = 5.0f;
    //Timer from when the kettle boil
    private float timeSinceBoil;

    private void Start()
    {
        //Call Base Start on pourable object
        base.Start();

        //Set Kettle State to the empty
        currentKettleState = KettleState.EMPTY;

        //Init time since boil to 0 as boil will not yet be
        //initiated
        timeSinceBoil = 0.0f;

    }

    private new void Update()
    {
        //NEED TO ADD AUDIO

        //Call Base Update on the pourable object
        base.Update();

        switch (currentKettleState)
        {
            case KettleState.FILLED_BOILING:
                {
                    //Increment boil timer
                    timeSinceBoil += Time.deltaTime;

                    //Check if we should be boiled, if so set it
                    //the kettle as boiled
                    if (timeSinceBoil >= timeToBoil)
                    {
                        currentKettleState = KettleState.FILLED_BOILED;

                        //Reset the time since boil
                        timeSinceBoil = 0f;

                        //Call Job Manager to call that kettle has boiled
                        //Change Job Info to bolied
                        jobInfo.action = Job.JOB_ACTIONS.BOILED;
                        JobManager.RegisterJobAction(jobInfo);
                    }
                    break;
                }
            case KettleState.EMPTY:
                {
                    //Check if we are ove the required number of 
                    //water blobs to the filled
                    if(waterDropCount >= requiredBlobsForFilled)
                    {
                        currentKettleState = KettleState.FILLED;

                        //Call Job Manager to call that kettle has filled
                        //Change Job Info to bolied
                        jobInfo.action = Job.JOB_ACTIONS.FILLED;
                        JobManager.RegisterJobAction(jobInfo);
                    }
                    break;
                }
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentKettleState++;
        }

        //Reset back to empty from whatever state that we were in if 
        //we run out of liquid
        if(waterDropCount <= 0 && currentKettleState != KettleState.EMPTY)
        {
            currentKettleState = KettleState.EMPTY;
        }
    }

    public override void DoAction(VRHand hand)
    {
        //Do Holdable Actions
        base.DoAction(hand);

        //Check if have pressed the interact button
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            //If we are filled then initate boiled
            if(currentKettleState == KettleState.FILLED)
            {
                //Change state to be boiling
                currentKettleState = KettleState.FILLED_BOILING;
            }
        }
    }
}

//Lewis Hammond