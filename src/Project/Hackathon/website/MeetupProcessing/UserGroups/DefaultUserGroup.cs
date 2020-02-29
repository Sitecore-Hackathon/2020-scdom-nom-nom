using System;
using System.Collections.Generic;
using System.Linq;
using ScDom.Project.Hackathon.MeetupProcessing.Meetups;
using Sitecore.Collections;
using Sitecore.Data.Items;

namespace ScDom.Project.Hackathon.MeetupProcessing.UserGroups
{
    public sealed class DefaultUserGroup : IUserGroup
    {
        private readonly UserGroupItem _userGroup;

        public DefaultUserGroup(Item userGroup)
        {
            _userGroup = new UserGroupItem(userGroup);
        }

        public string Name => _userGroup.UserGroupName;

        public Guid? AssociatedList
        {
            get
            {
                var value = _userGroup.AssociatedList.Value;

                return Guid.TryParse(value, out var listId) ? (Guid?) listId : null;
            }
        }

        public IReadOnlyCollection<IMeetupInfo> GetMeetups(DateTime threshholdDate)
        {
            var meetups = from child in _userGroup.InnerItem.GetChildren(ChildListOptions.SkipSorting | ChildListOptions.IgnoreSecurity)
                where child.TemplateID == Templates.MeetupInfo.ID
                let meetup = new DefaultMetupInfo(child)
                where meetup.StartDate.HasValue
                where meetup.StartDate < threshholdDate
                orderby meetup.StartDate
                select meetup;

            return meetups.ToArray();
        }
    }
}