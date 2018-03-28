using System;
using System.Windows.Forms;
using Compilador_1_Analise_Lexica.Lexer;

namespace Compilador_1_Analise_Lexica.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {
                textBox1.Text = openFileDialog1.FileName;
                richTextBox1.ResetText();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TS TabelaSimbolos = new TS();
            Token token;
            richTextBox1.ResetText();

            try
            {
                Analise_Lexica analise = new Analise_Lexica(textBox1.Text, TabelaSimbolos);

                do
                {
                    //chamda parao proximo token que sera lido
                    token = analise.ProximoToken();

                    if (token != null)
                    {
                        //imprime o token com linas e colunas
                        richTextBox1.Text += "Token: " + token.toString() + "\t Linha: " + analise.linha + "\t Coluna: " + analise.coluna + "\n";
                        //Console.WriteLine("Token: " + token.toString() + "\t Linha: " + analise.linha + "\t Coluna: " + analise.coluna);
                    }

                } while (token != null && token.Classe != Lexer.Tag.EOF); // do while só para se chegar no fim do arquivo

                //fecha o arquivo depos de ter terminado a analise
                analise.Fechar_Arquivo();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
