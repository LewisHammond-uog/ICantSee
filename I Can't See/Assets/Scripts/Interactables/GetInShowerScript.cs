﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInShowerScript : MonoBehaviour
{
    [SerializeField]
    private TwistInteractable showerTap;
    [SerializeField]
    private float timer = 6.0f;

    [SerializeField]
    protected JobActionInfo jobInfo;

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.GetComponent<HeadScript>() == true)
        {
            if(showerTap.rotatedAmount.x > 1.0f ||
                showerTap.rotatedAmount.y > 0.0f ||
                showerTap.rotatedAmount.z > 0.0f)
            {
                DoShowerTimer();
            }
        }
    }

    void DoShowerTimer()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}


//Rhys Wareham