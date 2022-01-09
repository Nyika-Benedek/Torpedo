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
        private enum Type { Hit, Miss, Ship}

        // states
        private IGame _game;
        //private bool _inShipPlacement = false;
        private int _currentShipSize = 2;
        private IShips.Direction _currentDirection = IShips.Direction.Horizontal;
        private bool _isDatabaseExists;

        public MainWindow()
        {
            InitializeComponent();

            DrawPoint(new Coordinate(0, 0), Type.Miss);
            DrawPoint(new Coordinate(0, 1), Type.Hit);

            if (File.Exists("ScoreBoard"))
            {
                _isDatabaseExists = true;
            }
            else
            {
                _isDatabaseExists = false;
            }

        }

        // Shooting in game
        private void DrawPoint(Coordinate position, Type type)
        {
            var shape = new Rectangle();
            if (type == Type.Miss)
            {
                shape.Fill = Brushes.Red;
            }
            else if (type == Type.Ship)
            {
                shape.Fill = Brushes.LightBlue;
            }
            else
            {
                shape.Fill = Brushes.Yellow;
            }
            var unitY = canvas.Width / BattlefieldWidth;
            var unitX = canvas.Height / BattlefieldHeight;
            shape.Width = unitY;
            shape.Height = unitX;
            Canvas.SetLeft(shape, unitX * position.X);
            Canvas.SetTop(shape, unitY * position.Y);
            canvas.Children.Add(shape);
        }

        private void PlaceShip(Coordinate position, MyVector vector)
        {
            bool isValidPosition = true;

            if (vector.Way == IShips.Direction.Horizontal)
            {
                // Too close to the right side of the battlefield
                if (BattlefieldWidth - position.X < _currentShipSize)
                {
                    isValidPosition = false;
                }
            }
            else
            {
                // Too close to the bottom side of the battlefield
                if (BattlefieldHeight - position.Y < _currentShipSize)
                {
                    isValidPosition = false;
                }
            }

            var ships = _game.CurrentPlayer.BattlefieldBuilder.Ships;
            var shipPositions = new List<Coordinate>(14);

            foreach (var ship in ships)
            {
                shipPositions.AddRange(ship.Parts);
            }

            IShip newShip = new Ship(position, vector);

            // Checkin if there is a collision with other already placed whips
            foreach (var shipPart in shipPositions)
            {
                for (int i = 1; i <= vector.Size; i++)
                {
                    if (position == shipPart)
                    {
                        isValidPosition = false;
                    }
                }
            }

            if (isValidPosition)
            {
                _game.CurrentPlayer.BattlefieldBuilder.AddShip(new Ship(position, vector));
                DrawPoint(position, Type.Ship);

                if (vector.Way == IShips.Direction.Horizontal)
                {
                    for (int i = 1; i < vector.Size; i++)
                    {
                        position.X++;
                        //DrawPoint()
                    }
                }
                else
                {
                    for (int i = 1; i < vector.Size; i++)
                    {

                    }
                }
                _currentShipSize++;
                ShipToPlace.Content = $"Placing Ship {_currentShipSize}";
                
            }
        }
            
            /*
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
                //TODO (Beninek) DrawPoint(new Coordinate(), Type.Ship);

                if (vector.Way == IShips.Direction.Horizontal)
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
                _game.CurrentPlayer.BattlefieldBuilder.AddShip(new Ship(GetMousePosition(), vector));
            }
            else
            {
                MessageBox.Show("Invalid ship position!");
            }*/

        private void CanvasClick(object sender, MouseButtonEventArgs e)
        {
            if (_game.State == GameState.Battle)
            {
                if (!(e.OriginalSource is Rectangle))
                {
                    Coordinate shot = GetMousePosition();
                    _game.CurrentPlayer.EnemyBattlefield.Shoot(shot);
                }
                _game.NextPlayer();

                // TODO kirajzolni a következő játékos számára az ellenfél csatamezőjét
            }
            else
            {
                if (!(e.OriginalSource is Rectangle))
                {
                    PlaceShip(GetMousePosition(), new MyVector(_currentDirection, _currentShipSize));
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
            _game.AddPlayer(new Player(newGameWindow.Player1Name));
            _game.AddPlayer(new Player (newGameWindow.Player2Name));
            player1Name.Text = newGameWindow.Player1Name;
            player2Name.Text = newGameWindow.Player2Name;

            shipPlacementGrid.Visibility = Visibility.Visible;
            ShipToPlace.Content = $"Place ship {_currentShipSize}";
            _game.NextPlayer();
            MessageBox.Show($"Ask {_game.CurrentPlayer.Name} to turn away and start your shipplacement turn!");
            // Set Player 1 ships...
        }

        private void SetVerticalPlaceMent(object sender, RoutedEventArgs e)
        {
            _currentDirection = IShips.Direction.Vertical;
        }

        private void SetHorizontalPlaceMent(object sender, RoutedEventArgs e)
        {
            _currentDirection = IShips.Direction.Horizontal;
        }

        private void Query(object sender, RoutedEventArgs e)
        {
            if (_isDatabaseExists)
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
            if (_isDatabaseExists)
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
