using System;
using System.Linq;
using System.Security.Cryptography;
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
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = utilisateurData.GetMd5Hash(md5Hash, mdp.Text);

                    var user = new Utilisateur
                    {
                        Nom = nom.Text,
                        Prenom = prenom.Text,
                        Login = login.Text,
                        Mdp = hash
                    };
                    user.ID = utilisateurData.SaveUtilisateur(user);
                    UtilisateurActif.Utilisateur = user;
                }

                Navigation.InsertPageBefore(new ListeProduit(), this);
                await Navigation.PopAsync();
            }
        }

        private bool VerifEntry()
        {
            bool verif = true;

            if ((nom.Text == null)|| (nom.Text == ""))
            {
                errornom.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errornom.Text = "";
            }

            if ((prenom.Text == null)||(prenom.Text == ""))
            {
                errorprenom.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else
            {
                errorprenom.Text = ""; 
            }
            
            if ((login.Text == null)||(login.Text == ""))
            {
                errorlogin.Text = "Veuillez remplir ce champ";
                verif = false;
            }
            else if (utilisateurData.GetFilteredUtilisateurs(login.Text).Count() != 0)
            {
                errorlogin.Text = "Login déjà utilisé";
                verif = false;
            }
            else
            {
                errorlogin.Text = "";
            }

            if ((mdp.Text == null)||(mdp.Text.Count() < 8))
            {
                errormdp.Text = "Le mot de passe doit être composé de au moins 8 caractères.";
                verif = false;
            }
            else
            {
                errormdp.Text = "";
            }

            if (remdp.Text != mdp.Text)
            {
                errorremdp.Text = "Les champs de mot de passe ne correspondent pas";
                verif = false;
            }
            else
            {
                errorremdp.Text = "";
            }

            return verif;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }
    }
}