using Microsoft.Extensions.DependencyInjection;
using ScDom.Project.Hackathon.MeetupProcessing;
using Sitecore.DependencyInjection;

namespace ScDom.Project.Hackathon.DependencyInjection
{
    public sealed class StanaloneMeetupConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MeetupManager, DefaultMeetupManager>();
        }
    }
}