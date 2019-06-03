using System;
using TpXamarin.DAL;
using TpXamarin.Model;
using Xamarin.Forms;

namespace TpXamarin
{
    public partial class MainPage : ContentPage
    {
        private UtilisateurDataAccess UtilisateurData;

        public MainPage()
        {
            this.Navigation.PopAsync(true);
            InitializeComponent();
            UtilisateurData = new UtilisateurDataAccess();

            login.Text = "Yoann";
            password.Text = "sohcahtoa";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = UtilisateurData.ConnexionUtilisateur(login.Text, password.Text);

            if(user != null)
            {
                UtilisateurActif.Utilisateur = user;
                Navigation.InsertPageBefore(new ListeProduit(), this);
                await Navigation.PopAsync();
            }
            error.Text = "Login ou Mot de passe incorrect.";
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Inscription(), this);
            await Navigation.PopAsync();
        }
    }
}
