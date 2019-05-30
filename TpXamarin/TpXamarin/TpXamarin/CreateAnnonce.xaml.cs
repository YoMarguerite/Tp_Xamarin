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
	public partial class CreateAnnonce : ContentPage
	{
        private ListView annonceList;

		public CreateAnnonce (ListView _annonceList)
		{
			InitializeComponent ();
            categorie.ItemsSource = new string[] { "Console & Jeux Vidéos", "Immobilier", "Electroménager", "Sport et Véhicules" };
            categorie.SelectedItem = "Console & Jeux Vidéos";
            annonceList = _annonceList;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (VerifEntry())
            {
                try
                {
                    AnnonceDataAccess annonceData = new AnnonceDataAccess();
                    annonceData.SaveAnnonce(new Annonce
                    {
                        Titre = titre.Text,
                        Description = description.Text,
                        Prix = double.Parse(prix.Text),
                        Contact = contact.Text,
                        Categorie = categorie.SelectedItem.ToString(),
                        Date = new DateTime().ToString(),
                        User = UtilisateurActif.Utilisateur.ID,
                        UserName = UtilisateurActif.Utilisateur.Prenom + " " + UtilisateurActif.Utilisateur.Nom
                    });
                    annonceList.ItemsSource = annonceData.MyAnnonces(UtilisateurActif.Utilisateur.ID);
                    await DisplayAlert("Nouvelle Annonce", "Votre Annonce '" + titre.Text + "' a bien été créée", "Youpi !");
                    await this.Navigation.PopAsync();
                }catch(Exception ex)
                {
                    await DisplayAlert("Erreur", ex.Message, "Oh nan...");
                }
            }
        }

        private bool VerifEntry()
        {
            bool verif = true;

            if (titre.Text == null)
            {
                errortitre.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errortitre.Text = "";
            }

            if (description.Text == null)
            {
                errordescription.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errordescription.Text = "";
            }

            if (prix.Text == null)
            {
                errorprix.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errorprix.Text = "";
            }

            if (contact.Text == null)
            {
                errorcontact.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errorcontact.Text = "";
            }

            return verif;
        }
    }
}