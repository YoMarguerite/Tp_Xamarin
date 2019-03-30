using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpXamarin.DAL;
using TpXamarin.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TpXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inscription : ContentPage
	{
        UtilisateurDataAccess utilisateurData;

		public Inscription ()
		{
            InitializeComponent ();
            utilisateurData = new UtilisateurDataAccess();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (VerifEntry())
            {
                var user = new Utilisateur
                {
                    Nom = nom.Text,
                    Prenom = prenom.Text,
                    Login = login.Text,
                    Mdp = mdp.Text
                };
                user.ID = utilisateurData.SaveUtilisateur(user);
                UtilisateurActif.Utilisateur = user;
                await this.Navigation.PushAsync(new ListeProduit());
            }
        }

        private bool VerifEntry()
        {
            bool verif = true;

            if (nom.Text == null)
            {
                errornom.Text = "Veuillez remplir ce champ";
                verif = false;
            }

            if (prenom.Text == null)
            {
                errorprenom.Text = "Veuillez remplir ce champ";
                verif = false;
            }

            if (login.Text == null)
            {
                errorlogin.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else if (utilisateurData.GetFilteredUtilisateurs(login.Text) != null)
            {
                errorlogin.Text = "Login déjà utilisé";
                verif = false;
            }

            if (mdp.Text == null)
            {
                errormdp.Text = "Veuillez remplir ce champ";
                verif = false;
            }

            if (remdp.Text != mdp.Text)
            {
                errorremdp.Text = "Ne correspond pas";
                verif = false;
            }

            return verif;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync(true);
            await this.Navigation.PushAsync(new MainPage());
        }
    }
}