using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VGSlideShow
{
    public partial class Form1 : Form
    {

        FileSystemWatcher watcher = new FileSystemWatcher();
        ImagemController imagemController = new ImagemController();
        String PATH = @"C:\xampp\htdocs\VGFileTransfer\server\imagem";
        FormImagem formImagem;

        public Form1()
        {
            InitializeComponent();
            this.StartWacher();
            textBoxDiretorio.Text = PATH;
            
        }

        private void StartWacher()
        {
            this.watcher.Path = PATH;
            this.watcher.NotifyFilter = NotifyFilters.LastAccess |
                                        NotifyFilters.LastWrite |
                                        NotifyFilters.FileName |
                                        NotifyFilters.DirectoryName;
            this.watcher.Filter = "*.jpg";
            this.watcher.Created += Watcher_Created;
            this.watcher.Deleted += Watcher_Deleted; 
            this.watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath);
            imagemController.NovasImagens.Add(new Imagem { Caminho = e.FullPath, Nome = e.Name });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            this.getAllImagens();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            this.loadImagem(listBox1.SelectedItem.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                        
            if (listBox1.Items.Count == 0)
                return;

            //this.checkFromNewImages();

            if (imagemController.NovasImagens.Count > 0)
            {
                this.showNewImages();
                return;
            }

            this.proximoDaLista();
        }

        private void proximoDaLista()
        {
            if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
            {
                listBox1.SelectedIndex = 0;
            }
            else
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }

            this.loadImagem(listBox1.SelectedItem.ToString());
        }

        private void getAllImagens()
        {
            List<Imagem> listaImagem = imagemController.GetFromPath(textBoxDiretorio.Text);
            imagemController.AddImagens(listaImagem);
            foreach (Imagem imagem in listaImagem)
            {
                listBox1.Items.Add(imagem.Caminho);
            }
        }

        private void checkFromNewImages()
        {
            List<Imagem> listaImagem = imagemController.GetFromPath(textBoxDiretorio.Text);
            if (listaImagem.Count > 0)
            {
                imagemController.AddNewImages(listaImagem);
            }            
        }

        private void showNewImages()
        {
            if (imagemController.NovasImagens.Count>0)
            {
                Imagem imagem = this.imagemController.NovasImagens[0];
                listBox1.Items.Add(imagem.Caminho);
                this.loadImagem(imagem.Caminho);
                this.imagemController.NovasImagens.RemoveAt(0);
                this.imagemController.Lista.Add(imagem);

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = Decimal.ToInt32(numericUpDown1.Value * 1000);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            formImagem = new FormImagem();
            formImagem.Show();
        }

        private void loadImagem(String image)
        {
            pictureBox1.Load(image);
            if(formImagem != null)
            {
                formImagem.pictureBox1.Load(image);
            }
            
        }
    }
}
