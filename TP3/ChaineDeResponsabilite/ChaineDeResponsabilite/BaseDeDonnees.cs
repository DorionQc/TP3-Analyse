/***********************************
 * Samuel Goulet
 * Mars / Avril 2017
 * TP3 - Singleton
 ***********************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ChaineDeResponsabilite
{
    public enum BD_Code_Erreur : byte
    {
        AllGood,
        IngredientNotFound,
        RecetteNotFound,
        IngredientExisteDeja,
        RecetteExisteDeja,
    };
    public class BaseDeDonnees
    {

        private static BaseDeDonnees __instance;

        private static string __FileName = "BD.json";

        private Inventaire m_inv;

        private BaseDeDonnees()
        {
            FileStream fs = new FileStream(__FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamReader sr = new StreamReader(fs);
            string Json = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            m_inv = JsonConvert.DeserializeObject<Inventaire>(Json, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.All });
            
            
        }

        /// <summary>
        /// Donne la liste de tous les ingrédients dans la BD
        /// </summary>
        /// <returns>Une liste contenant les ingrédients de la BD</returns>
        public List<Ingredient> getIngredients()
        {
            return m_inv.prop_ing;
        }

        /// <summary>
        /// Donne la liste de toutes les recettes dans la BD
        /// </summary>
        /// <returns>Une liste contenant les recettes de la BD</returns>
        public List<Recette> getRecettes()
        {
            return m_inv.prop_rec;
        }


        /// <summary>
        /// Donne l'ingrédient au nom spécifié
        /// </summary>
        /// <param name="Nom">Le nom de l'ingrédient</param>
        /// <returns>Un ingrédient, ou null si aucun match</returns>
        public Ingredient getIngredient(string Nom)
        {
            return m_inv.method_ing0_geta(Nom);
        }

        /// <summary>
        /// Ajoute un ingrédient à la base de données
        /// </summary>
        /// <param name="Nom">Le nom de l'ingrédient</param>
        /// <returns>AllGood || IngredientExisteDeja</returns>
        public BD_Code_Erreur AjouterIngredient(string Nom)
        {
            return m_inv.method_ing1_add(Nom);
        }

        /// <summary>
        /// Supprime un ingrédient de la base de données
        /// </summary>
        /// <param name="Nom">Le nom de l'ingrédient</param>
        /// <returns>AllGood || IngredientNotFound</returns>
        public BD_Code_Erreur EnleverIngredient(string Nom)
        {
            return m_inv.method_ing2_rem(Nom);
        }

        /// <summary>
        /// Renomme un ingrédient de la base de données
        /// </summary>
        /// <param name="AncienNom">Le nom de l'ingrédient à renommer</param>
        /// <param name="NouveauNom">Le nouveau nom de l'ingrédient</param>
        /// <returns>AllGood || IngredientNotFound || IngredientExisteDeja</returns>
        public BD_Code_Erreur RenommerIngredient(string AncienNom, string NouveauNom)
        {
            return m_inv.method_ing3_rnam(AncienNom, NouveauNom);
        }


        /// <summary>
        /// Donne la recette au nom spécifié
        /// </summary>
        /// <param name="Nom">Le nom de la recette</param>
        /// <returns>Un recette, ou null si aucun match</returns>
        public Recette getRecette(string Nom)
        {
            return m_inv.method_rec0_geta(Nom);
        }

        /// <summary>
        /// Ajoute une recette à la base de données
        /// </summary>
        /// <param name="Nom">Le nom de la recette</param>
        /// <returns>AllGood || RecetteExisteDeja</returns>
        public BD_Code_Erreur AjouterRecette(Recette r)
        {
            return m_inv.method_rec1_add(r);
        }

        /// <summary>
        /// Supprime une recette de la base de données
        /// </summary>
        /// <param name="Nom">Le nom de la recette</param>
        /// <returns>AllGood || RecetteNotFound</returns>
        public BD_Code_Erreur EnleverRecette(string Nom)
        {
            return m_inv.method_rec2_rem(Nom);
        }

        /// <summary>
        /// Renomme une recette de la base de données
        /// </summary>
        /// <param name="AncienNom">Le nom de la recette à renommer</param>
        /// <param name="NouveauNom">Le nouveau nom de la recette</param>
        /// <returns>AllGood || RecetteNotFound || RecetteExisteDeja</returns>
        public BD_Code_Erreur RenommerRecette(string AncienNom, string NouveauNom)
        {
            return m_inv.method_rec3_rnam(AncienNom, NouveauNom);
        }

        /// <summary>
        /// Ajoute (ou enlève) une certaine quantité d'un ingrédient dans une recette
        /// </summary>
        /// <param name="Recette">Nom de la recette à modifier</param>
        /// <param name="Ingredient">Nom de l'ingrédient à ajouter ou enlever</param>
        /// <param name="Quantite">Nombre positif pour ajouter, négatif pour enlever, ou 0 pour supprimer l'ingrédient de la recette</param>
        /// <returns>AllGood || RecetteNotFound || IngredientNotFound</returns>
        public BD_Code_Erreur ModifierRecette(string Recette, string Ingredient, int Quantite)
        {
            return m_inv.method_rec4_mod(Recette, Ingredient, Quantite);
        }



        public static BaseDeDonnees getInstance()
        {
            return __instance ?? (__instance = new BaseDeDonnees());
        }

        public void AfficherDansListBox(Ingredient i, ListBox lb)
        {
            m_inv.method_showall(i, lb);
        }

        public void Save()
        {
            File.WriteAllText(__FileName, JsonConvert.SerializeObject(m_inv, Formatting.Indented));
        }
        
















        [JsonObject(ItemIsReference = true)]
        private class Inventaire
        {
            List<Ingredient> m_lIngredients;
            List<Recette> m_lRecettes;

            public Inventaire()
            {
                
            }

            [JsonProperty(ItemIsReference = true, Order = 0, PropertyName = "Ingredients")]
            public List<Ingredient> prop_ing
            {
                get { return m_lIngredients; }
                set { m_lIngredients = value; }
            }

            [JsonProperty(ItemIsReference = true, Order = 1, PropertyName = "Recettes")]
            public List<Recette> prop_rec
            {
                get { return m_lRecettes; }
                set { m_lRecettes = value; }
            }

            [JsonProperty(PropertyName = "Premier", IsReference = true, Order = 2)]
            Recette m_PremiereRecette;
            [JsonProperty(PropertyName = "Dernier", IsReference = true, Order = 3)]
            Recette m_DerniereRecette;


            //
            // Ingrédients
            //


            public Ingredient method_ing0_geta(string Nom)
            {
                if (m_lIngredients.Count == 0)
                    return null;
                return m_lIngredients.Where(ing => ing.Nom == Nom).FirstOrDefault();
            }

            public BD_Code_Erreur method_ing1_add(string Nom)
            {
                // Vérifier que l'ingrédient n'existe pas déjà
                Ingredient i = method_ing0_geta(Nom);
                if (i == null)
                {
                    m_lIngredients.Add(new Ingredient(Nom));
                    return BD_Code_Erreur.AllGood;
                }
                return BD_Code_Erreur.IngredientExisteDeja;
            }

            public BD_Code_Erreur method_ing2_rem(string Nom)
            {
                Ingredient i = method_ing0_geta(Nom);
                if (i == null)
                    return BD_Code_Erreur.IngredientNotFound;

                foreach (Recette r in m_lRecettes)
                    foreach (Composante cmp in r.Ingredients)
                        if (cmp.Ingredient == i)
                            r.Ingredients.Remove(cmp);
                m_lIngredients.Remove(i);
                return BD_Code_Erreur.AllGood;
            }

            public BD_Code_Erreur method_ing3_rnam(string Ancien, string Nouveau)
            {
                Ingredient toRename = method_ing0_geta(Ancien);
                if (toRename == null)
                    return BD_Code_Erreur.IngredientNotFound;

                Ingredient nvIng = method_ing0_geta(Nouveau);
                if (nvIng == null)
                {
                    toRename.Nom = Nouveau;
                    return BD_Code_Erreur.AllGood;
                }
                return BD_Code_Erreur.IngredientExisteDeja;
            }

            //
            // Recettes
            //


            public Recette method_rec0_geta(string Nom)
            {
                if (m_lRecettes.Count == 0)
                    return null;
                return m_lRecettes.Where(rec => rec.Nom == Nom).FirstOrDefault();
            }

            public BD_Code_Erreur method_rec1_add(Recette r)
            {
                Recette ck = method_rec0_geta(r.Nom);
                if (ck != null)
                    return BD_Code_Erreur.RecetteExisteDeja;

                if (m_PremiereRecette == null)
                    m_PremiereRecette = r;
                if (m_DerniereRecette != null)
                    m_DerniereRecette.SetNext(r);
                m_DerniereRecette = r;
                m_lRecettes.Add(r);
                return BD_Code_Erreur.AllGood;
            }

            public BD_Code_Erreur method_rec2_rem(string Nom)
            {
                Recette ck = method_rec0_geta(Nom);
                if (ck == null)
                    return BD_Code_Erreur.RecetteNotFound;
                m_lRecettes.Remove(ck);
                return BD_Code_Erreur.AllGood;
            }

            public BD_Code_Erreur method_rec3_rnam(string Ancien, string Nouveau)
            {
                Recette toRename = method_rec0_geta(Ancien);
                if (toRename == null)
                    return BD_Code_Erreur.RecetteNotFound;

                // Si la recette existe déjà, on l'utilise. Sinon, on en crée une nouvelle
                Recette nvRecette = method_rec0_geta(Nouveau);
                if (nvRecette == null)
                {
                    toRename.Nom = Nouveau;
                    return BD_Code_Erreur.AllGood;
                }
                return BD_Code_Erreur.RecetteExisteDeja;
            }

            /// <summary>
            /// Ajoute (ou enlève) une certaine quantité d'un ingrédient dans une recette
            /// </summary>
            /// <param name="Recette">Nom de la recette à modifier</param>
            /// <param name="Ingredient">Nom de l'ingrédient à ajouter ou enlever</param>
            /// <param name="Quantite">Nombre positif pour ajouter, négatif pour enlever, ou 0 pour supprimer l'ingrédient de la recette</param>
            public BD_Code_Erreur method_rec4_mod(string Recette, string Ingredient, int Quantite)
            {
                Recette r = method_rec0_geta(Recette);
                if (r == null)
                    return BD_Code_Erreur.RecetteNotFound;

                Ingredient ing = method_ing0_geta(Ingredient);
                if (ing == null)
                    return BD_Code_Erreur.IngredientNotFound;

                Composante cmp = null;
                int i = 0;
                while (i < r.Ingredients.Count && cmp == null)
                {
                    if (r.Ingredients[i].Ingredient == ing)
                        cmp = r.Ingredients[i];
                    i++;
                }

                if (cmp == null) // La recette ne contient pas l'ingrédient demandé
                {
                    if (Quantite > 0) // mais on désire l'ajouter à la recette
                    {
                        r.AjouterIngredient(ing, Quantite);
                        return BD_Code_Erreur.AllGood;
                    }
                    return BD_Code_Erreur.IngredientNotFound;
                    // On ne peut rien faire d'autre si l'ingrédient n'était pas dans la recette originalement
                }
                else
                {
                    // La classe Recette s'occupe de tout
                    if (Quantite > 0)
                    {
                        r.AjouterIngredient(cmp.Ingredient, cmp.Quantite);
                    }
                    else if (Quantite == 0)
                    {
                        r.EnleverIngredient(cmp.Ingredient);
                    }
                    else
                    {
                        r.EnleverIngredient(cmp.Ingredient, cmp.Quantite);
                    }
                    return BD_Code_Erreur.AllGood;
                }
            }

            public void method_showall(Ingredient i, ListBox lb)
            {
                m_PremiereRecette?.AfficherDansLeListBoxSiLIngredientEstPresent(i, lb);
            }
        }
    }
}
