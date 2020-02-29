using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using static ScDom.Project.Hackathon.Templates.UserGroup;

namespace ScDom.Project.Hackathon
{
    public sealed class UserGroupItem : CustomItem
    {
        public UserGroupItem(Item innerItem) : base(innerItem)
        {
        }

        public Field AssociatedList => InnerItem.Fields[Fields.ListReference];

        public string UserGroupName => Name;
    }
}