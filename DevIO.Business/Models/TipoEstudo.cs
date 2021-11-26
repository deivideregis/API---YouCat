using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models
{
    public class TipoEstudo : Entity
    {
        public Guid EstudoId { get; set; }

        public string Descricao { get; set; }

        /* EF Relation */
        public Estudos Estudos { get; set; }
    }
}
