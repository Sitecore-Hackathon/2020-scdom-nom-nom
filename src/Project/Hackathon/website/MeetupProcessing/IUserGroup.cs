using System;

namespace ScDom.Project.Hackathon.MeetupProcessing
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
        Guid AssociatedList { get; }
    }
}