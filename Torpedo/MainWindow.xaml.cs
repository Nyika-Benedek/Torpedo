using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Torpedo.Models;

namespace Torpedo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BattlefieldWidth = 10;
        private const int BattlefieldHeight = 10;
        private const int FieldSize = 50;

        public MainWindow()
        {
            InitializeComponent();

            DrawPoint(new Coordinate(0, 0), true);
            DrawPoint(new Coordinate(0, 1), false);

        }

        private void DrawPoint(Coordinate position, bool isHit)
        {
            var shape = new Rectangle();
            if (isHit)
            {
                shape.Fill = Brushes.Red;
            }
            else
            {
                shape.Fill = Brushes.LightBlue;
            }
            var unitY = canvas.Width / BattlefieldWidth;
            var unitX = canvas.Height / BattlefieldHeight;
            shape.Width = unitY;
            shape.Height = unitX;
            Canvas.SetLeft(shape, unitX * position.X);
            Canvas.SetTop(shape, unitY * position.Y);
            canvas.Children.Add(shape);
        }

        private void Shoot(object sender, MouseButtonEventArgs e)
        {
            // IGame.getCurrentPlayer().getIBattleField().Shoots().Stream().Foreach( (coordinate) -> DrawPoint(coordinate))
            // Model -> IGame.NextPlayer().getIBattleField().Shoot(coordinate) hozzáadja a lövések listájához
            // csak akkor csináljon bármit ha nem egy már kirajzolt pontra kattintunk
            if (!(e.OriginalSource is Rectangle))
            {
                DrawPoint(GetMousePosition(), true);
            }
        }

        private Coordinate GetMousePosition()
        {
            int x = (int)(Mouse.GetPosition(canvas).X / FieldSize);
            int y = (int)(Mouse.GetPosition(canvas).Y / FieldSize);
            return new Coordinate(x, y);
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            // Set Player Names
            var newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
            player1Name.Text = newGameWindow.Player1Name;
            player2Name.Text = newGameWindow.Player2Name;

            // Set Player 1 ships
            MessageBox.Show($"Ask {player2Name.Text} to turn away and start your shipplacement turn!");

            // Do something bc its not working              <------------------------------------------------
            //MessageBox.Show(Mouse.LeftButton.ToString());
            for (int i = 2; i < 5; i++)
            {
                var shipPlacementWindow = new ShipPlacementWindow();
                shipPlacementWindow.ShowDialog();
                while (true)
                {
                    if ((System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed))
                    {
                        MessageBox.Show(Mouse.Captured.ToString());
                    }
                }
            }

            //throw new NotImplementedException();

        }

        private void Query(object sender, RoutedEventArgs e)
        {
            var queryWindow = new QueryWindow();
            queryWindow.ShowDialog();
            //throw new NotImplementedException();
        }

        private void DatabaseCheck(object sender, RoutedEventArgs e)
        {
            // TODO Check is there an existing data file.
            if (true)
            {
                MessageBox.Show("Database is available!");
            }
            else
            {
                MessageBox.Show("There is no database!");
            }

            //throw new NotImplementedException();
        }
    }
}
