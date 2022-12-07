using Plugin.Geolocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TPFINALPDM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoLocalizaPage : ContentPage
    {
        public GeoLocalizaPage()
        {
            InitializeComponent();
        }
        double latitude = 0;
        double longitude = 0;
        private async void btnGeolocalizacao_Clicked(object sender, EventArgs e)
        {
            lblGeolocalizacao.Text = "Obtendo a geolocalização....\n";
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync();
                lblGeolocalizacao.Text = "";
                lblGeolocalizacao.Text += "Status: " + position.Timestamp + "\n";
                lblGeolocalizacao.Text += "Latitude: " + position.Latitude + "\n";
                lblGeolocalizacao.Text += "Longitude: " + position.Longitude;
                latitude = position.Latitude;
                longitude = position.Longitude;
            }
            catch (Exception ex)
            {
                lblGeolocalizacao.Text = "";
                await DisplayAlert("Erro : ", ex.Message, "OK");
            }
        }

    }
}