using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public class App : Application
    {
        Label lbl;
        HMS.Auth.AppPlatform platform;
        HMS.Auth.IHttpClient hmsClient;
        public App(HMS.Auth.AppPlatform platform)
        {
            init(platform, null);

        }
        public App(HMS.Auth.AppPlatform platform, HMS.Auth.IHttpClient hmsClient)
        {
            init(platform, hmsClient);
        }
        public void init(HMS.Auth.AppPlatform platform, HMS.Auth.IHttpClient hmsClient)
        {
            this.platform = platform;
            this.hmsClient = hmsClient;

            lbl = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Welcome to Xamarin Forms!"
            };
            // The root page of your application
            var content = new ContentPage
            {
                Title = "App1",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {lbl
                       
                    }
                }
            };

            MainPage = new NavigationPage(content);
            update();
        }
        private void update()
        {
            lbl.Text = "loading ..";

            string ret = "";
            Task.Run(async () =>
            {
                HMS.Auth.Login hmsLogin = new HMS.Auth.LoginNTLM("http://192.168.30.109/NTLM", "winxmap", "authtest", "hello@123");
                HMS.Auth.haHttpClient.hmsClient = hmsClient;
                hmsLogin.setPlatform(this.platform);
                ret = await hmsLogin.getJSON("data.json");
                
            }).Wait();
            lbl.Text = ret;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
