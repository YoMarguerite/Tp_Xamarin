using System.ComponentModel;
using SQLite;

namespace TpXamarin.Model
{
    [Table("Annonces")]
    public class Annonce
    {

        private int id;

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private string titre;

        [MaxLength(50), NotNull]
        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }


        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        private double prix;
        
        [NotNull]
        public double Prix
        {
            get { return prix; }
            set { prix = value; }
        }


        private string contact;

        [NotNull]
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }


        private string categorie;

        [NotNull]
        public string Categorie
        {
            get { return categorie; }
            set { categorie = value; }
        }


        private string date;

        [NotNull]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }


        private int user;

        [NotNull]
        public int User
        {
            get { return user; }
            set { user = value; }
        }

        private string username;

        [NotNull]
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
    }
}
