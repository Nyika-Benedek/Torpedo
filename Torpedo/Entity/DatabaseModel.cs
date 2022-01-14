using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Entity
{
    /// <summary>
    /// Datamodell used by Entity to build the collums of the database.
    /// </summary>
    public class DatabaseModel
    {
        /// <summary>
        /// Time automaticly generated on add[ID].
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The number of turns.
        /// </summary>
        public int Turns { get; set; }

        /// <summary>
        /// The winner's name.
        /// </summary>
        public string Winner { get; set; }

        /// <summary>
        /// Name of player1.
        /// </summary>
        public string Player1 { get; set; }

        /// <summary>
        /// Score of Player1.
        /// </summary>
        public int Player1Score { get; set; }

        /// <summary>
        /// Name of Player2.
        /// </summary>
        public string Player2 { get; set; }

        /// <summary>
        /// Score of Player2.
        /// </summary>
        public int Player2Score { get; set; }

        /// <summary>
        /// Constructor of DatabaseModel.
        /// </summary>
        /// <param name="date">Time automaticly generated on add[ID].</param>
        /// <param name="turns">The number of turns.</param>
        /// <param name="winner">The winner's name.</param>
        /// <param name="player1">Name of player1.</param>
        /// <param name="player1Score">Score of Player1.</param>
        /// <param name="player2">Name of Player2.</param>
        /// <param name="player2Score">Score of Player2.</param>
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

        /// <summary>
        /// Constructor of DatabaseModel, but this generate the actual date if not given.
        /// </summary>
        /// <param name="turns">The number of turns.</param>
        /// <param name="winner">The winner's name.</param>
        /// <param name="player1">Name of player1.</param>
        /// <param name="player1Score">Score of Player1.</param>
        /// <param name="player2">Name of Player2.</param>
        /// <param name="player2Score">Score of Player2.</param>
        public DatabaseModel(int turns, string winner, string player1, int player1Score, string player2, int player2Score)
        {
            Date = DateTime.Now;
            Turns = turns;
            Winner = winner;
            Player1 = player1;
            Player1Score = player1Score;
            Player2 = player2;
            Player2Score = player2Score;
        }
    }
}
