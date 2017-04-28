using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Forms;

namespace ChaineDeResponsabilite.Commande
{
    public class CommandeAjouterRecette : ICommande
    {
        private string Nom;
        //private Composante[] Ingredients;

        public CommandeAjouterRecette(string Nom/*, params Composante[] Ingredient*/)
        {
            this.Nom = Nom;
            //this.Ingredients = Ingredient;
        }

        public void Executer()
        {
            Recette r = new Recette(Nom);
            //foreach (Composante cmp in Ingredients)
            //    r.AjouterIngredient(cmp.Ingredient, cmp.Quantite);
            BD_Code_Erreur err = BaseDeDonnees.getInstance().AjouterRecette(r);
            if (err == BD_Code_Erreur.RecetteExisteDeja)
                MessageBox.Show("La recette " + Nom + " existe déjà.");
            else
                MessageBox.Show("Ajout de la recette \"" + Nom + "\" dans la base de données");
            
        }
    }

    public class CommandeEnleverRecette : ICommande
    {
        private string Nom;

        public CommandeEnleverRecette(string Nom)
        {
            this.Nom = Nom;
        }

        public void Executer()
        {
            BD_Code_Erreur err = BaseDeDonnees.getInstance().EnleverRecette(Nom);
            if (err == BD_Code_Erreur.RecetteNotFound)
                MessageBox.Show("La recette " + Nom + " est introuvable");
            else
                MessageBox.Show("Suppression de la recette \"" + Nom + "\" dans la base de données");
        }
    }

    public class CommandeRenommerRecette : ICommande
    {
        private string Nom;
        private string NouveauNom;

        public CommandeRenommerRecette(string AncienNom, string NvNom)
        {
            this.Nom = AncienNom;
            this.NouveauNom = NvNom;
        }

        public void Executer()
        {
            BD_Code_Erreur err = BaseDeDonnees.getInstance().RenommerRecette(Nom, NouveauNom);
            if (err == BD_Code_Erreur.RecetteNotFound)
                MessageBox.Show("La recette " + Nom + " est introuvable");
            else if (err == BD_Code_Erreur.RecetteExisteDeja)
                MessageBox.Show("La recette " + NouveauNom + " existe déjà");
            else
                MessageBox.Show("Renomage de la recette \"" + Nom + "\" vers \"" + NouveauNom + "\"");
        }
    }

    public class CommandeModifierRecette : ICommande
    {
        private string Recette;
        private string Ingredient;
        private int Quantite;

        public CommandeModifierRecette(string Recette, string Ingredient, int Quantite)
        {
            this.Recette = Recette;
            this.Ingredient = Ingredient;
            this.Quantite = Quantite;
        }

        public void Executer()
        {
            BD_Code_Erreur err = BaseDeDonnees.getInstance().ModifierRecette(this.Recette, this.Ingredient, this.Quantite);
            if (err == BD_Code_Erreur.RecetteNotFound)
                MessageBox.Show("La recette " + Recette + " est introuvable");
            else if (err == BD_Code_Erreur.IngredientNotFound)
                MessageBox.Show("L'ingrédient " + Ingredient + " est introuvable");
            else
                MessageBox.Show("Modification de la recette \"" + Recette + "\" dans la base de données, pour y " + (Quantite > 0 ? "Ajouter" : "Enlever") + " " + (Quantite == 0 ? " tout" : Quantite.ToString()) + " le(s) " + Ingredient);
        }
    }



    public class CommandeListerRecette : ICommande
    {
        public CommandeListerRecette() { }

        public void Executer()
        {
            string s = "";
            foreach (Recette rec in BaseDeDonnees.getInstance().getRecettes())
            {
                s += rec.Nom + "\n";
            }
            MessageBox.Show(s);
        }
    }

    public class CommandeAfficherRecette : ICommande
    {
        private string m_Nom;
        public CommandeAfficherRecette(string Nom)
        {
            m_Nom = Nom;
        }

        public void Executer()
        {
            Recette r = BaseDeDonnees.getInstance().getRecette(m_Nom);
            if (r != null)
            {
                string s = r.ToString() + '\n';
                foreach (Composante c in r.Ingredients)
                {
                    s += c.Ingredient.ToString() + " : " + c.Quantite.ToString() + '\n';
                }
                MessageBox.Show(s);
            }
            else
                MessageBox.Show("Recette introuvable");
        }
    }
}
