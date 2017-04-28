using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChaineDeResponsabilite.Commande;

namespace ChaineDeResponsabilite.Interpreteur
{
    /// <summary>
    /// Utilisé pour simplifier l'appel des interpreteurs
    /// </summary>
    public static class Interpreteur
    {

        private static List<IExpression> s_RegisteredExpressions;

        /// <summary>
        /// Constructeur statique, appelable de partout
        /// </summary>
        static Interpreteur()
        {
            s_RegisteredExpressions = new List<IExpression>();
        }

        public static ICommande Interpreter(string Contexte)
        {
            CommandeSquelette cmd = new CommandeSquelette();
            return Interpreter(Contexte, ref cmd);
        }


        public static ICommande Interpreter(string Contexte, ref CommandeSquelette cmd)
        {
            if (s_RegisteredExpressions.Count > 0)
                return s_RegisteredExpressions[0].Interpreter(Contexte, ref cmd);
            return new CommandeVide();
        }

        /// <summary>
        /// Ajoute l'expression à la liste vérifiée, qui forme elle-même une chaine de responsabilité
        /// </summary>
        /// <param name="exp"></param>
        public static void Register(IExpression exp)
        {
            s_RegisteredExpressions.Add(exp);
            if (s_RegisteredExpressions.Count > 1)
            {
                // Construit la chaine de responsabilités
                s_RegisteredExpressions[s_RegisteredExpressions.Count - 2].Next = exp;
            }
        }
    }
}
