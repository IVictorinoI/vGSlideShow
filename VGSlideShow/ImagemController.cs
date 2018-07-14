using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VGSlideShow
{
    class ImagemController
    {
        public List<Imagem> Lista { get; set; }
        public List<Imagem> NovasImagens { get; set; }

        public ImagemController()
        {
            Lista = new List<Imagem>();
            NovasImagens = new List<Imagem>();
        }

        public List<Imagem> GetFromDirectory(DirectoryInfo dir)
        {
            List<Imagem> listaRetorno = new List<Imagem>();
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension.ToUpper().Equals(".JPG"))
                {
                    var listaFiltrada = Lista.Where(c => c.Nome.ToUpper() == file.Name.ToUpper());
                    if (listaFiltrada.Count()==0)
                    {
                        Imagem imagem = new Imagem { Caminho = file.FullName, Nome = file.Name, Extensao = file.Extension };
                        listaRetorno.Add(imagem);
                    }                    
                }
            }
            return listaRetorno;
        }

        public void AddImagens(List<Imagem> imagens)
        {
            Lista.AddRange(imagens);
        }

        public void AddNewImages(List<Imagem> imagens)
        {
            NovasImagens.AddRange(imagens);
        //    System.IO.FileSystemEventHandler
        //    System.IO.FileSystemEventArgs
        }


        public List<Imagem> GetFromPath(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return this.GetFromDirectory(dir);
        }
    }
}
