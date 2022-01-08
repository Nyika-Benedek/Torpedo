using System;
using System.Collections.Generic;
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
        private bool _inShipPlacement = false;
        private int _currentShipSize = 2;
        private IShips.Direction _currentDirection = IShips.Direction.Horizontal;

        public MainWindow()
        {
            InitializeComponent();

            DrawPoint(new Coordinate(0, 0), Type.Miss);
            DrawPoint(new Coordinate(0, 1), Type.Hit);

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

        // Drawing multiple point like in ship placement state
        private void DrawPoints(Coordinate position, MyVector vector)
        {
            // TODO Check collision with other ships
            bool isValidPosition = true;
            if (vector.Way == IShips.Direction.Horizontal)
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
            }
        }

        private void CanvasClick(object sender, MouseButtonEventArgs e)
        {
            
            if (!_inShipPlacement)
            {
                // _game.CurrentPlayer.BattleField.Shoots().Stream().Foreach( (coordinate) -> DrawPoint(coordinate, type))
                // _game.NextPlayer().BattleField.Shoot(coordinate) hozzáadja a lövések listájához
                // csak akkor csináljon bármit ha nem egy már kirajzolt pontra kattintunk

                if (!(e.OriginalSource is Rectangle))
                {
                    DrawPoint(GetMousePosition(), Type.Miss);
                    //TODO to return a value with IsHit
                    
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
            _game.AddPlayer(new Player(newGameWindow.Player1Name));
            _game.AddPlayer(new Player (newGameWindow.Player2Name));
            player1Name.Text = newGameWindow.Player1Name;
            player2Name.Text = newGameWindow.Player2Name;

            shipPlacementGrid.Visibility = Visibility.Visible;
            ShipToPlace.Content = $"Place ship {_currentShipSize}";
            _game.NextPlayer();
            MessageBox.Show($"Ask {_game.CurrentPlayer.Name} to turn away and start your shipplacement turn!");
            // Set Player 1 ships...
            _inShipPlacement = true;
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
            var queryWindow = new QueryWindow();
            queryWindow.ShowDialog();
            //throw new NotImplementedException();
        }

        // TODO Implement Entity Framework
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
