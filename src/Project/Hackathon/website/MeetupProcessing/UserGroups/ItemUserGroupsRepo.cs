using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Abstractions;
using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.SecurityModel;

namespace ScDom.Project.Hackathon.MeetupProcessing.UserGroups
{
    public sealed class ItemUserGroupsRepo : UserGroupsRepo
    {
        private readonly Item _repoRoot;

        public ItemUserGroupsRepo(BaseFactory factory, BaseSettings settings)
        : this(FindRoot(factory, settings))
        {
        }

        private static Item FindRoot(BaseFactory factory, BaseSettings settings)
        {
            var database = factory.GetDatabase(settings.GetSetting("Meetup.DefaultRepo", defaultValue: "web"), assert: true);
            using (new SecurityDisabler())
            {
                var item = database.GetItem(ItemIDs.MeetupContentRootId);

                return item ?? throw new RequiredObjectIsNullException($"Root item {ItemIDs.MeetupContentRootId} was not found in {database.Name}");
            }
        }

        protected ItemUserGroupsRepo(Item repoRoot)
        {
            Assert.ArgumentNotNull(repoRoot, nameof(repoRoot));
            _repoRoot = repoRoot;
        }

        public override IReadOnlyCollection<IUserGroup> GetAll()
        {
            var all = from child in _repoRoot.GetChildren(ChildListOptions.IgnoreSecurity | ChildListOptions.SkipSorting)
                      where child.TemplateID == Templates.UserGroup.ID
                      select new DefaultUserGroup(child);

            return all.ToArray();
        }

        public override IReadOnlyCollection<IUserGroup> Get(IReadOnlyCollection<Guid> listIds)
        {
            if (!listIds.Any())
            {
                return Array.Empty<IUserGroup>();
            }

            var matched = from candidate in GetAll()
                          let listId = candidate.AssociatedList
                          where listId.HasValue
                          where listIds.Contains(listId.Value)
                          select candidate;

            return matched.ToArray();
        }
    }
}