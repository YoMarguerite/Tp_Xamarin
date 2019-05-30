using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TpXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyAnnonce : ContentPage
	{
        private AnnonceDataAccess annonceData;

		public MyAnnonce ()
		{
			InitializeComponent ();
            annonceData = new AnnonceDataAccess();
            AnnonceList.ItemsSource = annonceData.MyAnnonces(UtilisateurActif.Utilisateur.ID);
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new CreateAnnonce(AnnonceList));
        }
    }
}