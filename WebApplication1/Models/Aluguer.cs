﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Aluguer
    {
        public int IdAluguer { get; set; }
        public int IdArtigo { get; set; }
        public string IdUtilizador { get; set; }
        public double Preco { get; set; }
        public int Duracao { get; set; }
        public string IdRent { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Estado { get; set; }
        public int Quantidade { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Utilizador IdUtilizadorNavigation { get; set; }
        public bool Estado { get; set; }
        public int Quantidade { get; set; }
    }
}
