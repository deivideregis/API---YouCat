using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models
{
    public class Estudos : Entity
    {
        public int NumeroYouCat { get; set; }
        public string Descricao { get; set; }

        public string Pergunta { get; set; }

        public string Resposta { get; set; }

        public string Explicacao { get; set; }

        public DateTime DataCadastro { get; set; }

        /* EF Relation */
        public IEnumerable<TipoEstudo> TipoEstudo { get; set; }
    }
}
