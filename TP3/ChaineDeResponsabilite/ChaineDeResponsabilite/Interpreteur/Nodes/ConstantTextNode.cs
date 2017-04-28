using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaineDeResponsabilite.Commande;

namespace ChaineDeResponsabilite.Interpreteur.Nodes
{
    class ConstantTextNode : INode
    {

        private string m_Texte;
        private INode m_Next;

        public ConstantTextNode(string TextToMatch)
        {
            m_Texte = TextToMatch.ToUpper();
        }

        public INode Next
        {
            get
            {
                return m_Next;
            }

            set
            {
                m_Next = value;
            }
        }

        public bool Decoder(ref string Contexte)
        {
            if (Contexte.ToUpper().StartsWith(m_Texte))
            {
                if (Contexte.Length <= m_Texte.Length + 1)
                    Contexte = "";
                else
                    Contexte = Contexte.Substring(m_Texte.Length + 1);
                if (m_Next != null)
                    return m_Next.Decoder(ref Contexte);
                return true;
            }
            return false;
        }

        public void Interpreter(string Contexte, ref CommandeSquelette cmd)
        {
            m_Next?.Interpreter(Contexte, ref cmd);
            return;
        }
    }
}
