using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador_1_Analise_Lexica.Editor
{
    public partial class PanelError : UserControl
    {
        private int panelErrorHeight;
        private bool hidePanelError;

        private int panelOutputHeight;
        private bool hidePanelOutput;

        public RichTextBox getRichError { get { return richTextBox1; } set { richTextBox1 = value; } }
        public RichTextBox getRichOutput { get { return richOutput; } set { richOutput = value; } }

        public PanelError()
        {
            InitializeComponent();

            panelErrorHeight = pError.Height;
            hidePanelError = false;

            panelOutputHeight = panelOutput.Height;
            hidePanelOutput = false;

            showHideError();
            //showHideOutput();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showHideError();
        }

        public void hidePanels()
        {
            hideError();
            hideOutput();
        }

        public void showError()
        {
            pError.Height = panelErrorHeight;
            hidePanelError = false;
            pError.Visible = true;
        }

        public void hideError()
        {
            pError.Height = -panelErrorHeight;
            hidePanelError = true;
        }

        public void showOutput()
        {
            panelOutput.Height = panelOutputHeight;
            hidePanelOutput = false;
            panelOutput.Visible = true;
        }

        public void hideOutput()
        {
            panelOutput.Height = -panelOutputHeight;
            hidePanelOutput = true;
        }

        public void showHideError()
        {
            pError.Visible = true;

            if (hidePanelError || panelOutput.Visible)
            {
                pError.Height = panelErrorHeight;
                hidePanelError = false;
            }
            else
            {
                pError.Height = -panelErrorHeight;
                hidePanelError = true;
            }

            panelOutput.Visible = false;
        }

        public void showHideOutput()
        {
            if (hidePanelOutput || pError.Visible)
            {
                panelOutput.Height = panelOutputHeight;
                hidePanelOutput = false;
            }
            else
            {
                panelOutput.Height = -panelOutputHeight;
                hidePanelOutput = true;
            }

            panelOutput.Visible = true;
            pError.Visible = false;
        }

        public void setColorDetailError(string newColor = "")
        {
            if (!string.IsNullOrEmpty(newColor))
            {
                Color color = ColorTranslator.FromHtml(newColor);
                detailError.BackColor = color;
            }
            else
            {
                Color color = new Color();

                if (!string.IsNullOrEmpty(richTextBox1.Text))
                    color = ColorTranslator.FromHtml("#F03434");
                else
                    color = ColorTranslator.FromHtml("#D3B833");

                detailError.BackColor = color;
            }
        }

        public void setColorDetailOutput(string newColor = "")
        {
            if (!string.IsNullOrEmpty(newColor))
            {
                Color color = ColorTranslator.FromHtml(newColor);
                detailOutput.BackColor = color;
            }
            else
            {
                Color color = ColorTranslator.FromHtml("#D3B833");
                detailOutput.BackColor = color;
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            setColorDetailError();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
                detailError.BackColor = Color.DimGray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showHideOutput();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            setColorDetailOutput();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            detailOutput.BackColor = Color.DimGray;
        }
    }
}
