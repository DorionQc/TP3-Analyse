/**************************************
 * Samuel Goulet
 * Mars 2017
 * TP3 - Chaine de responsabilités
 *************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChaineDeResponsabilite.Interpreteur.Nodes;
using ChaineDeResponsabilite.Commande;
using ChaineDeResponsabilite.Interpreteur;

namespace ChaineDeResponsabilite
{
    public partial class Form1 : Form
    {

        BaseDeDonnees m_bd;

        public Form1()
        {
            InitializeComponent();

            
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeListerRecette(); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Liste")));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeListerIngredient(); }, false, new ConstantTextNode("Ingredient"), new ConstantTextNode("Liste")));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeAfficherRecette(cmd.NomProduit); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Afficher"), new VariableTextNode(Role.Nom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeEnleverRecette(cmd.NomProduit); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Enlever"), new VariableTextNode(Role.Nom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeRenommerRecette(cmd.NomProduit, cmd.NouveauNom); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Renommer"), new VariableTextNode(Role.Nom), new VariableTextNode(Role.NouveauNom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeAjouterIngredient(cmd.NomProduit); }, false, new ConstantTextNode("Ingredient"), new ConstantTextNode("Ajouter"), new VariableTextNode(Role.Nom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeEnleverIngredient(cmd.NomProduit); }, false, new ConstantTextNode("Ingredient"), new ConstantTextNode("Enlever"), new VariableTextNode(Role.Nom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeRenommerIngredient(cmd.NomProduit, cmd.NouveauNom); }, false, new ConstantTextNode("Ingredient"), new ConstantTextNode("Renommer"), new VariableTextNode(Role.Nom), new VariableTextNode(Role.NouveauNom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeAjouterRecette(cmd.NomProduit); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Ajouter"), new VariableTextNode(Role.Nom)));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeModifierRecette(cmd.NomProduit, cmd.NomIngredient, (cmd.QuantiteIngredient == null ? 0 : (int)cmd.QuantiteIngredient)); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Modifier"), new VariableTextNode(Role.Nom), new ConstantTextNode("Ajouter"), new DictionaryNode<string, int>()));
            Interpreteur.Interpreteur.Register(new Expression(delegate (CommandeSquelette cmd) { return new CommandeModifierRecette(cmd.NomProduit, cmd.NomIngredient, (cmd.QuantiteIngredient == null ? 0 : -(int)cmd.QuantiteIngredient)); }, false, new ConstantTextNode("Recette"), new ConstantTextNode("Modifier"), new VariableTextNode(Role.Nom), new ConstantTextNode("Enlever"), new DictionaryNode<string, int>()));

            // Création d'un inventaire bâtard
            m_bd = BaseDeDonnees.getInstance();
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            string nom = txtIngredient.Text;
            int i = 0;
            List<Ingredient> Ingredients = m_bd.getIngredients();
            List <Ingredient> ing = new List<Ingredient>();
            while (i < Ingredients.Count)
            {
                if (Ingredients[i].Nom.ToUpper().Contains(nom.ToUpper()))
                    ing.Add(Ingredients[i]);
                i++;
            }
            if (ing.Count == 0)
            {
                MessageBox.Show("L'ingrédient donné n'est pas un ingrédient :(");
                return;
            }
            lbRecettes.Items.Clear();
            foreach (Ingredient ingredient in ing)
            {
                m_bd.AfficherDansListBox(ingredient, lbRecettes);
            }
        }

        private void btnVider_Click(object sender, EventArgs e)
        {
            lbRecettes.Items.Clear();
        }

        private void btnCommande_Click(object sender, EventArgs e)
        {
            ICommande cmd = Interpreteur.Interpreteur.Interpreter(txtCommande.Text);
            cmd.Executer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_bd.Save();
        }
    }
}
