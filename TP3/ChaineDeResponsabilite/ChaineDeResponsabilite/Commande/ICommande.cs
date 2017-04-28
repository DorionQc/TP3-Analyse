using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaineDeResponsabilite.Commande
{
    public interface ICommande
    {
        void Executer();
    }

    public interface IStringCommande : ICommande
    {

    }
}
