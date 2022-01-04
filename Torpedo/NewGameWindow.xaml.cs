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
        private void onChecked(object sender, RoutedEventArgs e)
        {
            Player2.IsEnabled = false;
            Player2.Text = "AI";
        }

        private void onUnchecked(object sender, RoutedEventArgs e)
        {
            Player2.IsEnabled = true;
            Player2.Text = string.Empty;
        }

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
