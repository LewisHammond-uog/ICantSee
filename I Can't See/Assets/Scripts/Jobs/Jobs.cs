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
    //Enum for types of actions that jobs can have
    public enum JOB_ACTIONS
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
        ALARM_CLOCK
    }

    private AudioClip voiceClip;
    private JobActionInfo jobInfo;

    //Properties for actions and room
    public JOB_ACTIONS JobAction { get { return jobInfo.action; } }
    public JOB_OBJECTS JobObject { get { return jobInfo.objectType; } }
    public JOB_ROOMS JobRoom { get { return jobInfo.room; } }

    /// <summary>
    /// Create a Job and add it to the required jobs
    /// </summary>
    /// <param name="a_action"></param>
    /// <param name="a_room"></param>
    /// <param name="a_voiceline"></param>
    public Job(JOB_ACTIONS a_action, JOB_ROOMS a_room, JOB_OBJECTS a_objectType, AudioClip a_voiceline)
    {
        //Assign properties of the job
        jobInfo = new JobActionInfo();
        jobInfo.action = a_action;
        jobInfo.room = a_room;
        jobInfo.objectType = a_objectType;
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

public static class JobManager
{
    //Stacks for remaining and completed jobs
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
    /// <param name="a_jobInfo">Job info from the action that was completed</param>
    /// <returns>If action and room were for the next job in the stack</returns>
    public static bool RegisterJobAction(JobActionInfo a_jobInfo)
    {
        //Check if the action completed is the same as the next job, if it is
        //then register that job as completed
        Job currentJob = remainingJobs.Peek();
        if (currentJob.JobAction == a_jobInfo.action && currentJob.JobRoom == a_jobInfo.room && currentJob.JobObject == a_jobInfo.objectType)
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