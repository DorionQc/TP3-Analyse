using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaineDeResponsabilite.Commande
{
    public class CommandeVide : ICommande
    {
        public void Executer()
        {
            return;
        }
    }
}
