using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SQLite;
using TpXamarin.Model;
using Xamarin.Forms;

namespace TpXamarin
{
    public class AnnonceDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Annonce> Annonces { get; set; }

        public AnnonceDataAccess()
        {
            database =  DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Annonce>();
            Console.WriteLine("hoofjf");
            this.Annonces = new ObservableCollection<Annonce>(database.Table<Annonce>());

            if (!database.Table<Annonce>().Any())
            {
                addnewannonce();
            }
        }

        public void addnewannonce()
        {
            var i = SaveAnnonce(new Annonce
            {
                Titre = "Titre...",
                Description = "Description...",
                Prix = 500,
                Contact = "Numéro de téléphone...",
                Categorie = "Console & Jeux Vidéos",
                Date = new DateTime().ToString(),
                User = 0,
                UserName = "Nobody"
            });
            this.Annonces.Add(GetAnnonce(i));
        }

        public IEnumerable<Annonce> GetFilteredAnnonces(string Titre, int id)
        {
            lock(collisionLock)
            {
                var query = from annonce in database.Table<Annonce>()
                            where annonce.Titre.Contains(Titre) && annonce.User != id
                            select annonce;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Annonce> NotMyAnnonces(int id)
        {
            lock (collisionLock)
            {
                var query = from annonce in database.Table<Annonce>()
                            where annonce.User != id
                            select annonce;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Annonce> MyAnnonces(int id)
        {
            lock (collisionLock)
            {
                var query = from annonce in database.Table<Annonce>()
                            where annonce.User == id
                            select annonce;
                return query.AsEnumerable();
            }
        }

        public Annonce GetAnnonce(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Annonce>().FirstOrDefault(annonce => annonce.ID == id);
            }
        }

        public int SaveAnnonce(Annonce annonce)
        {
            lock (collisionLock)
            {
                if(annonce.ID != 0)
                {
                    database.Update(annonce);
                    return annonce.ID;
                }
                else
                {
                    database.Insert(annonce);
                    return annonce.ID;
                }
            }
        }

        public void SaveAllAnnonces()
        {
            lock (collisionLock)
            {
                foreach (var annonce in this.Annonces)
                {
                    if (annonce.ID != 0)
                    {
                        database.Update(annonce);
                    }
                    else
                    {
                        database.Insert(annonce);
                    }
                }
            }
        }

        public int DeleteAnnonce(Annonce annonce)
        {
            var id = annonce.ID;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Annonce>(id);
                }
            }
            this.Annonces.Remove(annonce);
            return id;
        }

        public void DeleteAllAnnonces()
        {
            lock (collisionLock)
            {
                database.DropTable<Annonce>();
                database.CreateTable<Annonce>();
            }
            this.Annonces = null;
            this.Annonces = new ObservableCollection<Annonce>(database.Table<Annonce>());
        }
    }
}
