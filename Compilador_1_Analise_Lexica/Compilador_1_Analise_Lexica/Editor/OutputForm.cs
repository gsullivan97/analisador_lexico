using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador_1_Analise_Lexica.Editor
{
    public partial class OutputForm : Form
    {
        private TextEditor formMain;
        public OutputForm(TextEditor form)
        {
            InitializeComponent();

            this.formMain = form;
        }

        private void OutputForm_Load(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionColor = formMain.RichOutput.SelectionColor;
            this.richTextBox1.Text = formMain.RichOutput.Text;

            Color color = ColorTranslator.FromHtml("#F03434");
            this.CheckKeyword("Erro Lexico: ", color, 0);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            this.Close();
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }
    }
}
