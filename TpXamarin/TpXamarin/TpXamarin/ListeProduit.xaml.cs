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
	public partial class ListeProduit : ContentPage
	{
        public AnnonceDataAccess AnnonceData;

        public ListeProduit()
        {
            InitializeComponent();
            AnnonceData = new AnnonceDataAccess();
            AnnonceList.ItemsSource = AnnonceData.Annonces;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AnnonceData.AddNewAnnonce();
            AnnonceList.ItemsSource = AnnonceData.Annonces;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            AnnonceData.DeleteAllAnnonces();
            AnnonceList.ItemsSource = AnnonceData.Annonces;
        }
    }
}