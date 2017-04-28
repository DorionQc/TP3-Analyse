using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaineDeResponsabilite.Commande
{
    public class CommandeAjouterIngredient : ICommande
    {
        private string Nom;

        public CommandeAjouterIngredient(string Nom)
        {
            this.Nom = Nom;
        }

        public void Executer()
        {
            BD_Code_Erreur err = BaseDeDonnees.getInstance().AjouterIngredient(Nom);
            if (err == BD_Code_Erreur.IngredientExisteDeja)
                MessageBox.Show("L'ingrédient " + Nom + " existe déjà.");
            else
                MessageBox.Show("Ajout de l'ingrédient \"" + Nom + "\" dans la base de données");
        }
    }

    public class CommandeEnleverIngredient : ICommande
    {
        private string Nom;

        public CommandeEnleverIngredient(string Nom)
        {
            this.Nom = Nom;
        }

        public void Executer()
        {
            BD_Code_Erreur err = BaseDeDonnees.getInstance().EnleverIngredient(Nom);
            if (err == BD_Code_Erreur.IngredientNotFound)
                MessageBox.Show("L'ingrédient " + Nom + " est introuvable");
            else
                MessageBox.Show("Suppression de l'ingrédient \"" + Nom + "\" dans la base de données");
        }
    }

    public class CommandeRenommerIngredient : ICommande
    {
        private string Nom;
        private string NouveauNom;

        public CommandeRenommerIngredient(string AncienNom, string NvNom)
        {
            this.Nom = AncienNom;
            this.NouveauNom = NvNom;
        }

        public void Executer()
        {
            BD_Code_Erreur err = BaseDeDonnees.getInstance().RenommerIngredient(Nom, NouveauNom);
            if (err == BD_Code_Erreur.IngredientNotFound)
                MessageBox.Show("L'ingrédient " + Nom + " est introuvable");
            else if (err == BD_Code_Erreur.IngredientExisteDeja)
                MessageBox.Show("L'ingrédient " + NouveauNom + " existe déjà");
            else
                MessageBox.Show("Renomage de l'ingrédient \"" + Nom + "\", vers \"" + NouveauNom + "\"");
        }
    }

    public class CommandeListerIngredient : ICommande
    {
        public CommandeListerIngredient() { }

        public void Executer()
        {
            string s = "";
            foreach (Ingredient ing in BaseDeDonnees.getInstance().getIngredients())
            {
                s += ing.Nom + "\n";
            }
            MessageBox.Show(s);
        }
    }
}
