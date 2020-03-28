using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    [Header("Water")]
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

    //Time to wait until we destroy this drop
    private float destroyTime = 5.0f;

    private void Start()
    {
        //Set to Destory after time
        Destroy(gameObject, destroyTime);
    }
}

//Rhys Wareham