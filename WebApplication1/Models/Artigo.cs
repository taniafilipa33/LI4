﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Artigo
    {
        public Artigo()
        {
            Aluguer = new HashSet<Aluguer>();
            Venda = new HashSet<Venda>();
        }

        public int IdArtigo { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Modo { get; set; }
        public int Quantidade { get; set; }
        public string Categoria { get; set; }
        public string Etiquetas { get; set; }
        public bool Estado { get; set; }
        public string IdDono { get; set; }
        

        public virtual Utilizador IdDonoNavigation { get; set; }
        public virtual ICollection<Aluguer> Aluguer { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
        
    }
}
