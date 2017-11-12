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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Orientadores_PPgSI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DatalhesDoOrientador : Page
    {
        Orientador orientador;
        OrientadorDetalhes orientadorDetalhes;

        public DatalhesDoOrientador()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.orientador = e.Parameter as Orientador;

            carregaInforcaoesDoOrientador();

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private async void carregaInforcaoesDoOrientador()
        {
            using (var client = new HttpClient())
            {
                string url = "http://orientadores-efitfashiontest.rhcloud.com/orientador/" + orientador.Id;
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                var resp = await client.SendAsync(req);
                var data = await resp.Content.ReadAsStringAsync();

                this.orientadorDetalhes = JsonConvert.DeserializeObject<OrientadorDetalhes>(data);

                NomeDoOrientador.Text = orientadorDetalhes.Nome;
                FotoDoOrientador.Source = new BitmapImage(new Uri("http://orientadores-efitfashiontest.rhcloud.com/foto/" + orientadorDetalhes.Foto));
                FormacaoDoOrientador.ItemsSource = orientadorDetalhes.Formacao;
                PesquisaDoOrientador.ItemsSource = orientadorDetalhes.Pesquisa;
            }
        }

        private async void IrParaOLattes(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(orientadorDetalhes.Lattes);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void MandarEmail(object sender, object e)
        {
            Uri uri = new Uri("mailto:" + orientadorDetalhes.Email);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
