using System.Linq;
using System.Collections.Generic;
using SQLite;
using TpXamarin.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using System.Text;
using System.Security.Cryptography;

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

        public IEnumerable<Utilisateur> GetUtilisateurs()
        {
            lock (collisionLock)
            {
                var query = from utilisateur in database.Table<Utilisateur>()
                            select utilisateur;
                return query.AsEnumerable();
            }
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
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, mdp);

                    var query = from Utilisateur in database.Table<Utilisateur>()
                                where Utilisateur.Login == login && Utilisateur.Mdp == hash
                                select Utilisateur;


                    if (!query.Any())
                    {
                        return null;
                    }

                    return (query.First());
                }
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

        public string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
