using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batak
{
    class BatakGame
    {

        private Deck myDeck;
        private Player[] players;
        private Card[] desk ;
        private string koz;
        private int startTurn;
        private int turn;
        private int[] scores;
        private int[] totalScores;

        private int[] bids;
        private int maxBid;
        private int bidder;

        private int gameStage; // 0- players coming, 1- claim, 2- game, 3- aftergame

        private bool kozOpened;

        private Form1 myForm;

        public BatakGame(Form1 form) 
        {
            StartTurn = 0;
            TotalScores = new int[]{0,0,0,0};
            Scores = new int[] { 0, 0, 0, 0 };
            Bids = new int[] { 0, 0, 0, 0 };
            Players = new Player[4];
            Desk = new Card[4];
            MaxBid = Int32.MinValue;
            GameStage = 0;
            KozOpened = false;
            MyDeck = new Deck();
            MyForm = form;
        }

        public int Play()
        {
            /// Players Join
            AddPlayer("Berat");

            AddPlayer("Memin");

            AddPlayer("İsot");

            AddPlayer("Ali");

            //// Cards are distributed

            MyDeck.FillDesk();
            MyDeck.shuffleHands();

            DistributeCards();


            //// Who is going to start
            Turn = StartTurn;
            StartTurn++;


            /// Bidding Start
            MaxBid = 4;
            Bidder = Turn;


            MyForm.AddTextToTextBox(Turn.ToString() + "bidded 5");
            MakeBid(5); // starter 

            MyForm.AddTextToTextBox(Turn.ToString() + "bidded 0");
            MakeBid(0);

            MyForm.AddTextToTextBox(Turn.ToString() + "bidded 0");
            MakeBid(0);


            MyForm.AddTextToTextBox(Turn.ToString() + "bidded 0");
            MakeBid(0);


            /// Bidding has ended
            MyForm.AddTextToTextBox(Turn.ToString ()+ "starting with bid" + MaxBid.ToString());


            /// Winner of bidding will decide what the koz is
            /// 

            ChooseKoz(0);


            return 0;
        }

        public int AddPlayer(String n)
        {

            for (int i = 0; i<4; i++)
            {
                if (Players[i] == null)
                {
                    Players[i] = new Player(n, 0, i);
                    return i;
                }
            }
            GameStage = 1;
            return -1;
        }

        public bool IsFull()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Players[i] == null) return false;
            }
            GameStage = 1;
            return true;
        }

        public void DistributeCards() {
            for(int i = 0; i< 4; i++)   Players[i].Hand = MyDeck.Hands[i];
        }

        /*
         * Takes a claim: 
         * if it is viable make is claim of player and increments turn 
         * if not does nothing
         * returns whose turn it is
        */
        public int MakeBid(int bid)
        {
            if (IsBiddingDone()) return -2; // bidding is done

            if (bid == 0)
            {
                Bids[Turn] = bid;
                Turn = ++Turn % 4;
            }else if (bid < MaxBid)
            {
                return -1; // cannot bid this number
            }
            else if (bid > MaxBid)
            {
                MaxBid = bid;
                Bidder = Turn;
                Bids[Turn] = bid;
                Turn = ++Turn % 4;
            }

            return Turn;
        }


        /*
         * Checks whether claiming process is done
         */
        public bool IsBiddingDone() => (Turn == Bidder);

        public string ChooseKoz(int i)
        {
            Koz = Card.Colors[i];
            GameStage = 2;
            return Card.Colors[i];
        }


        public bool CardPlayable(Card card) => true; ///koz + artırma

        public bool PlayCard(Card card)
        {

            if (CardPlayable(card))
            {
                desk[Turn] = card;
                Turn = ++Turn % 4;
                return true;
            }
            return false;
        }


        /*
         finds the winner of the turn 
         But doesnt add the points or rearrange turn
         */
        public int WinnerOfTheTurn()
        {
            bool kozPlayed = false;

            int maxCard = -1;
            int maxPlayer = 0;

            for (int i = 0; i<4; i++)
            {
                Card card = Desk[i];

                if ((card.Color == Koz) && !kozPlayed)
                {
                    kozPlayed = true;
                    KozOpened = true;
                    maxCard = card.Value;
                    maxPlayer = i;
                }
                else if(!kozPlayed || (card.Color == Koz ) )
                {
                    if(card.Value > maxCard)
                    {
                        maxCard = card.Value;
                        maxPlayer = i;
                    }
                }

                
            }
            return maxPlayer;
        }





        public string Koz { get => koz; set => koz = value; }
        public int Turn { get => turn; set => turn = value; }
        public int[] Scores { get => scores; set => scores = value; }
        public int[] Bids { get => bids; set => bids = value; }
        public int StartTurn { get => startTurn; set => startTurn = value; }
        public int[] TotalScores { get => totalScores; set => totalScores = value; }
        internal Player[] Players { get => players; set => players = value; }
        internal Card[] Desk { get => desk; set => desk = value; }
        public int MaxBid { get => maxBid; set => maxBid = value; }
        public int GameStage { get => gameStage; set => gameStage = value; }
        public int Bidder { get => bidder; set => bidder = value; }
        public bool KozOpened { get => kozOpened; set => kozOpened = value; }
        internal Deck MyDeck { get => myDeck; set => myDeck = value; }
        public Form1 MyForm { get => myForm; set => myForm = value; }
    }
}
