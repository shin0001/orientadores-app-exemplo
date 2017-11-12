using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Orientadores_PPgSI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HttpPost : Page
    {
        public HttpPost()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private async void toUpperCase(object sender, RoutedEventArgs e)
        {
            string body = "texto=" + TextoDoUsuario.Text;

            var content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            using (var client = new HttpClient())
            {
                var resp = await client.PostAsync("http://orientadores-efitfashiontest.rhcloud.com/uppercase", content);

                string resultado = await resp.Content.ReadAsStringAsync();

                TextoMaiuscula.Text = resultado;
            }
        }

        private void IrParaHome(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void IrParaDados(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dados));
        }
    }
}
