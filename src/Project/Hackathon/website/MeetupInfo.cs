using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using static ScDom.Project.Hackathon.Templates.MeetupInfo;

namespace ScDom.Project.Hackathon
{
    public sealed class MeetupInfo: CustomItem
    {
        public MeetupInfo(Item innerItem) : base(innerItem)
        {
        }

        public Field EngagementPlanReference => InnerItem.Fields[Fields.EngagementPlanReference];
    }
}