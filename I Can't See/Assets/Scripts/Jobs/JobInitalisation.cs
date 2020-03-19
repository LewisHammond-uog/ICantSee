using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to initalise all of the worlds jobs
/// </summary>
public class JobInitalisation : MonoBehaviour
{
    private static bool jobsInitalised = false;

    // Start is called before the first frame update
    void Awake()
    {
        //Make sure jobs are not already initalised
        if (jobsInitalised)
        {
            return;
        }

        //---------Initalise all of the jobs---------
        //Bedroom
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.TURN_OFF, Job.JOB_ROOMS.BEDROOM, Job.JOB_OBJECTS.ALARM_CLOCK, (AudioClip)(Resources.Load("VoiceLine-Job-1")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.OPEN, Job.JOB_ROOMS.BEDROOM, Job.JOB_OBJECTS.CURTAIN, (AudioClip)(Resources.Load("VoiceLine-Job-2")));
        //JobManager.AddJobToComplete(Job.JOB_ACTIONS.OPEN, Job.JOB_ROOMS.BATHROOM, Job.JOB_OBJECTS.DOOR, (AudioClip)(Resources.Load("VoiceLine-Job-3")));

        //Bathroom
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PUT_ON, Job.JOB_ROOMS.BATHROOM, Job.JOB_OBJECTS.TOOTH_BRUSH, (AudioClip)(Resources.Load("VoiceLine-Job-4")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.TURN_ON, Job.JOB_ROOMS.BATHROOM, Job.JOB_OBJECTS.TAP, (AudioClip)(Resources.Load("VoiceLine-Job-5")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.USE, Job.JOB_ROOMS.BATHROOM, Job.JOB_OBJECTS.TOOTH_BRUSH, (AudioClip)(Resources.Load("VoiceLine-Job-6")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.USE, Job.JOB_ROOMS.BATHROOM, Job.JOB_OBJECTS.SHOWER, (AudioClip)(Resources.Load("VoiceLine-Job-7")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.OPEN, Job.JOB_ROOMS.BATHROOM, Job.JOB_OBJECTS.DOOR, (AudioClip)(Resources.Load("VoiceLine-Job-8")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PUT_ON, Job.JOB_ROOMS.BEDROOM, Job.JOB_OBJECTS.CLOTHING_SHIRT, (AudioClip)(Resources.Load("VoiceLine-Job-9")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PUT_ON, Job.JOB_ROOMS.BEDROOM, Job.JOB_OBJECTS.CLOTHING_TROUSERS, (AudioClip)(Resources.Load("VoiceLine-Job-10")));
        //JobManager.AddJobToComplete(Job.JOB_ACTIONS.OPEN, Job.JOB_ROOMS.BEDROOM, Job.JOB_OBJECTS.DOOR, (AudioClip)(Resources.Load("VoiceLine-Job-11")));

        //Kitchen
        //Tea
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.FILLED, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.KETTLE, (AudioClip)(Resources.Load("VoiceLine-Job-12")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.BOILED, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.KETTLE, (AudioClip)(Resources.Load("VoiceLine-Job-13")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.LIQUID_TOUCH_POURABLE, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.CUP, (AudioClip)(Resources.Load("VoiceLine-Job-14")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PICKUP, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.MILK, (AudioClip)(Resources.Load("VoiceLine-Job-15")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.POUR, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.MILK, (AudioClip)(Resources.Load("VoiceLine-Job-16")));
        //Toast
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PICKUP, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.TOAST, (AudioClip)(Resources.Load("VoiceLine-Job-17")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PICKUP, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.TOASTER, (AudioClip)(Resources.Load("VoiceLine-Job-18")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.PICKUP, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.TOAST, (AudioClip)(Resources.Load("VoiceLine-Job-19")));
        JobManager.AddJobToComplete(Job.JOB_ACTIONS.EAT, Job.JOB_ROOMS.KITCHEN, Job.JOB_OBJECTS.TOAST, (AudioClip)(Resources.Load("VoiceLine-Job-20")));

        //Porch
        //JobManager.AddJobToComplete(Job.JOB_ACTIONS.OPEN, Job.JOB_ROOMS.PORCH, Job.JOB_OBJECTS.DOOR, (AudioClip)(Resources.Load("VoiceLine-Job-21")));

        jobsInitalised = true;
        //---------End of Jobs Initalisation---------
    }
}

//Lewis Hammond