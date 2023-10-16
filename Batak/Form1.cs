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

        private BatakGame game;
        private List<Button> cardButtons;

        internal BatakGame Game { get => game; set => game = value; }
        public List<Button> CardButtons { get => cardButtons; set => cardButtons = value; }

        public Form1()
        {
            InitializeComponent();
            Game = new BatakGame(this);
            CardButtons = new List<Button> { button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button17, button18 };
            Game.Play();
        }


        public void AddTextToTextBox(String s)
        {
            textBox1.AppendText(s + Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddTextToTextBox(Game.Turn.ToString());
            AddTextToTextBox(Game.Players[Game.Turn].HandToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTextToTextBox(Game.DeskToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddTextToTextBox(Game.WinnerCard.ToString() + " ");
            if (Game.Players[Game.Turn].HasGreater(Game.TurnsColor, Game.WinnerCard.Value)) ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Game.PlayCard(Int32.Parse(richTextBox1.Text)))
            {
                AddTextToTextBox("played the card");
            }
            else
            {
                AddTextToTextBox("couldnt play the card" + Environment.NewLine);
            }
            RenameButtons();
        }

        private void RenameButtons()
        {
            List<Card> hand = Game.Players[Game.Turn].Hand;
            for (int i = 0; i<13; i++)
            {

                if (i < hand.Count)
                    CardButtons[i].Text = hand[i].ToString();
                else
                    CardButtons[i].Text = " ";
            }
        }
    }
}
