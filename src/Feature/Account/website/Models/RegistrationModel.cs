using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Links;
using Sitecore.XA.Foundation.Multisite;
using Sitecore.XA.Foundation.Mvc.Models;

namespace ScDom.Featrure.Account.Models
{
    public class RegistrationRenderingModel : RenderingModelBase
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(256, ErrorMessage = "The {0} should not exceed {1} characters.")]
        public string UserName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Password and {0} do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        public string CancelLink => LinkManager.GetItemUrl(ServiceLocator.ServiceProvider.GetService<IMultisiteContext>().HomeItem);
    }
}