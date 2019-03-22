using System;
using System.Collections.Generic;
using System.Text;

namespace TpXamarin.Model
{
    public class Annonce
    {
        public int Id { get; set; }

        public string Titre { get; set; }

        public string Description { get; set; }

        public double Prix { get; set; }

        public string Contact { get; set; }

        public string Categorie { get; set; }

        public string Date { get; set; }
    }
}
