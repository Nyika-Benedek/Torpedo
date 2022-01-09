using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torpedo.Entity.ENTITYFORDUMIES
{
    class DatabaseCommands : IDatabaseCommands
    {
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

        public List<DatabaseModel> GetScoreBoard()
        {
            using (var database = new Context())
            {
                return database.ScoreBoard.ToList();
            }
        }
    }
}
