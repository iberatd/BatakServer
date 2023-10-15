using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Batak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BatakGame game = new BatakGame(this);
            game.Play();
        }


        public void AddTextToTextBox(String s)
        {
            textBox1.AppendText(s + Environment.NewLine);
        }
    }
}
