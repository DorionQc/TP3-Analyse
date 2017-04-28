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
using System.Windows.Forms;

using Newtonsoft.Json;

namespace ChaineDeResponsabilite
{
    [JsonObject(ItemIsReference = true)]
    public class Recette : IProduit
    {
        private List<Composante> m_lIngredients;
        private string m_Nom;
        [JsonProperty(PropertyName = "Next", IsReference = true)]
        private Recette m_Next;

        public Recette(string nom)
        {
            m_lIngredients = new List<Composante>();
            m_Nom = nom;
            m_Next = null;
        }

        [JsonProperty]
        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value; }
        }

        [JsonProperty(ItemIsReference = true)]
        public List<Composante> Ingredients
        {
            get { return m_lIngredients; }
            set { m_lIngredients = value; }
        }

        public void SetNext(Recette r)
        {
            m_Next = r;
        }

        public override string ToString()
        {
            return this.m_Nom;
        }

        public void AfficherDansLeListBoxSiLIngredientEstPresent(Ingredient Ingredient, ListBox lb)
        {
            for (int i = 0; i < m_lIngredients.Count; i++)
            {
                if (m_lIngredients[i].Ingredient == Ingredient)
                {
                    if (!lb.Items.Contains(this.ToString()))
                        lb.Items.Add(this.ToString());
                    break;
                }
            }

            m_Next?.AfficherDansLeListBoxSiLIngredientEstPresent(Ingredient, lb);
        }

        public Recette AjouterIngredient(Ingredient Ingredient, int Quantite)
        {
            for (int i = 0; i < m_lIngredients.Count; i++)
            {
                if (m_lIngredients[i].Ingredient == Ingredient)
                {
                    Composante c = m_lIngredients[i];
                    c.Quantite += Quantite;
                    m_lIngredients[i] = c;
                    return this; // Oui! Un return dans un for!
                }
            }
            m_lIngredients.Add(new Composante(Ingredient, Quantite));
            return this;
        }

        public Recette EnleverIngredient(Ingredient Ingredient)
        {
            for (int i = 0; i < m_lIngredients.Count; i++)
            {
                if (m_lIngredients[i].Ingredient == Ingredient)
                {
                    m_lIngredients.RemoveAt(i);
                    return this; // Oui! Un return dans un for!
                }
            }
            return this;
        }

        public Recette EnleverIngredient(Ingredient Ingredient, int Quantite)
        {
            for (int i = 0; i < m_lIngredients.Count; i++)
            {
                if (m_lIngredients[i].Ingredient == Ingredient)
                {
                    Composante c = m_lIngredients[i];
                    c.Quantite -= Quantite;
                    if (c.Quantite <= 0)
                        m_lIngredients.RemoveAt(i);
                    else
                        m_lIngredients[i] = c;
                    return this; // Oui! Un return dans un for!
                }
            }
            return this;
        }
    }
}
