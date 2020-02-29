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

        /// <summary>
        /// The user group details, such as <see cref="Fields.Data"/> (group name, description), as well as geographical location.
        /// <para>Contains image info as well.</para>
        /// </summary>
        public struct UserGroup
        {
            public static readonly ID ID = new ID("{399C22AF-BBDB-4218-81B0-7D313C5E1848}");

            public struct Fields
            {
                public static readonly ID ListReference = new ID("{B93EA3A2-2E4E-49F8-AAFE-7862B355245D}");

                public static readonly SXA.Maps.Templates.Map.Fields Map = new SXA.Maps.Templates.Map.Fields();

                public static readonly Page.Fields Data = new Page.Fields();

                public static readonly SXA.Media.Templates.Image.Fields Image = new SXA.Media.Templates.Image.Fields();
            }
        }


        /// <summary>
        /// The meeting details, such as <see cref="Fields.Data"/> (name, description), as well as geographical location.
        /// <para>Contains image info as well.</para>
        /// </summary>
        public struct MeetupInfo
        {
            public static readonly ID ID = new ID("{E7F9974F-1C01-4C82-9F2C-D347548EC317}");

            public struct Fields
            {
                public static readonly ID EngagementPlanReference = new ID("{C00DCB1D-41B4-420B-8C8F-36D327D61E21}");

                public static readonly SXA.Maps.Templates.Map.Fields Map = new SXA.Maps.Templates.Map.Fields();

                public static readonly Page.Fields Data = new Page.Fields();

                public static readonly SXA.Media.Templates.Image.Fields Image = new SXA.Media.Templates.Image.Fields();

                public static readonly SXA.Events.Templates.CalendarEvent.Fields CalendarEvents = new SXA.Events.Templates.CalendarEvent.Fields();
            }
        }
    }
}