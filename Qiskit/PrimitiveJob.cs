using System;
using System.Collections.Generic;

public class PrimitiveJob
{
    public int JobId { get; set; }
    public string JobType { get; set; }
    public bool IsCompleted { get; set; }

    public PrimitiveJob(int jobId, string jobType)
    {
        JobId = jobId;
        JobType = jobType;
        IsCompleted = false;
    }

    // Method to simulate job completion
    public void CompleteJob()
    {
        IsCompleted = true;
        // Add logic to mark job as completed
    }

    // Method to check the job status
    public string GetJobStatus()
    {
        return IsCompleted ? "Completed" : "Pending";
    }
}
