using System;
using System.Collections.Generic;
using System.Text;

namespace TpXamarin.Model
{
    public class Utilisateur
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Login { get; set; }

        public string Mdp{ get; set; }
    }
}
