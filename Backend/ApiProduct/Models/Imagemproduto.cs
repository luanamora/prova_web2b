using System;
using System.Collections.Generic;

namespace ApiProduct.Models
{
    public partial class Imagemproduto
    {
        public int CdImagem { get; set; }
        public string DsLink { get; set; }
        public int? CdProduto { get; set; }

        public Produto CdProdutoNavigation { get; set; }
    }
}
