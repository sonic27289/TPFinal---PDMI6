using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;

namespace TPFinal
{
    public partial class MainPage : ContentPage
    {

        CancellationTokenSource cts;
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var prod = await App.SQLiteDb.GetItemsAsync();
            if (prod != null)
            {
                lstBooks.ItemsSource = prod;
            }
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNomeMercadoria.Text))
            {
                Mercadoria Prod = new Mercadoria()
                {
                    NomeMercadoria = txtNomeMercadoria.Text,
                    Peso = txtPesoMercadoria.Text,
                    NCM = txtNCM.Text,
                    NomeProdutor = txtNomeProdutor.Text,
                    ProdutorEmail = txtProdutorEmail.Text
                };

                //Add New Mercadoria
                await App.SQLiteDb.SaveItemAsync(Prod);
                txtId.Text = string.Empty;
                txtPesoMercadoria.Text = string.Empty;
                txtNomeMercadoria.Text = string.Empty;
                txtNCM.Text = string.Empty;
                txtNomeProdutor.Text = string.Empty;
                txtProdutorEmail.Text = string.Empty;
                await DisplayAlert("Parabéns", "Mercadoria adicionada com sucesso!", "OK");
                //Get All Mercadoria
                var ListProd = await App.SQLiteDb.GetItemsAsync();
                if (ListProd != null)
                {
                    lstBooks.ItemsSource = ListProd;
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Por favor insira o nome da mercadoria", "OK");
            }
        }

        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {

                var Prod = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtId.Text));
                if (Prod != null)
                {
                    txtNomeMercadoria.Text = Prod.NomeMercadoria;
                    txtPesoMercadoria.Text = Prod.Peso;
                    txtNCM.Text = Prod.NCM;
                    txtNomeProdutor.Text = Prod.NomeProdutor;
                    txtProdutorEmail.Text = Prod.ProdutorEmail;
                    await DisplayAlert("Sucesso em ler ID", "Mercadoria: " + Prod.NomeMercadoria, "OK");
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Insira o ID da Mercadoria", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                Mercadoria Prod = new Mercadoria()
                {
                    Id = Convert.ToInt32(txtId.Text),
                    NomeMercadoria = txtNomeMercadoria.Text,
                    Peso = txtPesoMercadoria.Text,
                    NCM = txtNCM.Text,
                    NomeProdutor = txtNomeProdutor.Text,
                    ProdutorEmail = txtProdutorEmail.Text
                };

                await App.SQLiteDb.SaveItemAsync(Prod);

                txtId.Text = string.Empty;
                txtNomeMercadoria.Text = string.Empty;
                txtPesoMercadoria.Text = string.Empty;
                txtNCM.Text = string.Empty;
                txtNomeProdutor.Text = string.Empty;
                txtProdutorEmail.Text = string.Empty;
                await DisplayAlert("Parabéns", "Mercadoria atualizada com sucesso!", "OK");
                var ListProd = await App.SQLiteDb.GetItemsAsync();
                if (ListProd != null)
                {
                    lstBooks.ItemsSource = ListProd;
                }

            }
            else
            {
                await DisplayAlert("Atenção", "Insira o ID da Mercadoria", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                var Prod = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtId.Text));
                if (Prod != null)
                {
                    //Delete Mercadoria
                    await App.SQLiteDb.DeleteItemAsync(Prod);
                    txtId.Text = string.Empty;
                    txtNomeMercadoria.Text = string.Empty;
                    txtPesoMercadoria.Text = string.Empty;
                    txtNCM.Text = string.Empty;
                    txtNomeProdutor.Text = string.Empty;
                    txtProdutorEmail.Text = string.Empty;
                    await DisplayAlert("Ação efetuada com sucesso", "Mercadoria Excluida", "OK");

                    //Get All Mercadoria
                    var ListProd = await App.SQLiteDb.GetItemsAsync();
                    if (ListProd != null)
                    {
                        lstBooks.ItemsSource = ListProd;
                    }
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Insira o ID da Mercadoria", "OK");
            }
        }

        private async void BtnLocalizacao_Clicked(object sender, EventArgs e)
        {
            try
            {
                var lastLocation = await Geolocation.GetLastKnownLocationAsync();
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                
                var newLocation = await Geolocation.GetLocationAsync(request, cts.Token);

                if (lastLocation != null && newLocation != null)
                {
                    await DisplayAlert("Exibindo localização passada e atual: ",
                        $"Última localização:\n\n" +
                        $"Latitude: {lastLocation.Latitude}\n" +
                        $"Longitude: {lastLocation.Longitude}\n" +
                        $"Altitude: {lastLocation.Altitude}\n\n" +
                        $"Localização atualizada:\n\n" +
                        $"Latitude: {newLocation.Latitude}\n" +
                        $"Longitude: {newLocation.Longitude}\n" +
                        $"Altitude: {newLocation.Altitude}\n", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Erro em acessar localização atual", $"Erro ao mostrar localização do dispositivo", "OK");
            }
        }
    }
}
