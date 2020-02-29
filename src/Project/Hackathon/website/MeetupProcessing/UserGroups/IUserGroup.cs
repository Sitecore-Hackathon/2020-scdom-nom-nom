using System;
using System.Collections.Generic;
using ScDom.Project.Hackathon.MeetupProcessing.Meetups;
using Sitecore.Exceptions;

namespace ScDom.Project.Hackathon.MeetupProcessing.UserGroups
{
    /// <summary>
    /// The user group details.
    /// </summary>
    public interface IUserGroup
    {
        /// <summary>
        /// The name of the user group.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The identifier of the associated List Manager list.
        /// </summary>
        Guid? AssociatedList { get; }

        /// <summary>
        /// Gets meetups from today till given <paramref name="threshholdDate"/>.
        /// </summary>
        /// <param name="threshholdDate">The date to look meetups until.</param>
        /// <returns></returns>
        IReadOnlyCollection<IMeetupInfo> GetMeetups(DateTime threshholdDate);
    }

    public static class UserGroupExtensions
    {
        public static Guid GetAssociatedListRequired(this IUserGroup userGroup)
        {
            if (userGroup.AssociatedList.HasValue)
            {
                return userGroup.AssociatedList.Value;
            }

            throw new RequiredObjectIsNullException($"{userGroup.Name} does not have any list associated with it");
        }

        public static bool HasAssociatedList(this IUserGroup userGroup) => userGroup.AssociatedList.HasValue;
    }
}