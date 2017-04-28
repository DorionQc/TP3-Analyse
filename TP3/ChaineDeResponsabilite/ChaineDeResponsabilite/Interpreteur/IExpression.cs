using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChaineDeResponsabilite.Commande;

namespace ChaineDeResponsabilite.Interpreteur
{
    public delegate ICommande CreateCommandeDelegate(CommandeSquelette cmd);
    public interface IExpression
    {
        ICommande Interpreter(string Contexte, ref CommandeSquelette cmd);
        IExpression Next { get; set; }

    }

    public interface INode
    {
        void Interpreter(string Contexte, ref CommandeSquelette cmd);
        INode Next { get; set; }
        bool Decoder(ref string Contexte);
    }
}
