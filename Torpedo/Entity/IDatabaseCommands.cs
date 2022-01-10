using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Entity.ENTITYFORDUMIES
{
    /// <summary>
    /// This Interface describes all the available operations that can be used on the database.
    /// </summary>
    public interface IDatabaseCommands
    {
        List<DatabaseModel> GetScoreBoard();

        void AddEntry(DatabaseModel entry);
    }
}
