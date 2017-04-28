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
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, ItemIsReference = true)]
    public class Composante
    {
        [JsonProperty(IsReference = true, PropertyName = "Ingredient")]
        public Ingredient Ingredient;
        [JsonProperty]
        public int Quantite;

        public Composante(Ingredient i, int q)
        {
            this.Ingredient = i;
            this.Quantite = q;
        }
    }
}
