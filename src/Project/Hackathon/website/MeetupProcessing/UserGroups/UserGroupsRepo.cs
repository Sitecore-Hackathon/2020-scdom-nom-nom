using System;
using System.Collections.Generic;

namespace ScDom.Project.Hackathon.MeetupProcessing.UserGroups
{
    /// <summary>
    /// Provides <see cref="IUserGroup"/> repo.
    /// </summary>
    public abstract class UserGroupsRepo
    {
        public abstract IReadOnlyCollection<IUserGroup> GetAll();

        public abstract IReadOnlyCollection<IUserGroup> Get(IReadOnlyCollection<Guid> listIds);
    }
}