using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
    //Enum for types of actions that jobs can have
    public enum ACTION_TYPE
    {
        TURN_OFF,
        TURN_ON,
        OPEN,
        CLOSE,
        PICKUP,
        USE,
        ENTER_TRIGGER,
        EXIT_TRIGGER
    }

    //Enum for the rooms that jobs can happen in
    public enum ROOMS
    {
        BEDROOM,
        BATHROOM,
        HALLWAY,
        KITCHEN,
        PORCH
    }

    private AudioClip voiceClip;
    private ACTION_TYPE action;
    private ROOMS requiredRoom;

    //Properties for actions and room
    public ACTION_TYPE JobAction { get { return action; } }
    public ROOMS JobRoom { get { return requiredRoom; } }

    /// <summary>
    /// Create a Job and add it to the required jobs
    /// </summary>
    /// <param name="a_action"></param>
    /// <param name="a_room"></param>
    /// <param name="a_voiceline"></param>
    public Job(ACTION_TYPE a_action, ROOMS a_room, AudioClip a_voiceline)
    {
        //Assign properties of the job
        action = a_action;
        requiredRoom = a_room;
        voiceClip = a_voiceline;

        //Add to Stack
        JobManager.AddJobToComplete(this);
    }

    public void PlayJobAudio()
    {
        //[TO DO] Implement Playing Job Audio through and audio player
        throw new NotImplementedException();
    }


}

static class JobManager
{
    private static Stack<Job> remainingJobs;
    private static Stack<Job> completedJobs;

    /// <summary>
    /// Adds a job that the player has to complete to the manager list
    /// </summary>
    public static void AddJobToComplete(Job a_job)
    {
        remainingJobs.Push(a_job);
    }

    /// <summary>
    /// Register an action that may be a job that the player has to complete
    /// </summary>
    /// <param name="actionType">Type of action completed</param>
    /// <param name="completedRoom">Room that the action was completed in</param>
    /// <returns>If action and room were for the next job in the stack</returns>
    public static bool RegisterJobAction(Job.ACTION_TYPE a_actionType, Job.ROOMS a_completedRoom)
    {
        //Check if the action completed is the same as the next job, if it is
        //then register that job as completed
        Job currentJob = remainingJobs.Peek();
        if (currentJob.JobAction == a_actionType && currentJob.JobRoom == a_completedRoom)
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
            Job completedJob = remainingJobs.Pop();

            //Add to completed jobs list
            completedJobs.Push(completedJob);

            //Play Audio of the next job
            Job nextJob = remainingJobs.Peek();
            nextJob.PlayJobAudio();
        }
    }


}
