using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Orientadores_PPgSI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Orientador> orientadores;

        public MainPage()
        {
            this.InitializeComponent();
            carregaListaDeOrientadores();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
        }

        private async void carregaListaDeOrientadores()
        {
            using (var client = new HttpClient())
            {
                string url = "http://orientadores-efitfashiontest.rhcloud.com/";
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                var resp = await client.SendAsync(req);
                var data = await resp.Content.ReadAsStringAsync();

                this.orientadores = JsonConvert.DeserializeObject<List<Orientador>>(data);

                ListaDeOrientadores.ItemsSource = this.orientadores;
            }
        }

        private void IrParaPaginaDeDetalhesDoOrientador(object sender, ItemClickEventArgs e)
        {
            Orientador orientador = e.ClickedItem as Orientador;
            Frame.Navigate(typeof(DatalhesDoOrientador), orientador);
        }

        private void IrParaHttpPost(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HttpPost));
        }

        private void IrParaDados(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dados));
        }
    }
}
