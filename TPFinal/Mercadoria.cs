using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPFinal
{
    public class Mercadoria
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeMercadoria { get; set; }
        public string Peso { get; set; }
        public string NCM { get; set; }
        public string NomeProdutor { get; set; }
        public string ProdutorEmail { get; set; }
    }
}
