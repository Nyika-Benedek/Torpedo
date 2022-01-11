using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Entity
{
    /// <summary>
    /// This Interface collects all the available operations that can be used on the database.
    /// </summary>
    public interface IDatabaseCommands
    {
        List<DatabaseModel> GetScoreBoard();

        void AddEntry(DatabaseModel entry);
    }
}
