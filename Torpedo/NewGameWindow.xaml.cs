using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Torpedo
{
    /// <summary>
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        public NewGameWindow()
        {
            InitializeComponent();
        }

        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }

        /// <summary>
        /// Set the Player2's name to AI and clock it's contet
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
        private void OnChecked(object sender, RoutedEventArgs e)
        {
            Player2.IsEnabled = false;
            Player2.Text = "AI";
        }

        /// <summary>
        /// Delete the Player2's name and unlock it's content
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            Player2.IsEnabled = true;
            Player2.Text = string.Empty;
        }

        /// <summary>
        /// Checks if the names are valid, then send it to the main window, otherwise alert the user from the wrong names
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
        private void GiveNames(object sender, RoutedEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(Player1.Text) || string.IsNullOrWhiteSpace(Player1.Text))
            {
                MessageBox.Show("Player1 cannot be whitespace or contain special characters");
                return;
            }

            if (regex.IsMatch(Player2.Text) || string.IsNullOrWhiteSpace(Player2.Text))
            {
                MessageBox.Show("Player2 cannot be whitespace or contain special characters");
                return;
            }

            Player1Name = Player1.Text;
            Player2Name = Player2.Text;
            Close();
        }
    }
}
