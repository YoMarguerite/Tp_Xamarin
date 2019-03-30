using System;
using TpXamarin.DAL;
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
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = UtilisateurData.ConnexionUtilisateur(login.Text, password.Text);

            if(user != null)
            {
                UtilisateurActif.Utilisateur = user;
                await this.Navigation.PushAsync(new ListeProduit());
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new Inscription());
        }
    }
}
