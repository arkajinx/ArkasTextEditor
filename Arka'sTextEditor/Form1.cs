using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ArkasTextEditor
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            this.Text = "Arka's Text Editor [Untitled Document]*";
            rchTextBox.SelectionChanged += new EventHandler(this.rchTextBox_SelectionChanged);
            rchTextBox.LinkClicked += new LinkClickedEventHandler(this.rchTextBox_LinkClicked);
            rchTextBox.TextChanged += new EventHandler(this.rchTextBox_TextChanged);
            rchTextBox.VScroll += new EventHandler(this.rchTextBox_Vscroll);
            rchTextBox.FontChanged += new EventHandler(this.rchTextBox_FontChanged);
            rchTextBox.MouseDown += new MouseEventHandler(this.rchTextBox_MouseDown);
            linenumberTextBox.MouseDown += new MouseEventHandler(this.linenumberTextBox_MouseDown);
            rchTextBox.Resize += new EventHandler(this.rchTextBox_Resize);
            rchTextBox.Select();
            AddLineNumbers();
            FormClosed += new FormClosedEventHandler(this.Form1_Close);
            rchTextBox.ContextMenuStrip = contextMenuStrip1;
        }

        private void Form1_Close(object sender, FormClosedEventArgs e)
        {
            if (this.Text.Contains("*"))
            {
                if (rchTextBox.Text != "")
                {
                    DialogResult ds = MessageBox.Show("The untitled file has not been saved.\n\n Do you want to save the file?", "Arka's Text Editor.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ds == DialogResult.Yes)
                    {

                        SaveFileDialog fd2 = new SaveFileDialog();
                        fd2.Title = "Save";
                        fd2.Filter = "Text Files|*.txt|All Files|*.*";
                        fd2.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
                        if (fd2.ShowDialog() == DialogResult.OK)
                        {
                            rchTextBox.SaveFile(fd2.FileName, RichTextBoxStreamType.UnicodePlainText);
                            this.Text = fd2.FileName;
                        }
                        Application.Exit();
                    }
                    if (ds == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void rchTextBox_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void linenumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            rchTextBox.Select();
            linenumberTextBox.DeselectAll();
        }

        private void rchTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            rchTextBox.Select();
            linenumberTextBox.DeselectAll();
        }

        private void rchTextBox_FontChanged(object sender, EventArgs e)
        {
            linenumberTextBox.Font = rchTextBox.Font;
            AddLineNumbers();
        }

        private void rchTextBox_Vscroll(object sender, EventArgs e)
        {
            linenumberTextBox.Text = "";
            AddLineNumbers();
            linenumberTextBox.Invalidate();
        }

        public void AddLineNumbers()
        {
            rchTextBox.Select();
            // create & set Point pt to (0,0)
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1
            int First_Index = rchTextBox.GetCharIndexFromPosition(pt);
            int First_Line = rchTextBox.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1
            int Last_Index = rchTextBox.GetCharIndexFromPosition(pt);
            int Last_Line = rchTextBox.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox
            linenumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value
            linenumberTextBox.Text = "";
            linenumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                linenumberTextBox.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1
            int line = rchTextBox.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)rchTextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)rchTextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)rchTextBox.Font.Size;
            }

            return w;
        }

        private void rchTextBox_TextChanged(object sender, EventArgs e)
        {
            String str = this.Text;
            if (str.Contains("*"))
            {

            }
            else
            {
                this.Text = str + "*";
            }
            AddLineNumbers();
            MatchCollection WordColl = Regex.Matches(rchTextBox.Text, @"[\W]+");
            txtWordcount.Text = "Word Count : " + WordColl.Count.ToString();
        }

        private void rchTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.LinkText);
            }
            catch
            {
                
            }
        }

        private void rchTextBox_SelectionChanged(object sender, EventArgs e)
        {

            int sel = rchTextBox.SelectionStart;
            int line = rchTextBox.GetLineFromCharIndex(sel) + 1;
            int col = sel - rchTextBox.GetFirstCharIndexFromLine(line - 1) + 1;

            this.LinetoolStripStatus.Text = "Line : " + line.ToString();
            this.ColtoolStripStatus.Text = "Col : " + col.ToString();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rchTextBox.Text != "")
            {
                if (this.Text.Contains("*"))
                {


                    DialogResult ds = MessageBox.Show("The untitled file has not been saved.\n\n Do you want to save the file?", "Arka's Text Editor.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (ds == DialogResult.Yes)
                    {
                        SaveFileDialog fd2 = new SaveFileDialog();
                        fd2.Title = "Save";
                        fd2.Filter = "txt Files|*.txt|All Files|*.*";
                        fd2.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
                        if (fd2.ShowDialog() == DialogResult.OK)
                        {
                            rchTextBox.SaveFile(fd2.FileName, RichTextBoxStreamType.UnicodePlainText);                           
                        }
                        rchTextBox.Clear();
                        this.Text = "Arka's Text Editor [Untitled Document]*";
                        txtFilename.Text = fd2.FileName.ToString();

                    }
                    if (ds == DialogResult.No)
                    {
                        rchTextBox.Clear();
                        this.Text = "Arka's text Editor [Untitled Document]*";
                        txtFilename.Text = " ";
                    }
                }
                else
                {
                    rchTextBox.Clear();
                    this.Text = "Arka's text Editor [Untitled Document]*";
                    txtFilename.Text = " ";
                }
            }
            else
            {
                rchTextBox.Clear();
                this.Text = "Arka's text Editor [Untitled Document]*";
                txtFilename.Text = " ";
            }

        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Filter = "Text Files|*.txt|All Files|*.*";
            fd1.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
            fd1.Title = "Open";
            if (fd1.ShowDialog() == DialogResult.OK)
            {
                string filename = fd1.FileName;
                StreamReader str = new StreamReader(filename);
                rchTextBox.Text = str.ReadToEnd();
                str.Close();
                this.Text = filename.Substring(filename.LastIndexOf("\\") + 1);
                txtFilename.Text = filename.ToString();
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.Text.Contains("*"))
            {
                
                            
                if (File.Exists(txtFilename.Text))
                {                    
                    rchTextBox.SaveFile(txtFilename.Text, RichTextBoxStreamType.UnicodePlainText);                   
                    this.Text = txtFilename.Text.Substring(txtFilename.Text.LastIndexOf("\\") + 1);
                }
                else
                {
                    SaveFileDialog fd2 = new SaveFileDialog();
                    fd2.Title = "Save";
                    fd2.Filter = "Text Files|*.txt|All Files|*.*";
                    fd2.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
                    if (fd2.ShowDialog() == DialogResult.OK)
                    {
                        rchTextBox.SaveFile(fd2.FileName, RichTextBoxStreamType.PlainText);
                        string filename = fd2.FileName;
                        this.Text = filename.Substring(filename.LastIndexOf("\\") + 1);
                        txtFilename.Text = filename;
                    }
                }
            }
            else
            {

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd3 = new SaveFileDialog();
            fd3.Title = "Save As";
            fd3.Filter = "Text Files|*.txt|All Files|*.*";
            fd3.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
            if (fd3.ShowDialog() == DialogResult.OK)
            {
                rchTextBox.SaveFile(fd3.FileName, RichTextBoxStreamType.PlainText);
                string filename = fd3.FileName;
                this.Text = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
        }

        
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.rchTextBox.Print();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetupDialog1.ShowNetwork = false;
            DialogResult result = pageSetupDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                object[] results = new object[]{
                    pageSetupDialog1.PageSettings.Margins,
                    pageSetupDialog1.PageSettings.PaperSize,
                    pageSetupDialog1.PageSettings.Landscape,
                    pageSetupDialog1.PrinterSettings.PrinterName,
                    pageSetupDialog1.PrinterSettings.PrintRange};
                //richTextBox1.Text.LastIndexOf(results);
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.SelectAll();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.ShowDialog();
            rchTextBox.Font = fontDialog1.Font;
            rchTextBox.ForeColor = fontDialog1.Color;
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colordialog = new ColorDialog();
            if (colordialog.ShowDialog() == DialogResult.OK)
            {
                rchTextBox.BackColor = colordialog.Color;
            }
        }

        
        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string timeDate;
            timeDate = DateTime.Now.ToShortTimeString() + " " +
            DateTime.Now.ToShortDateString();
            int newSelectionStart = rchTextBox.SelectionStart + timeDate.Length;
            rchTextBox.Text = rchTextBox.Text.Insert(rchTextBox.SelectionStart, timeDate);
            rchTextBox.SelectionStart = newSelectionStart;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find_Form f = new Find_Form(rchTextBox);
            f.Show();
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoTo_Form gotof = new GoTo_Form(rchTextBox);
            gotof.ShowDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replace_Form replace = new Replace_Form(rchTextBox);
            replace.ShowDialog();
        }

        private void goonlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.co.in/search?q=Notepad&sourceid=chrome&ie=UTF-8");
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About_Form f1 = new About_Form();
            f1.Show();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Arka's Text Editor.chm");
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

            if (rchTextBox.Text != "")
            {
                if (this.Text.Contains("*"))
                {


                    DialogResult ds = MessageBox.Show("The untitled file has not been saved.\n\n Do you want to save the file?", "Arka's Text Editor.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (ds == DialogResult.Yes)
                    {
                        SaveFileDialog fd2 = new SaveFileDialog();
                        fd2.Title = "Save";
                        fd2.Filter = "Text Files|*.txt|All Files|*.*";
                        fd2.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
                        if (fd2.ShowDialog() == DialogResult.OK)
                        {
                            rchTextBox.SaveFile(fd2.FileName, RichTextBoxStreamType.PlainText);
                        }
                        rchTextBox.Clear();
                        this.Text = "Arka's Text Editor [Untitled Document]*";
                        txtFilename.Text = " ";

                    }
                    if (ds == DialogResult.No)
                    {
                        rchTextBox.Clear();
                        this.Text = "Arka's text Editor [Untitled Document]*";
                        txtFilename.Text = " ";
                    }
                }
                else
                {
                    rchTextBox.Clear();
                    this.Text = "Arka's text Editor [Untitled Document]*";
                    txtFilename.Text = " ";
                }
            }
            else
            {
                rchTextBox.Clear();
                this.Text = "Arka's text Editor [Untitled Document]*";
                txtFilename.Text = " ";
            }

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Filter = "Text Files|*.txt|All Files|*.*";
            fd1.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
            fd1.Title = "Open";
            if (fd1.ShowDialog() == DialogResult.OK)
            {
                string filename = fd1.FileName;
                StreamReader str = new StreamReader(filename);
                rchTextBox.Text = str.ReadToEnd();
                str.Close();
                this.Text = filename.Substring(filename.LastIndexOf("\\") + 1);
                txtFilename.Text = filename.ToString();

            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.Text.Contains("*"))
            {

               

                if (File.Exists(txtFilename.Text))
                {
                    rchTextBox.SaveFile(txtFilename.Text, RichTextBoxStreamType.PlainText);
                    this.Text = txtFilename.Text.Substring(txtFilename.Text.LastIndexOf("\\") + 1);
                }
                else
                {
                    SaveFileDialog fd2 = new SaveFileDialog();
                    fd2.Title = "Save";
                    fd2.Filter = "Text Files|*.txt|All Files|*.*";
                    fd2.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents";
                    if (fd2.ShowDialog() == DialogResult.OK)
                    {
                        rchTextBox.SaveFile(fd2.FileName, RichTextBoxStreamType.PlainText);
                        string filename = fd2.FileName;
                        this.Text = filename.Substring(filename.LastIndexOf("\\") + 1);
                        txtFilename.Text = filename;
                    }
                }
            }
            else
            {

            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            this.rchTextBox.Print();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            rchTextBox.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            rchTextBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            rchTextBox.Paste();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Process.Start("Arka's Text editor.chm");
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTextBox.WordWrap = true;
        }

        private void restartAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {            
                MessageBox.Show("No new updates are available.", "Arka's Text Editor.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Run_Form rf = new Run_Form(txtFilename.Text);
                rf.ShowDialog();
            }
            catch(Exception exp)
            {
                MessageBox.Show("This file cannot be opened by the program. \n\n Errors : " + exp.ToString(), "Arka's Text Editor.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void runInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtFilename.Text.Contains(".html") || txtFilename.Text.Contains(".htm") || txtFilename.Text.Contains(".php") || txtFilename.Text.Contains(".asp") || txtFilename.Text.Contains(".js") || txtFilename.Text.Contains(".xml"))
            {
                try
                {
                    Process.Start("\"" + txtFilename.Text + "\"");
                }
                catch (Exception exp)
                {
                    MessageBox.Show("This file is not supported in browser.\n\n Errors : " + exp.ToString(), "Arka's Text Editor.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                MessageBox.Show("This file is not supported in browser.", "Arka's Text Editor.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void previewHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewHTMLPage_Form previewhtmlpage = new PreviewHTMLPage_Form(rchTextBox.Text, txtFilename.Text);
            previewhtmlpage.Show();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rchTextBox.Cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rchTextBox.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rchTextBox.Paste();
        }

        private void selectallToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rchTextBox.SelectAll();
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Find_Form f = new Find_Form(rchTextBox);
            f.Show();
        }

        private void gotoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GoTo_Form gotof = new GoTo_Form(rchTextBox);
            gotof.ShowDialog();
        }

        private void replaceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Replace_Form replace = new Replace_Form(rchTextBox);
            replace.ShowDialog();
        }
    }
}
