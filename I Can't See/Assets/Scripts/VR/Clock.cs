using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Holdable
{

    public override void DoAction(VRHand hand)
    {
        //Does pickup check for holdable
        base.DoAction(hand);

        //Check if D-Pad has been pressed to turn off alarm
        

    }
}
