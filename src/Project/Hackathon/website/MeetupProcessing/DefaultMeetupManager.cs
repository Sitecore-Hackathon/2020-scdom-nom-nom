using System;
using System.Collections.Generic;
using System.Linq;
using ScDom.Project.Hackathon.MeetupProcessing.UserGroups;
using Sitecore.Analytics.Tracking;
using Sitecore.Diagnostics;
using Sitecore.ListManagement.XConnect.Web;
using Sitecore.Text;

namespace ScDom.Project.Hackathon.MeetupProcessing
{
    public sealed class DefaultMeetupManager : MeetupManager
    {
        private readonly ISubscriptionService _listEnrollment;
        private readonly UserGroupsRepo _userGroupsRepo;

        /// <summary>
        /// The name of the tag to store list ids in (as in old good days).
        /// </summary>
        private const string UserGroupTagKey = nameof(UserGroupTagKey);

        public DefaultMeetupManager(ISubscriptionService listEnrollment, UserGroupsRepo userGroupsRepo)
        {
            Assert.ArgumentNotNull(listEnrollment, nameof(listEnrollment));
            Assert.ArgumentNotNull(userGroupsRepo, nameof(userGroupsRepo));

            _listEnrollment = listEnrollment;
            _userGroupsRepo = userGroupsRepo;
        }

        public override void Add(Contact contact, IUserGroup userGroup)
        {
            Assert.ArgumentNotNull(contact, nameof(contact));
            Assert.ArgumentNotNull(userGroup, nameof(userGroup));

            var listId = userGroup.GetAssociatedListRequired();
            var candidate = listId.ToString();

            if (TryGetListIds(contact, out var lists))
            {
                if (lists.Contains(candidate, StringComparer.OrdinalIgnoreCase))
                {
                    return; // Already enrolled.
                }

                lists.Add(candidate);
                Update(contact, lists.ToString());
            }
            else
            {
                Update(contact, candidate);
            }

            _listEnrollment.Subscribe(listId, contact.ContactId);
        }

        public override IReadOnlyCollection<IUserGroup> FindFor(Contact contact)
        {
            TryGetListIds(contact, out var listIds);

            var ids = from raw in listIds
                where Guid.TryParse(raw, out _)
                select Guid.Parse(raw);

            return _userGroupsRepo.Get(ids.ToArray());
        }

        private static void Update(Contact contact, string listIds) => contact.Tags.Set(UserGroupTagKey, listIds);

        private static bool TryGetListIds(Contact contact, out ListString lists)
        {
            var value = contact.Tags.Find(UserGroupTagKey)?.Values.FirstOrDefault()?.Value ?? string.Empty;

            lists = new ListString(value);
            return lists.Count > 0;
        }
    }
}
