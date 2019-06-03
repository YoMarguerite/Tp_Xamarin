using System;
using TpXamarin.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TpXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifAnnonce : ContentPage
	{
        private ListView annonceList;
        private Annonce annonce;

        public ModifAnnonce(ListView _annonceList, Annonce _annonce)
        {
            InitializeComponent();
            categorie.ItemsSource = new string[] { "Console & Jeux Vidéos", "Immobilier", "Electroménager", "Sport et Véhicules" };
            annonceList = _annonceList;
            annonce = _annonce;

            titre.Text = annonce.Titre;
            description.Text = annonce.Description;
            prix.Text = annonce.Prix.ToString();
            contact.Text = annonce.Contact;
            categorie.SelectedItem = annonce.Categorie;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (VerifEntry())
            {
                try
                {
                    annonce.Titre = titre.Text;
                    annonce.Description = description.Text;
                    annonce.Prix = double.Parse(prix.Text);
                    annonce.Contact = contact.Text;
                    annonce.Categorie = categorie.SelectedItem.ToString();
                    annonce.Date = String.Format("Modifié le {0:dd/MM/yy}", DateTime.Today);

                    AnnonceDataAccess annonceData = new AnnonceDataAccess();
                    annonceData.SaveAnnonce(annonce);
                    annonceList.ItemsSource = annonceData.MyAnnonces(UtilisateurActif.Utilisateur.ID);
                    await DisplayAlert("Modification Annonce", "Votre Annonce '" + titre.Text + "' a bien été modifiée :) !", "Youpi !");
                    await this.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", ex.Message, "Oh nan...");
                }
            }
        }

        private bool VerifEntry()
        {
            bool verif = true;

            if ((titre.Text == null)||(titre.Text == ""))
            {
                errortitre.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errortitre.Text = "";
            }

            //if (description.Text == null)
            //{
            //    errordescription.Text = "Veuillez remplir ce champ";
            //    verif = false;
            //}
            //else
            //{
            //    errordescription.Text = "";
            //}

            if ((prix.Text == null)||(prix.Text == ""))
            {
                errorprix.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errorprix.Text = "";
            }

            if ((contact.Text == null)||(contact.Text == ""))
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


        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Supprimer ?", "Les données de l'annonce ne seront pas récupérables :( !", "Oui", "Non");
            if (result)
            {
                AnnonceDataAccess annonceData = new AnnonceDataAccess();
                annonceData.DeleteAnnonce(annonce);
                annonceList.ItemsSource = annonceData.MyAnnonces(UtilisateurActif.Utilisateur.ID);
                await Navigation.PopAsync();
            }
        }
    }
}