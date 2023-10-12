using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batak
{
    class BatakGame
    {

        private Player[] players;
        private Card[] desk ;
        private String koz;
        private int startTurn;
        private int turn;
        private int[] scores;
        private int[] totalScores;

        private int[] claims;
        private int maxClaim;
        private int claimer;

        private int gameStage; // 0- players coming, 1- claim, 2- game, 3- aftergame

        public BatakGame() 
        {
            StartTurn = 0;
            TotalScores = new int[]{0,0,0,0};
            Scores = new int[] { 0, 0, 0, 0 };
            Claims = new int[] { 0, 0, 0, 0 };
            Players = new Player[4];
            Desk = new Card[4];
            MaxClaim = 0;
            GameStage = 0;
        }

        public int AddPlayer(Player p)
        {

            for (int i = 0; i<4; i++)
            {
                if (Players[i] == null)
                {
                    Players[i] = p;
                    Players[i].Id = i;
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

        /*
         * Takes a claim: 
         * if it is viable make is claim of player and increments turn 
         * if not does nothing
         * returns whose turn it is
        */
        public int MakeClaim(int claim)
        {
            if (claim == 0)
            {
                Claims[Turn] = 0;
                Turn++;
                return Turn;
            }
            if (claim <= MaxClaim) return Turn;

            Claims[Turn] = claim;
            Turn = ++Turn % 4;

            MaxClaim = claim;

            return Turn;
        }


        /*
         * Checks whether claiming process is done
         */
        public bool ClaimsDone()
        {
            foreach(int i in claims) if (i != 0) return false;

            Turn = (Turn + 3) % 4;
            Claimer = Turn;

            return true;
        }

        public String ChooseKoz(int i)
        {
            Koz = Card.Colors[i];
            GameStage = 2;
            return Card.Colors[i];
        }


        public bool CardPlayable(Card card) => true;


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



        public string Koz { get => koz; set => koz = value; }
        public int Turn { get => turn; set => turn = value; }
        public int[] Scores { get => scores; set => scores = value; }
        public int[] Claims { get => claims; set => claims = value; }
        public int StartTurn { get => startTurn; set => startTurn = value; }
        public int[] TotalScores { get => totalScores; set => totalScores = value; }
        internal Player[] Players { get => players; set => players = value; }
        internal Card[] Desk { get => desk; set => desk = value; }
        public int MaxClaim { get => maxClaim; set => maxClaim = value; }
        public int GameStage { get => gameStage; set => gameStage = value; }
        public int Claimer { get => claimer; set => claimer = value; }
    }
}
