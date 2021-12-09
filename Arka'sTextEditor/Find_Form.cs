using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkasTextEditor
{
    public partial class Find_Form : Form
    {
        RichTextBox richtext;
        public Find_Form(RichTextBox R)
        {
            InitializeComponent();
            richtext = R;
        }

        public static void Find(RichTextBox rtb, String word, Color color,Color backcolor)
        {
            if (word == "")
            {
                return;
            }
            int s_start = rtb.SelectionStart, startIndex = 0, index;
            while ((index = rtb.Text.IndexOf(word, startIndex)) != -1)
            {
                rtb.Select(index, word.Length);
                rtb.SelectionColor = color;
                rtb.SelectionBackColor = backcolor;
                startIndex = index + word.Length;
            }
            rtb.SelectionStart = s_start;
            rtb.SelectionLength = 0;
            rtb.SelectionColor = Color.Black;
            rtb.SelectionBackColor = Color.Yellow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Find(richtext, textBox1.Text, Color.Blue,Color.Yellow);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Find(richtext, textBox1.Text, Color.Black,Color.White);
            this.Close();
        }

        private void Find_Form_Load(object sender, EventArgs e)
        {

        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            Find(richtext, textBox1.Text, Color.Black,Color.White);
            textBox1.Text = String.Empty;
        }
    }
}

