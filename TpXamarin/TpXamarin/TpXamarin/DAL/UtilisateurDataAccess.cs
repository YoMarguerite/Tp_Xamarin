using System.Linq;
using System.Collections.Generic;
using SQLite;
using TpXamarin.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;

namespace TpXamarin.DAL
{
    public class UtilisateurDataAccess
    {

        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Utilisateur> Utilisateurs { get; set; }

        public UtilisateurDataAccess()
        {
            database =
                DependencyService.Get<IDatabaseConnection>().
                DbConnection();
            database.CreateTable<Utilisateur>();

            this.Utilisateurs =
                new ObservableCollection<Utilisateur>(database.Table<Utilisateur>());
            
        }

        public IEnumerable<Utilisateur> GetFilteredUtilisateurs(string Login)
        {
            lock (collisionLock)
            {
                var query = from utilisateur in database.Table<Utilisateur>()
                            where utilisateur.Login == Login
                            select utilisateur;
                return query.AsEnumerable();
            }
        }

        public Utilisateur ConnexionUtilisateur(string login, string mdp)
        {
            lock (collisionLock)
            {
                Console.WriteLine("start");
                var query = from Utilisateur in database.Table<Utilisateur>()
                            where Utilisateur.Login == login && Utilisateur.Mdp == mdp
                            select Utilisateur;
                Console.WriteLine(query);
                if (!query.Any())
                {
                    Console.WriteLine("yes");
                    return null;
                }
                Console.WriteLine("hop");
                return (query.First());
            }
        }

        public Utilisateur GetUtilisateur(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Utilisateur>().FirstOrDefault(Utilisateur => Utilisateur.ID == id);
            }
        }

        public int SaveUtilisateur(Utilisateur Utilisateur)
        {
            lock (collisionLock)
            {
                if (Utilisateur.ID != 0)
                {
                    database.Update(Utilisateur);
                    return Utilisateur.ID;
                }
                else
                {
                    database.Insert(Utilisateur);
                    return Utilisateur.ID;
                }
            }
        }

        public void SaveAllUtilisateurs()
        {
            lock (collisionLock)
            {
                foreach (var Utilisateur in this.Utilisateurs)
                {
                    if (Utilisateur.ID != 0)
                    {
                        database.Update(Utilisateur);
                    }
                    else
                    {
                        database.Insert(Utilisateur);
                    }
                }
            }
        }

        public int DeleteUtilisateur(Utilisateur Utilisateur)
        {
            var id = Utilisateur.ID;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Utilisateur>(id);
                }
            }
            this.Utilisateurs.Remove(Utilisateur);
            return id;
        }

        public void DeleteAllUtilisateurs()
        {
            lock (collisionLock)
            {
                database.DropTable<Utilisateur>();
                database.CreateTable<Utilisateur>();
            }
            this.Utilisateurs = null;
            this.Utilisateurs = new ObservableCollection<Utilisateur>(database.Table<Utilisateur>());
        }
    }
}
