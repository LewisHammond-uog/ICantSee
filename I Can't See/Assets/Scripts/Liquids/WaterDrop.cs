using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    //Object that created this water drop
    private GameObject creator;
    public GameObject dropCreatorObject
    {
        //Only allow setting if we don't already have a creator
        set
        {
            if(creator == null)
            {
                creator = value;
            }
        }
        get { return creator; }
    }
}
