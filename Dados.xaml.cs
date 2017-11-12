using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Dados : Page
    {
        public Dados()
        {
            this.InitializeComponent();

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var valorLocal = localSettings.Values["ValorLocal"];
            if(valorLocal != null)
            {
                DadosLocais.Text = valorLocal.ToString();
            }

            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            var valorNuvem = roamingSettings.Values["ValorNuvem"];
            if(valorNuvem != null)
            {
                DadosNuvem.Text = valorNuvem.ToString();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void SalvarDadosLocais(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["ValorLocal"] = DadosLocais.Text;
        }

        private void SalvarDadosNuvem(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["ValorNuvem"] = DadosNuvem.Text;
        }

        private void IrParaHome(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void IrParaHttpPost(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HttpPost));
        }
    }
}
