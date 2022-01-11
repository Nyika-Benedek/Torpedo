using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Torpedo.Entity;

namespace Torpedo
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        /// <summary>
        /// Query the database's content, then fills up the QueryGrid's Datagrids
        /// </summary>
        public QueryWindow()
        {
            InitializeComponent();

            DatabaseCommands database = new DatabaseCommands();
            /*
            database.AddEntry(new DatabaseModel(DateTime.Now, 5, "Hardcoded entry via Entity Framework", "Player1", 15, "Player2", 2));
            QueryGrid.Items.Add(new DatabaseModel(DateTime.Now, 5, "Hardcoded entry", "Player1", 15, "Player2", 2));
            */

            foreach (var item in database.GetScoreBoard())
            {
                QueryGrid.Items.Add(item);
            }
        }
    }
}
