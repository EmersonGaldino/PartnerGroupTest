using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerGroup.Domain.Models
{
    public class Patrimonio : Entity
    {
        
        public string Nome { get; set; }
        public int MarcaId { get; set; }
        public string Descricao { get; set; }
        public int NrTombo { get; private set; }

        public Patrimonio() { }

        public Patrimonio(int nrTombo)
        {
            this.NrTombo = nrTombo;
        }


    }
}
