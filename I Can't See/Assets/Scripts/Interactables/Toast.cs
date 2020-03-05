using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : Holdable
{
    public override void DoAction(VRHand hand)
    {
        //Does pickup check for holdable
        base.DoAction(hand);
    }

    public void PlaceInToaster(Vector3 pos, Quaternion rot)
    {
        if(IsHeld)
        {
            this.CurrentHolder.DetachObject(this);
        }
        //this.CurrentHolder.DetachObject(this);
        this.transform.position = pos;
        this.transform.rotation = rot;
    }
}

// connor done
