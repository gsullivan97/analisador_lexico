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
    public partial class CustomTab : TabPage
    {
        public RichTextBox LineNumberTextBox = new RichTextBox();
        public RichTextBox richTextBox;
        private string filePath;

        public CustomTab(string title, string text, string _filePath)
            :base(title)
        {
            filePath = _filePath;

            LineNumberTextBox = new RichTextBox();
            this.Controls.Add(LineNumberTextBox);

            LineNumberTextBox.BorderStyle = BorderStyle.None;
            this.LineNumberTextBox.Location = new Point(0, 0);
            this.LineNumberTextBox.Name = "LineNumberTextBox";
            this.LineNumberTextBox.ReadOnly = true;
            this.LineNumberTextBox.ScrollBars = RichTextBoxScrollBars.None;
            this.LineNumberTextBox.Size = new Size(26, 571);
            this.LineNumberTextBox.TabIndex = 1;
            this.LineNumberTextBox.Text = "";
            this.LineNumberTextBox.MouseDown += new MouseEventHandler(this.LineNumberTextBox_MouseDown);

            richTextBox = new RichTextBox();
            richTextBox.Text = text;
            this.Controls.Add(richTextBox);

            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Location = new System.Drawing.Point(32, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(996, 571);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.SelectionChanged += new System.EventHandler(this.richTextBox_SelectionChanged);
            this.richTextBox.VScroll += new System.EventHandler(this.richTextBox_VScroll);
            this.richTextBox.FontChanged += new System.EventHandler(this.richTextBox_FontChanged);
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            AddLineNumbers();


            //richTextBox.Dock = DockStyle.Fill;
        }

        public RichTextBox getRichTextBox()
        {
            return richTextBox;
        }

        public string getFilePath()
        {
            return filePath;
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = richTextBox.GetPositionFromCharIndex(richTextBox.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void richTextBox_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox.Font;
            richTextBox.Select();
            AddLineNumbers();
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox    
            int First_Index = richTextBox.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox    
            int Last_Index = richTextBox.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox.GetLineFromCharIndex(Last_Index >= 1 ? Last_Index + 1 : Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox    
            int line = richTextBox.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox.Font.Size;
            }

            return w;
        }
    }
}
