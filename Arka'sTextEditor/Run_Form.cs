using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace ArkasTextEditor
{
    public partial class Run_Form : Form
    {
        string filename = "";
        public Run_Form(String f)
        {
            InitializeComponent();
            filename ="\"" + f.ToString() + "\"" ;
        }      

        private void selectfile_button_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && filename != "")
            {
                if(File.Exists(textBox1.Text))
                {
                    Process.Start(textBox1.Text,filename);
                    this.Close();
                }
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
