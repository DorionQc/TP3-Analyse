using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ChaineDeResponsabilite.Commande;

namespace ChaineDeResponsabilite.Interpreteur.Nodes
{
    public class DictionaryNode<TKey, TValue> : INode
    {
        private INode m_Next;
        private TKey m_Key;
        private TValue m_Value;

        private static Regex s_Regex = new Regex(@"^((""[a-zA-Z0-9 ]+?"")|([a-zA-Z0-9]+?)):[0-9]*");

        public DictionaryNode()
        {

        }

        private bool FromString<T>(string Value, out T ValConvert)
        {
            try
            {
                ValConvert = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(Value);
            }
            catch(Exception)
            {
                ValConvert = default(T);
                return false;
            }
            return true;
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
            Match m = s_Regex.Match(Contexte);
            if (m.Success)
            {
                string[] Val = m.Value.Split(':');
                Val[0] = Val[0].Trim('"', ' ');
                TKey Key;
                TValue Value;
                if (FromString<TKey>(Val[0], out Key) && FromString<TValue>(Val[1], out Value))
                {
                    m_Key = Key;
                    m_Value = Value;
                    if (Contexte.Length <= m.Value.Length + 1)
                        Contexte = "";
                    else
                        Contexte = Contexte.Substring(m.Value.Length + 1);
                    if (m_Next != null)
                        return m_Next.Decoder(ref Contexte);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void Interpreter(string Contexte, ref CommandeSquelette cmd)
        {
            // Malheureusement, un peu de code fermé.
            if (this.m_Key is string && this.m_Value is int)
            {
                cmd.NomIngredient = this.m_Key as string;
                cmd.QuantiteIngredient = this.m_Value as int?;
            }
            m_Next?.Interpreter(Contexte, ref cmd);
            return;
        }
    }
}
