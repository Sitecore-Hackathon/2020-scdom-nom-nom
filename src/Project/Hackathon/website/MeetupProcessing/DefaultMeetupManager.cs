using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Analytics.Tracking;
using Sitecore.Diagnostics;
using Sitecore.ListManagement.XConnect.Web;
using Sitecore.Text;

namespace ScDom.Project.Hackathon.MeetupProcessing
{
    public sealed class DefaultMeetupManager : MeetupManager
    {
        private readonly ISubscriptionService _listEnrollment;

        /// <summary>
        /// The name of the tag to store list ids in (as in old good days).
        /// </summary>
        private const string UserGroupTagKey = nameof(UserGroupTagKey);

        public DefaultMeetupManager(ISubscriptionService listEnrollment)
        {
            Assert.ArgumentNotNull(listEnrollment, nameof(listEnrollment));

            _listEnrollment = listEnrollment;
        }

        public override void Add(Contact contact, IUserGroup userGroup)
        {
            Assert.ArgumentNotNull(contact, nameof(contact));
            Assert.ArgumentNotNull(userGroup, nameof(userGroup));

            var candidate = userGroup.AssociatedList.ToString();
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

            _listEnrollment.Subscribe(userGroup.AssociatedList, contact.ContactId);
        }


        public override IReadOnlyCollection<IUserGroup> FindFor(Contact contact) => throw new NotImplementedException("not yet");

        private static void Update(Contact contact, string listIds) => contact.Tags.Set(UserGroupTagKey, listIds);

        private static bool TryGetListIds(Contact contact, out ListString lists)
        {
            var value = contact.Tags.Find(UserGroupTagKey)?.Values.FirstOrDefault()?.Value ?? string.Empty;

            lists = new ListString(value);
            return lists.Count > 0;
        }
    }
}
