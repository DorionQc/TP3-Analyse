using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaineDeResponsabilite.Commande;

namespace ChaineDeResponsabilite.Interpreteur
{
    class Expression : IExpression, INode
    {
        // L'expression est composée de plusieurs "nodes", qui forment, eux aussi, une chaine de responsabilité
        private INode[] m_tNodes;
        private INode m_Next_INode;
        private IExpression m_Next_IExpression;
        private bool m_IsNode;
        /// <summary>
        /// Délégué donné pour créer la commande
        /// </summary>
        private CreateCommandeDelegate m_commandCreator;

        /// <summary>
        /// Construit en lui donnant, en ordre, ce qui le compose, parmis les différentes nodes.
        /// </summary>
        /// <param name="nodes"></param>
        public Expression(CreateCommandeDelegate del, bool IsNode, params INode[] nodes)
        {
            m_IsNode = IsNode;
            m_tNodes = nodes;
            m_Next_INode = null;
            m_commandCreator = del;

            if (m_tNodes.Length > 1)
            {
                int i = 1;
                while (i < m_tNodes.Length)
                {
                    // Crée la chaine de responsabilité
                    m_tNodes[i - 1].Next = m_tNodes[i];
                    i++;
                }
            }
        }

        IExpression IExpression.Next
        {
            get
            {
                return m_Next_IExpression;
            }

            set
            {
                m_Next_IExpression = value;
            }
        }

        INode INode.Next
        {
            get
            {
                return m_Next_INode;
            }

            set
            {
                m_Next_INode = value;
            }
        }

        public bool Decoder(ref string Contexte)
        {
            if (m_tNodes.Length > 0)
            {
                if (m_IsNode)
                {
                    if (m_tNodes[0].Decoder(ref Contexte))
                    {
                        if (m_Next_INode == null)
                            return true;
                        else
                            return m_Next_INode.Decoder(ref Contexte);
                    }
                    return false;
                }
                else
                    return m_tNodes[0].Decoder(ref Contexte);
            }
            return false;
        }

        ICommande IExpression.Interpreter(string Contexte, ref CommandeSquelette cmd)
        {
            string bck = string.Copy(Contexte);
            if (this.Decoder(ref Contexte))
            {
                if (m_tNodes.Length > 0)
                {
                    m_tNodes[0].Interpreter(Contexte, ref cmd);
                }
                return m_commandCreator(cmd);
            }
            else
            {
                Contexte = bck;
                if (m_IsNode == false)
                {
                    if (m_Next_IExpression == null)
                        return new CommandeVide();
                    else
                        return m_Next_IExpression.Interpreter(Contexte, ref cmd);
                }
                else
                    return new CommandeVide();
            }
        }

        void INode.Interpreter(string Contexte, ref CommandeSquelette cmd)
        {
            return; // inutile jusqu'à maintenant, peut éventuellement être utile pour imbriquer des commandes.
        }
    }
}
