using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class has infomation about the job within the world:
/// Action, Room and Assossicated Object Type
/// </summary>
[Serializable]
public class JobActionInfo
{
    public Job.JOB_ACTIONS action;
    public Job.JOB_OBJECTS objectType;
    public Job.JOB_ROOMS room;
}

public class Job
{

    #region Job Info (Actions, Rooms, Objects)

    //Enum for types of actions that jobs can have
    public enum JOB_ACTIONS
    {
        TURN_OFF,
        TURN_ON,
        OPEN,
        CLOSE,
        PICKUP,
        USE,
        PUT_ON, //FOR Putting Toothpaste on Toothbrush/CLOTHES
        FILLED,
        BOILED,
        LIQUID_TOUCH_POURABLE,
        POUR,
        EAT,
        ENTER_TRIGGER,
        EXIT_TRIGGER
    }

    //Enum for the rooms that jobs can happen in
    public enum JOB_ROOMS
    {
        BEDROOM,
        BATHROOM,
        HALLWAY,
        KITCHEN,
        PORCH
    }

    //Enum for the types of objects that jobs can happen in
    public enum JOB_OBJECTS
    {
        //Items
        ALARM_CLOCK,
        CURTAIN,
        LIGHT_SWITCH,
        TAP,
        TOAST,
        TOASTER,
        TOOTH_BRUSH,
        TOOTH_PASTE,
        DOOR,
        KETTLE,
        SHOWER,
        CUP,
        MILK,

        //Clothing
        CLOTHING_SHIRT,
        CLOTHING_TROUSERS,
    }
    #endregion

    private AudioClip voiceClip;
    public JobActionInfo JobInfo { get; private set; }

    /// <summary>
    /// Create a Job and add it to the required jobs
    /// </summary>
    /// <param name="a_action"></param>
    /// <param name="a_room"></param>
    /// <param name="a_voiceline"></param>
    public Job(JOB_ACTIONS a_action, JOB_ROOMS a_room, JOB_OBJECTS a_objectType, AudioClip a_voiceline)
    {
        //Assign properties of the job
        JobInfo = new JobActionInfo();
        JobInfo.action = a_action;
        JobInfo.room = a_room;
        JobInfo.objectType = a_objectType;
        voiceClip = a_voiceline;
    }

    public void PlayJobAudio()
    {
        if (voiceClip != null)
        {
            //Add to the jobs vo queue so that it is played in turn
            JobsVOPlayer.AddVOToPlayQueue(voiceClip);
        }
        else
        {
            Debug.Log("Attempted to Play Job Audio without a valid voice clip " + voiceClip.name);
        }
    }

}

public static class JobManager
{
    //Stacks for remaining and completed jobs
    private static Queue<Job> remainingJobs = new Queue<Job>();
    private static Stack<Job> completedJobs = new Stack<Job>();

    //Event Triggered when a job is complete
    public delegate void JobEvent();
    public static event JobEvent JobComplete;
    public static event JobEvent JobStarted;

    /// <summary>
    /// Adds a job that the player has to complete to the manager list
    /// </summary>
    public static void AddJobToComplete(Job.JOB_ACTIONS a_action, Job.JOB_ROOMS a_room, Job.JOB_OBJECTS a_objectType, AudioClip a_voiceline)
    {
        //Create a job to pass to the other function
        Job newJob = new Job(a_action, a_room, a_objectType, a_voiceline);
        //Call function with Job to add to queue
        AddJobToComplete(newJob);
    }

    /// <summary>
    /// Adds a job that the player has to complete to the manager list
    /// </summary>
    public static void AddJobToComplete(Job a_job)
    {
        remainingJobs.Enqueue(a_job);
    }

    /// <summary>
    /// Register an action that may be a job that the player has to complete
    /// </summary>
    /// <param name="a_jobInfo">Job info from the action that was completed</param>
    /// <returns>If action and room were for the next job in the stack</returns>
    public static bool RegisterJobAction(JobActionInfo a_jobInfo)
    {
        //Check that the job stack is not empty
        if(remainingJobs.Count == 0)
        {
            Debug.LogWarning("Tried to Register a Job Action but the stack was empty " + a_jobInfo.ToString());
            return false;
        }

        //Check if the action completed is the same as the next job, if it is
        //then register that job as completed
        Job currentJob = remainingJobs.Peek();
        if (currentJob.JobInfo.action == a_jobInfo.action && currentJob.JobInfo.room == a_jobInfo.room && currentJob.JobInfo.objectType == a_jobInfo.objectType)
        {
            CompleteJob(currentJob);
            return true;
        }

        //Actions and room did not meet current job
        return false;

    }

    /// <summary>
    /// Gets the current job that the player should be completing
    /// </summary>
    /// <returns>Current Job</returns>
    public static Job GetCurrentJob()
    {
        return remainingJobs.Peek();
    }

    /// <summary>
    /// Complete a Job and remove it from the remaining jobs stack
    /// </summary>
    /// <param name="a_job">Job</param>
    private static void CompleteJob(Job a_job)
    {
        //Remove the current job from the remaining jobs list
        if (a_job == remainingJobs.Peek())
        {
            Job completedJob = remainingJobs.Dequeue();

            //Call Job Complete Event
            JobComplete?.Invoke();

            //Add to completed jobs list
            completedJobs.Push(completedJob);

            //Play Audio of the next job
            if (remainingJobs.Count > 0)
            {
                Job nextJob = remainingJobs.Peek();
                nextJob.PlayJobAudio();

                //Call new Job event
                JobStarted?.Invoke();
            }
        }
    }
}

//Lewis Hammond