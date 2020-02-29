using System;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.ListManagement.Services.Model;
using Sitecore.ListManagement.Services.Repositories;
using Sitecore.SecurityModel;

namespace ScDom.Project.Hackathon.EventHandlers
{
    public sealed class CreateListForUserGroup
    {
        private readonly IFetchRepository<ContactListModel> _listManager;

        public CreateListForUserGroup(IFetchRepository<ContactListModel> listManager)
        {
            _listManager = listManager;
        }

        public void OnItemCreated(object sender, EventArgs args)
        {
            var eventArgs = Assert.ResultNotNull(args as SitecoreEventArgs);

            var createdItem = (eventArgs.Parameters[0] as ItemCreatedEventArgs)?.Item;

            if (createdItem?.TemplateID != Templates.UserGroup.ID || Sitecore.Publishing.PublishHelper.IsPublishing())
            {
                return;
            }

            var listId = Guid.NewGuid().ToString("B");
            _listManager.Add(new ContactListModel
            {
                Name = createdItem.Name, // TODO: Add name validations (that list by name exists; name is good enough..).
                Id = listId,
                Description = $"List to store {createdItem.Name} members."
            });

            var list = Assert.ResultNotNull(_listManager.FindById(listId), "could not find the list by id that was created in previous line {0}", listId);

            using (new EditContext(createdItem, SecurityCheck.Disable))
            {
                var userGroupItem = new UserGroupItem(createdItem);

                userGroupItem.AssociatedList.SetValue(list.Id, force: true);
            }
        }
    }
}