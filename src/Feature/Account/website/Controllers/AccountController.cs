using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScDom.Featrure.Account.Models;
using Sitecore.XA.Foundation.Mvc.Controllers;

namespace ScDom.Featrure.Account.Controllers
{
    public class AccountController : StandardController
    {
        [AllowAnonymous]
        public ActionResult Registration()
        {
            RegistrationRenderingModel registrationModel = new RegistrationRenderingModel();
            return base.View("Registration", registrationModel);
        }
    }
}