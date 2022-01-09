using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        // states
        private IGame _game;
        private bool _inShipPlacement = false;
        private int _currentShipSize = 2;
        private Direction _currentDirection = Direction.Horizontal;
        private bool isDatabaseExists;

        public MainWindow()
        {
            InitializeComponent();

            DrawPoint(new Coordinate(0, 0), true);
            DrawPoint(new Coordinate(0, 1), false);

            if (File.Exists("ScoreBoard"))
            {
                isDatabaseExists = true;
            }
            else
            {
                isDatabaseExists = false;
            }

        }

        // Shooting in game
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

        // Drawing multiple point like in ship placement state
        private void DrawPoints(Coordinate position, MyVector vector)
        {
            // TODO Check collision with other ships
            bool isValidPosition = true;
            if (vector.Way == Direction.Horizontal)
            {
                if (BattlefieldWidth - position.X < _currentShipSize)
                {
                    isValidPosition = false;
                }
            }
            else
            {
                if (BattlefieldHeight - position.Y < _currentShipSize)
                {
                    isValidPosition = false;
                }
            }

            if (isValidPosition)
            {
                var shape = new Rectangle();
                shape.Fill = Brushes.Yellow;
                var unitY = canvas.Width / BattlefieldWidth;
                var unitX = canvas.Height / BattlefieldHeight;
                shape.Width = unitY;
                shape.Height = unitX;
                Canvas.SetLeft(shape, unitX * position.X);
                Canvas.SetTop(shape, unitY * position.Y);
                canvas.Children.Add(shape);

                if (vector.Way == Direction.Horizontal)
                {
                    for (int i = 1; i < vector.Size; i++)
                    {
                        shape = new Rectangle();
                        shape.Fill = Brushes.Yellow;
                        shape.Width = unitY;
                        shape.Height = unitX;
                        Canvas.SetLeft(shape, unitX * position.X + unitX * i);
                        Canvas.SetTop(shape, unitY * position.Y);
                        canvas.Children.Add(shape);
                    }
                }
                else
                {
                    for (int i = 1; i < vector.Size; i++)
                    {
                        shape = new Rectangle();
                        shape.Fill = Brushes.Yellow;
                        shape.Width = unitY;
                        shape.Height = unitX;
                        Canvas.SetLeft(shape, unitX * position.X);
                        Canvas.SetTop(shape, unitY * position.Y + unitY * i);
                        canvas.Children.Add(shape);
                    }
                }
                _currentShipSize++;
                ShipToPlace.Content = $"Placing Ship {_currentShipSize}";
                // TODO: Hozzáadni a kirajzolt shippet a játékosunkhoz
            }
            else
            {
                MessageBox.Show("Invalid ship position!");
            }
        }

        private void CanvasClick(object sender, MouseButtonEventArgs e)
        {
            // IGame.getCurrentPlayer().getIBattleField().Shoots().Stream().Foreach( (coordinate) -> DrawPoint(coordinate))
            // Model -> IGame.NextPlayer().getIBattleField().CanvasClick(coordinate) hozzáadja a lövések listájához
            // csak akkor csináljon bármit ha nem egy már kirajzolt pontra kattintunk
            if (!_inShipPlacement)
            {

                if (!(e.OriginalSource is Rectangle))
                {
                    DrawPoint(GetMousePosition(), true);
                }
            }
            else
            {
                if (!(e.OriginalSource is Rectangle))
                {
                    DrawPoints(GetMousePosition(), new MyVector(_currentDirection, _currentShipSize));
                }
            }

            // TODO: Ha mind a 2 játékos shipje el van tárolva az utolsó CanvasClick-nél akkor lépjen vissza !_inShipPlacement-be és töntesse el a ShipPlacementGrid-et
        }

        private Coordinate GetMousePosition()
        {
            int x = (int)(Mouse.GetPosition(canvas).X / FieldSize);
            int y = (int)(Mouse.GetPosition(canvas).Y / FieldSize);
            return new Coordinate(x, y);
        }

        private async void NewGame(object sender, RoutedEventArgs e)
        {
            _game = new Game();
            // Set Player Names
            var newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
            //game.AddPlayer(newGameWindow.Player1Name);
            //game.AddPlayer(newGameWindow.Player2Name);
            player1Name.Text = newGameWindow.Player1Name;
            player2Name.Text = newGameWindow.Player2Name;

            shipPlacementGrid.Visibility = Visibility.Visible;
            ShipToPlace.Content = $"Place ship {_currentShipSize}";
            MessageBox.Show($"Ask {player2Name.Text} to turn away and start your shipplacement turn!");
            // Set Player 1 ships...
            _inShipPlacement = true;
        }

        private void SetVerticalPlaceMent(object sender, RoutedEventArgs e)
        {
            _currentDirection = Direction.Vertical;
        }

        private void SetHorizontalPlaceMent(object sender, RoutedEventArgs e)
        {
            _currentDirection = Direction.Horizontal;
        }

        private void Query(object sender, RoutedEventArgs e)
        {
            if (isDatabaseExists)
            {
                var queryWindow = new QueryWindow();
                queryWindow.ShowDialog();
            }
            else
            {
                // Hiba esetén!
                // PackageManager Console: Update-Database
                MessageBox.Show("The database is not exist, please generate it first!");
            }
        }

        private void DatabaseCheck(object sender, RoutedEventArgs e)
        {
            if (isDatabaseExists)
            {
                MessageBox.Show("Database is available!");
            }
            else
            {
                MessageBox.Show("There is no database!");
            }
        }
    }
}
