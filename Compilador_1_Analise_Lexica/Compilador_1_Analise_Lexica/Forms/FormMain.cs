using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Compilador_1_Analise_Lexica.Editor;
using Compilador_1_Analise_Lexica.Lexer;

namespace Compilador_1_Analise_Lexica.Forms
{
    public partial class FormMain : Form
    {
        private string fileOpenNow = "";
        private RichTextBox richTextBox = new RichTextBox();

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        private Image CloseImage;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            CloseImage = Properties.Resources.close;
            tabControl1.Padding = new Point(10, 3);

            tabControl1.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);

            showHideItens();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void NewFile()
        {
            var tabControl = new CustomTab("New file", "", "");
            richTextBox = tabControl.getRichTextBox();

            tabControl1.TabPages.Add(tabControl);
            tabControl1.SelectedIndex = tabControl1.TabCount - 1;
            showHideItens();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            showHideItens();

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

        private void label2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
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
                showHideItens();
                sr.Close();
            }
        }

        private void tabControl1_MouseClick_1(object sender, MouseEventArgs e)
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

        private void tabControl1_Selecting_1(object sender, TabControlCancelEventArgs e)
        {
            var current = (CustomTab)(sender as TabControl).SelectedTab;

            if (current != null)
            {
                richTextBox = current.getRichTextBox();
                fileOpenNow = current.getFilePath();
            }

            showHideItens();
        }

        private void showHideItens()
        {
            if (tabControl1.TabPages.Count <= 0)
            {
                labelInfo.Show();
                separator.Show();
                pictureBox7.Show();
                pictureBox8.Show();
                tabControl1.Visible = false;
            }
            else
            {
                labelInfo.Hide();
                separator.Hide();
                pictureBox7.Hide();
                pictureBox8.Hide();
                tabControl1.Visible = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            if (!string.IsNullOrEmpty(fileOpenNow))
                File.WriteAllText(fileOpenNow, richTextBox.Text);
            else
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "Text files (*.txt)|*.txt";
                svf.Title = "Save File";

                if (svf.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(svf.FileName);
                    sw.Write(richTextBox.Text);

                    var teste = tabControl1.SelectedIndex;

                    tabControl1.TabPages[teste].Text = Path.GetFileName(svf.FileName);


                    sw.Close();
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
