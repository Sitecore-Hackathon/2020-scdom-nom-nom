﻿using System.Collections.Generic;
using Sitecore.Analytics.Tracking;

namespace ScDom.Project.Hackathon.MeetupProcessing
{

    /// <summary>
    /// Finds <see cref="IUserGroup"/> for <see cref="Contact"/>.
    /// </summary>
    public abstract class MeetupManager
    {
        /// <summary>
        /// Finds <see cref="IUserGroup"/> that contact was <see cref="Add"/> to.
        /// </summary>
        /// <param name="contact">The contact to have user groups found.</param>
        /// <returns>The collection of user groups contact is a member of.</returns>
        public abstract IReadOnlyCollection<IUserGroup> FindFor(Contact contact);

        /// <summary>
        /// Adds the contact to the <see cref="IUserGroup"/>.
        /// </summary>
        /// <param name="contact">The contact to be added to the group.</param>
        /// <param name="userGroup">The group to have contact added.</param>
        public abstract void Add(Contact contact, IUserGroup userGroup);
    }
}