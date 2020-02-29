using Microsoft.Extensions.DependencyInjection;
using ScDom.Project.Hackathon.MeetupProcessing.UserGroups;
using Sitecore.DependencyInjection;

namespace ScDom.Project.Hackathon.DependencyInjection
{
    public sealed class GeneralConfiguration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<UserGroupsRepo, ItemUserGroupsRepo>();
        }
    }
}