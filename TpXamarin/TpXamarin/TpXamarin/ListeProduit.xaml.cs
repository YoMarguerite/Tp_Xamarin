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
	public partial class ListeProduit : ContentPage
	{
        public AnnonceDataAccess AnnonceData;

        public ListeProduit()
        {
            InitializeComponent();
            AnnonceData = new AnnonceDataAccess();
            AnnonceList.ItemsSource = AnnonceData.NotMyAnnonces(UtilisateurActif.Utilisateur.ID);
        }
        
        private async void AnnonceList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new DetailAnnonce(((Annonce)AnnonceList.SelectedItem).ID));
        }

        private void Entry_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if(recherche.Text != null)
            {
                AnnonceList.ItemsSource = AnnonceData.GetFilteredAnnonces(recherche.Text, UtilisateurActif.Utilisateur.ID);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new MyAnnonce());
        }
        
    }
}