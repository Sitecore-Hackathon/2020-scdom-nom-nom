using System;
using Sitecore.Data.Items;

namespace ScDom.Project.Hackathon.MeetupProcessing
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
    }
}