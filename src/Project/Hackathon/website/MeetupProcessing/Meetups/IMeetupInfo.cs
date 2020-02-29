using System;

namespace ScDom.Project.Hackathon.MeetupProcessing.Meetups
{
    public interface IMeetupInfo
    {
        Guid? AutomationPlan { get; } 

        string Name { get; }

        DateTime? StartDate { get; }
    }
}