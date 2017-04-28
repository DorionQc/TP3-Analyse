/**************************************
 * Samuel Goulet
 * Mars 2017
 * TP3 - Chaine de responsabilités
 *************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ChaineDeResponsabilite
{
    [JsonObject(IsReference = true)]
    public class Ingredient : IProduit
    {
        private string m_Nom;
        
        public Ingredient(string Nom)
        {
            m_Nom = Nom;
        }

        [JsonProperty]
        public string Nom
        {
            get { return m_Nom; }
            set { if (value != null) m_Nom = value; }
        }

        public override string ToString()
        {
            return this.m_Nom;
        }
    }
}
