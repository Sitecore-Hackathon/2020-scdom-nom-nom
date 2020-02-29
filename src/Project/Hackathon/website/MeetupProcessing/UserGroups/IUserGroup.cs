using System;
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