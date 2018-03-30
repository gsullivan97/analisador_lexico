using Compilador_1_Analise_Lexica.Lexer;
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

namespace Compilador_1_Analise_Lexica.Editor
{
    public partial class TextEditor : Form
    {
        private string fileOpenNow = "";
        private RichTextBox richTextBox = new RichTextBox();

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        private Image CloseImage;

        public TextEditor()
        {
            InitializeComponent();
        }

        private void TextEditor_Load_1(object sender, EventArgs e)
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            CloseImage = Properties.Resources.close;
            tabControl1.Padding = new Point(10, 3);

            tabControl1.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tabControl = new CustomTab("New file", "", "");
            richTextBox = tabControl.getRichTextBox();

            tabControl1.TabPages.Add(tabControl);
            tabControl1.SelectedIndex = tabControl1.TabCount - 1;
            //richTextBox.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Title = "Browse Text Files";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "txt";
            ofd.Filter = "Text files (*.txt)|*.txt";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            ofd.ReadOnlyChecked = true;
            ofd.ShowReadOnly = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                //richTextBox.Text = sr.ReadToEnd();
                fileOpenNow = ofd.FileName;

                string filenameWithoutPath = Path.GetFileName(ofd.FileName);

                var tabControl = new CustomTab(filenameWithoutPath, sr.ReadToEnd(), fileOpenNow);
                richTextBox = tabControl.getRichTextBox();

                tabControl1.TabPages.Add(tabControl);
                tabControl1.SelectedIndex = tabControl1.TabCount - 1;
                sr.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(fileOpenNow, richTextBox.Text);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TS TabelaSimbolos = new TS();
            Token token;
            RichTextBox textOutput = new RichTextBox();

            try
            {
                Analise_Lexica analise = new Analise_Lexica(fileOpenNow, TabelaSimbolos);

                do
                {
                    //chamda parao proximo token que sera lido
                    token = analise.ProximoToken();

                    if (token != null)
                    {
                        //imprime o token com linas e colunas
                        textOutput.Text += "Token: " + token.toString() + "\t Linha: " + analise.linha + "\t Coluna: " + analise.coluna + "\n";
                        //Console.WriteLine("Token: " + token.toString() + "\t Linha: " + analise.linha + "\t Coluna: " + analise.coluna);
                    }

                } while (token != null && token.Classe != Lexer.Tag.EOF); // do while só para se chegar no fim do arquivo

                textOutput.Text += "\n";
                textOutput.Text += "Tabela de simbolos: \n";
                textOutput.Text += TabelaSimbolos.toString();


                var tabControl = new CustomTab("Output", textOutput.Text, fileOpenNow);
                richTextBox = tabControl.getRichTextBox();
                tabControl1.TabPages.Add(tabControl);

                tabControl1.SelectedIndex = tabControl1.TabCount - 1;

                //fecha o arquivo depos de ter terminado a analise
                analise.Fechar_Arquivo();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "Text files (*.txt)|*.txt";
            svf.Title = "Save File";

            if (svf.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(svf.FileName);
                sw.Write(richTextBox.Text);
                sw.Close();
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Image img = new Bitmap(CloseImage, new Size(7, 7));
                Rectangle r = e.Bounds;
                r = this.tabControl1.GetTabRect(e.Index);
                r.Offset(2, 2);
                Brush TitleBrush = new SolidBrush(Color.Black);
                Font f = this.Font;
                string title = this.tabControl1.TabPages[e.Index].Text;

                e.Graphics.DrawString(title, f, TitleBrush, new PointF(r.X, r.Y));

                e.Graphics.DrawImage(img, new Point(r.X + (this.tabControl1.GetTabRect(e.Index).Width - _imageLocation.X), _imageLocation.Y));
            }
            catch (Exception) { }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            Point p = e.Location;
            int _tabWidth = 0;
            _tabWidth = this.tabControl1.GetTabRect(tc.SelectedIndex).Width - (_imgHitArea.X);
            Rectangle r = this.tabControl1.GetTabRect(tc.SelectedIndex);
            r.Offset(_tabWidth, _imgHitArea.Y);
            r.Width = 16;
            r.Height = 16;

            if (r.Contains(p))
            {
                TabPage TabP = (TabPage)tc.TabPages[tc.SelectedIndex];
                tc.TabPages.Remove(TabP);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            var current = (CustomTab)(sender as TabControl).SelectedTab;

            if (current != null)
            {
                richTextBox = current.getRichTextBox();
                fileOpenNow = current.getFilePath();
            }
        }
    }
}
