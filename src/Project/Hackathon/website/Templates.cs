using Sitecore.Data;
using SXA = Sitecore.XA.Feature;
namespace ScDom.Project.Hackathon
{
    public static class Templates
    {
        /// <summary>
        /// The page data - <see cref="Fields.Title"/> along with <see cref="Fields.Content"/>.
        /// </summary>
        public struct Page
        {
            public static readonly ID ID = new ID("{9D9D5E75-2AEA-44A7-9241-5840ECC7879A}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{1ED0DD7D-5169-41A5-83AC-8990377CE226}");

                public static readonly ID Content = new ID("{00D25759-CC5A-4855-9C5C-2F5605FC7A58}");
            }
        }
    }
}