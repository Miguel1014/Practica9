using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Practica9.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, ISQLAzure
    {

        private MobileServiceUser usuario;
        public async Task<MobileServiceUser> Authenticate()
        {
            var message = string.Empty;
            try
            {
                usuario = await Practica9.Vista.Cliente.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Facebook, "tesh.azurewebsites.net");
                if (usuario != null)
                {
                    message = string.Format("Usuario autenticado {0}.", usuario.UserId);
                    //await new MessageDialog(user.MobileServiceAuthenticationToken, "Token").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            IUIAlertViewDelegate iUIAlert = null;
            UIAlertView avAlert = new UIAlertView("Resultado de autenticación", message, iUIAlert, "ok", null);
            avAlert.Show();
            return usuario;

        }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
