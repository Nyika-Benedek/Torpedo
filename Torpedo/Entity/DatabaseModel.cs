using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Entity
{
    /// <summary>
    /// Datamodell used by Entity
    /// </summary>
    public class DatabaseModel
    {
        /// <summary>
        /// Uniq Id
        /// </summary>
        public DateTime Date { get; set; }
        public int Turns { get; set; }
        public string Winner { get; set; }
        public string Player1 { get; set; }
        public int Player1Score { get; set; }
        public string Player2 { get; set; }
        public int Player2Score { get; set; }

        public DatabaseModel(DateTime date, int turns, string winner, string player1, int player1Score, string player2, int player2Score)
        {
            Date = date;
            Turns = turns;
            Winner = winner;
            Player1 = player1;
            Player1Score = player1Score;
            Player2 = player2;
            Player2Score = player2Score;
        }
    }
}
