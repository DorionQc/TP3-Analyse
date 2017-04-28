using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ChaineDeResponsabilite.Commande;

namespace ChaineDeResponsabilite.Interpreteur.Nodes
{
    public class VariableTextNode : INode
    {
        private INode m_Next;
        private string m_Value;
        private string m_Filtre;
        private Role m_Role;

        public VariableTextNode(Role role, string Regex = null)
        {
            m_Role = role;
            m_Value = "";
            m_Filtre = Regex;
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
            if (Contexte.Length == 0)
                return false;
            Regex r = null;
            if (m_Filtre != null)
            {
                try
                {
                    r = new Regex(m_Filtre);
                }
                catch (Exception) { }

            }
            char stopChar = ' ';

            if (Contexte[0] == '"')
            {
                stopChar = '"';
                Contexte = Contexte.Substring(1);
            }
            
            m_Value = new string(Contexte.TakeWhile(c => c != stopChar).ToArray());
            if (r == null || (r != null && r.IsMatch(m_Value)))
            {
                int len = m_Value.Length + 1;
                if (stopChar == '"')
                    len++;
                if (Contexte.Length <= len)
                    Contexte = "";
                else
                    Contexte = Contexte.Substring(len);
                if (m_Next != null)
                    return m_Next.Decoder(ref Contexte);
                return true;
            }
            else
            {
                m_Value = "";
                return false;
            }
        }

        public void Interpreter(string Contexte, ref CommandeSquelette cmd)
        {
            switch (this.m_Role)
            {
                case Role.Nom:
                    cmd.NomProduit = this.m_Value;
                    break;
                case Role.NouveauNom:
                    cmd.NouveauNom = this.m_Value;
                    break;
                default:
                    break;
            }
            m_Next?.Interpreter(Contexte, ref cmd);
            return;
        }
    }
}
