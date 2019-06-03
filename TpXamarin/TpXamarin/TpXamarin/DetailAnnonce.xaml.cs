using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpXamarin.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TpXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailAnnonce : ContentPage
	{
        private Annonce annonce;

        public DetailAnnonce (int id )
		{
			InitializeComponent ();
            AnnonceDataAccess annonceData = new AnnonceDataAccess();
            annonce = annonceData.GetAnnonce(id);
            Annonce.BindingContext = annonce;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri("tel:" + annonce.Contact));
            }catch(Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }            
        }
    }
}