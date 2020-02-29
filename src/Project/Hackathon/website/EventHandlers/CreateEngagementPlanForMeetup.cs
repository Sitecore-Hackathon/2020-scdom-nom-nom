using System;
using System.Globalization;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.AutomationPlans.Model;
using Sitecore.SecurityModel;

namespace ScDom.Project.Hackathon.EventHandlers
{
    public sealed class CreateEngagementPlanForMeetup
    {
        private readonly IDefinitionManager<IAutomationPlanDefinition> _automationPlanManager;

        public CreateEngagementPlanForMeetup(DefinitionManagerFactory definitionManager)
        :this(definitionManager.GetDefinitionManager<IAutomationPlanDefinition>())
        {
        }

        private CreateEngagementPlanForMeetup(IDefinitionManager<IAutomationPlanDefinition> automationPlanManager)
        {
            Assert.ArgumentNotNull(automationPlanManager, nameof(automationPlanManager));

            _automationPlanManager = automationPlanManager;
        }

        public void OnItemCreated(object sender, EventArgs args)
        {
            var eventArgs = Assert.ResultNotNull(args as SitecoreEventArgs);

            var createdItem = (eventArgs.Parameters[0] as ItemCreatedEventArgs)?.Item;

            if (createdItem?.TemplateID != Templates.MeetupInfo.ID || Sitecore.Publishing.PublishHelper.IsPublishing())
            {
                return;
            }

            var planId = Guid.NewGuid();
            
            var plan = new AutomationPlanDefinition(
                id: planId,
                alias: $"{createdItem.Name} meetup",
                culture: CultureInfo.InvariantCulture,
                name: "Welcome new user plan",
                createdDate: DateTime.UtcNow,
                createdBy: Sitecore.Context.GetUserName() ?? "[Background]")
            {
                ReentryMode = AutomationPlanReentryMode.Single,
                Description = $"A campaign plan for {createdItem.Name}",
                EndDate = DateTime.Now.Date.AddYears(1)
            };

            // TODO: think about god engagement plans. https://doc.sitecore.com/developers/93/sitecore-experience-platform/en/automation-plans.html

            using (new EditContext(createdItem, SecurityCheck.Disable))
            {
                var meetupInfo = new MeetupInfo(createdItem);

                meetupInfo.EngagementPlanReference.SetValue(planId.ToString(), force: true);
            }

            _automationPlanManager.SaveAsync(plan, activate: true);
        }
    }
}