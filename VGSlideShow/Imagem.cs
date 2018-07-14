using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGSlideShow
{
    class Imagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string Icone { get; set; }
        public string Extensao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
