using System;
using Sitecore.Data.Items;

namespace ScDom.Project.Hackathon.MeetupProcessing.Meetups
{
    public class DefaultMetupInfo : IMeetupInfo
    {
        private readonly MeetupInfo _inner;
        public DefaultMetupInfo(Item inner) => _inner = new MeetupInfo(inner);

        public Guid? AutomationPlan
        {
            get
            {
                var value = _inner.EngagementPlanReference.Value;

                return Guid.TryParse(value, out var listId) ? (Guid?)listId : null;
            }
        }

        public string Name => _inner.Name;

        public DateTime? StartDate
        {
            get
            {
                var start = _inner.InnerItem[Templates.MeetupInfo.Fields.EventStart];
                return DateTime.TryParse(start, out var date) ? (DateTime?) date : null;
            }
        }
    }
}