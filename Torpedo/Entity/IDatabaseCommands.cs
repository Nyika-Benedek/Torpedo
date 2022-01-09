using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Entity.ENTITYFORDUMIES
{
    interface IDatabaseCommands
    {
        List<DatabaseModel> GetScoreBoard();

        void AddEntry(DatabaseModel database);
    }
}
