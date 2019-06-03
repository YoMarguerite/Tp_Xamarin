using System.ComponentModel;
using SQLite;

namespace TpXamarin.Model
{
    public class Utilisateur
    {
        private int id;

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private string nom;

        [NotNull, MaxLength(50)]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }


        private string prenom;

        [NotNull, MaxLength(50)]
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }


        private string login;

        [NotNull, MaxLength(50)]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }


        private string mdp;

        [NotNull, MaxLength(50)]
        public string Mdp
        {
            get { return mdp; }
            set { mdp = value; }
        }
    }
}
