using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torpedo.Entity.ENTITYFORDUMIES
{
    /// <summary>
    /// This class is responsible to provides all the necessary operation, to be able to apply changes on the active database, and make queries from it.
    /// </summary>
    public class DatabaseCommands : IDatabaseCommands
    {

        /// <summary>
        /// Give a DatabaseModel type in, and its adds to the active database.
        /// </summary>
        /// <param name="entry">Database type, like 1 row of the database</param>
        public void AddEntry(DatabaseModel entry)
        {
            using (var database = new Context())
            {
                database.ScoreBoard.Add(entry);
                // Hiba esetén!
                // PackageManager Console: Update-Database
                database.SaveChanges();
            }
        }

        /// <summary>
        /// It makes a query from whole the database
        /// </summary>
        /// <returns>The whole database in List<DatabaseModel></returns>
        public List<DatabaseModel> GetScoreBoard()
        {
            using (var database = new Context())
            {
                return database.ScoreBoard.ToList();
            }
        }
    }
}
