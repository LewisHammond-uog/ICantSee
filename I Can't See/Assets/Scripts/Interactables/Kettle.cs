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

    //Time it takes for the kettle to boil
    private const float timeToBoil = 5.0f;
    //Timer from when the kettle boil
    private float timeSinceBoil;

    private void Start()
    {
        //Set Kettle State to the empty
        currentKettleState = KettleState.EMPTY;

        //Init time since boil to 0 as boil will not yet be
        //initiated
        timeSinceBoil = 0.0f;

    }

    private new void Update()
    {
        //Call Base Update on the pourable object
        base.Update();

        //If Boiling keep going until boiled
        if(currentKettleState == KettleState.FILLED_BOILING)
        {
            //Increment boil timer
            timeSinceBoil += Time.deltaTime;

            //Check if we should be boiled, if so set it
            //the kettle as boiled
            if(timeSinceBoil >= timeToBoil)
            {
                currentKettleState = KettleState.FILLED_BOILED;
            }

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