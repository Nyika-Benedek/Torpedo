using System;
using System.Collections.Generic;
using System.Globalization;
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
using Torpedo.Entity;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo
{
    /// <summary>
    /// This represent what should drawn on the canvas
    /// </summary>
    public enum Type { Hit, Miss, Ship }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// How many cells in the battlefield in a row
        /// </summary>
        public const int BattlefieldWidth = 10;

        /// <summary>
        /// How many cells in the battlefield in a collumn
        /// </summary>
        public const int BattlefieldHeight = 10;

        /// <summary>
        /// Size of one cell (in pixels)
        /// </summary>
        private const int FieldSize = 50;

        // states
        private IGame _game;
        private int _currentShipSize = 2;
        private IShips.Direction _currentDirection = IShips.Direction.Horizontal;
        private bool _isDatabaseExists;
        private bool _isPlayer1 = true;
        private bool _isAI = false;
        private AI _ai = new AI();

        /// <summary>
        /// This window will open on the start.
        /// </summary>
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

        /// <summary>
        /// Making a square fill it up with the correct color by the type, then draw it on the canvas at the given coordinate
        /// </summary>
        /// <param name="position">The coordinate, where to draw</param>
        /// <param name="type">What type to draw(color)</param>
        private void DrawPoint(Coordinate position, Type type)
        {
            var shape = new Rectangle();
            if (type == Type.Miss)
            {
                shape.Fill = Brushes.LightBlue;
            }
            else if (type == Type.Ship)
            {
                shape.Fill = Brushes.Yellow;
            }
            else
            {
                shape.Fill = Brushes.Red;
            }
            var unitY = canvas.Width / BattlefieldWidth;
            var unitX = canvas.Height / BattlefieldHeight;
            shape.Width = unitY;
            shape.Height = unitX;
            Canvas.SetLeft(shape, unitX * position.X);
            Canvas.SetTop(shape, unitY * position.Y);
            canvas.Children.Add(shape);
        }

        /// <summary>
        /// Clear the canvas prom the Rectangles
        /// </summary>
        private void ClearCanvas()
        {
            foreach (UIElement child in canvas.Children)
            {
                if (child is Rectangle)
                {
                    child.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Draw the rectangles back based on the battlefield
        /// </summary>
        private void DrawBattlefield()
        {
            foreach ((Coordinate, bool) item in _game.CurrentPlayer.EnemyBattlefield.Shots)
            {
                Type fieldType;
                if (item.Item2)
                {
                    fieldType = Type.Hit;
                }
                else
                {
                    fieldType = Type.Miss;
                }

                DrawPoint(item.Item1, fieldType);
            }
        }

        /// <summary>
        /// Clear then redraw the battlefield
        /// </summary>
        private void RedrawCanvas()
        {
            ClearCanvas();
            DrawBattlefield();
        }

        /// <summary>
        /// Updates the Mainwindows scoreboard
        /// </summary>
        public void UpdateScoreBoard()
        {
            // TODO _game.Turn is not giving the current turn
            // Set current turn
            turnCounter.Text = $"Turn: {_game.Turn}";

            // Highlight current player, and set its score
            if (_game.CurrentPlayer.Name == player1Name.Text)
            {
                player1Name.Foreground = Brushes.Red;
                //player1Name.FontWeight = SystemFonts.;
                //player1Name.FontWeight = FontWeight.Bold;
                player2Name.Foreground = Brushes.Black;
                //MessageBox.Show(_game.CurrentPlayer.Name);
                player1Points.Text = Convert.ToString(value: _game.CurrentPlayer.Points, new NumberFormatInfo());

                // Set Remaining Units
                if (_game.CurrentPlayer.Battlefield != null)
                {
                    List<int> remainingShipsBuffer = _game.CurrentPlayer.Battlefield.RemainingShips();
                    string remainingShips = string.Join(' ', remainingShipsBuffer);
                    player1RemainingUnits.Text = remainingShips;
                }
            }
            else
            {
                player2Name.Foreground = Brushes.Red;
                player1Name.Foreground = Brushes.Black;
                player2Points.Text = Convert.ToString(value: _game.CurrentPlayer.Points, new NumberFormatInfo());

                // Set Remaining Units
                if (_game.CurrentPlayer.Battlefield != null)
                {
                    List<int> remainingShipsBuffer = _game.CurrentPlayer.Battlefield.RemainingShips();
                    string remainingShips = string.Join(' ', remainingShipsBuffer);
                    player2RemainingUnits.Text = remainingShips;
                }
            }
            _isPlayer1 = !_isPlayer1;
        }

        /// <summary>
        /// Calls the AI to get a recommended choice to place a ship
        /// </summary>
        /// <returns>A</returns>
        private (Coordinate, MyVector) AIPlacingOneShip() // TODO to AI
        {
            // TODO: Call the AI to get a random ship position
            return (new Coordinate(0, 0), new MyVector(IShips.Direction.Horizontal, 2));
        }

        /// <summary>
        /// Calls the AIPlacingOneShip() until its gives enough ship locations
        /// </summary>
        private void AIPlacingShips() //TODO to AI
        {
            while (_currentShipSize != 6)
            {
                (Coordinate position, MyVector vector) = AIPlacingOneShip();
                AddShipToAIBattlefield(position, vector);
            }
        }

        /// <summary>
        /// Its like <see cref="PlaceShip"/>, just without the interaction with the player
        /// </summary>
        /// <param name="position">Coordinate: starting point</param>
        /// <param name="vector">MyVector: (<see cref="IShips.Direction"/>, int: size) from the point</param>
        private void AddShipToAIBattlefield(Coordinate position, MyVector vector)
        {
            UpdateScoreBoard();
            IShips newShip = new Ship(position, vector);
            if (_game.CurrentPlayer.BattlefieldBuilder.TryToAddShip(newShip))
            {
                if (_currentShipSize == 5)
                {
                    _game.Start();
                    shipPlacementGrid.Visibility = Visibility.Collapsed;
                    VsAiLabel.Visibility = Visibility.Visible;
                    _ = MessageBox.Show("Let the battle begin!");
                    _game.NextPlayer();
                    UpdateScoreBoard();
                }
                _currentShipSize++;
            }
            ClearCanvas();
        }

        /// <summary>
        /// This method helps to build up the battlefields by adding ships to the current player's battlefield
        /// </summary>
        /// <param name="position">Coordinate where to start draw</param>
        /// <param name="vector">The vector to continue in one line</param>
        private void PlaceShip(Coordinate position, MyVector vector)
        {
            UpdateScoreBoard();
            IShips newShip = new Ship(position, vector);

            if (_game.CurrentPlayer.BattlefieldBuilder.TryToAddShip(newShip))
            {
                foreach (var part in newShip.Parts)
                {
                    DrawPoint(part, Type.Ship);
                }
                if (_currentShipSize == 5)
                {
                    if (_game.NextPlayer().BattlefieldBuilder.Ships.Count != 0)
                    {
                        _game.Start();
                        shipPlacementGrid.Visibility = Visibility.Collapsed;
                        ClearCanvas();
                        MessageBox.Show("Let the battle begin!");
                        _currentShipSize = 2;
                        UpdateScoreBoard();
                        return;
                    }
                    if (_isAI)
                    {
                        AIPlacingShips();
                    }
                    else
                    {
                        MessageBox.Show($"Ask {_game.NextPlayer().Name} to turn away and start your shipplacement turn!");
                        _game.NextPlayer();
                        ClearCanvas();
                        _currentShipSize = 1;
                    }
                }
                _currentShipSize++;
                ShipToPlace.Content = $"Place ship {_currentShipSize}";
            }
            else
            {
                _ = MessageBox.Show("Invalid Ship position");
            }
        }

        /// <summary>
        /// Calls th <see cref="AI"/> agent to get a shot
        /// </summary>
        /// <returns>A recommended shooting (<see cref="Coordinate"/>, bool: is hit any ship)</returns>
        private (Coordinate, bool) AIShoot()
        {
            // TODO: Get AI shoot recommendation
            // _game.CurrentPlayer.EnemyBattlefield.Shoot(shoot)
            return (new Coordinate(0, 0), true /*_game.CurrentPlayer.EnemyBattlefield.Shoot(shoot)*/);
        }

        /// <summary>
        /// Checks if the game is at win condicion
        /// </summary>
        private void PostWinCondition()
        {
            MessageBox.Show($"Congratulation {_game.CurrentPlayer.Name} you destroyed your enemies!");
            _game.State = GameState.Finished;
            DatabaseCommands database = new DatabaseCommands();
            database.AddEntry(new DatabaseModel(
                5,
                _game.Winner.Name,
                _game.CurrentPlayer.Name,
                _game.CurrentPlayer.Points,
                _game.NextPlayer().Name,
                _game.CurrentPlayer.Points));
        }

        /// <summary>
        /// Call the correct method after clicking on canvas based on which game phase are we currently in
        /// </summary>
        /// <param name="sender">The object we clicked(canvas)</param>
        /// <param name="e">Data of the mouse related event</param>
        private void CanvasClick(object sender, MouseButtonEventArgs e)
        {
            // Dont try to shoot if there is no game in progress
            if (_game is null || _game.State == GameState.Finished)
            {
                return;
            }

            if (_game.State == GameState.Battle)
            {
                if (!(e.OriginalSource is Rectangle))
                {
                    Coordinate shot = GetMousePosition();
                    if (_game.CurrentPlayer.EnemyBattlefield.Shoot(shot))
                    {
                        _game.CurrentPlayer.AddPoint();
                        DrawPoint(shot, Type.Hit);
                        DrawPoint(new Coordinate(0,0), Type.Hit);
                    }
                    else
                    {
                        DrawPoint(shot, Type.Miss);
                        DrawPoint(new Coordinate(0, 0), Type.Miss);
                    }
                    if (_game.IsEnded())
                    {
                        PostWinCondition();
                    }
                    else
                    {
                        UpdateScoreBoard();
                        _game.NextPlayer();
                        UpdateScoreBoard();
                        if (_isAI)
                        {
                            UpdateScoreBoard();
                            RedrawCanvas();
                            (Coordinate, bool) aiShot = AIShoot();
                            if (aiShot.Item2)
                            {
                                _game.CurrentPlayer.AddPoint();
                                DrawPoint(aiShot.Item1, Type.Hit);
                            }
                            else
                            {
                                DrawPoint(aiShot.Item1, Type.Miss);
                            }
                            DrawPoint(new Coordinate(5, 5), Type.Miss);
                            if (_game.IsEnded())
                            {
                                PostWinCondition();
                            }
                            _game.NextPlayer();
                        }
                    }
                    RedrawCanvas();
                }
                return;
            }

            if (_game.State == GameState.ShipPlacement)
            {
                if (!(e.OriginalSource is Rectangle))
                {
                    PlaceShip(GetMousePosition(), new MyVector(_currentDirection, _currentShipSize));
                }
                return;
            }

            // TODO: Ha mind a 2 játékos shipje el van tárolva az utolsó CanvasClick-nél akkor lépjen vissza !_inShipPlacement-be és töntesse el a ShipPlacementGrid-et
        }

        /// <summary>
        /// Takes the mouse position on the canvas and convert it into coordinate
        /// </summary>
        /// <returns>A coordinate which grid is the mouse in</returns>
        private Coordinate GetMousePosition()
        {
            int x = (int)(Mouse.GetPosition(canvas).X / FieldSize);
            int y = (int)(Mouse.GetPosition(canvas).Y / FieldSize);
            return new Coordinate(x, y);
        }

        /// <summary>
        /// This method starts the process of creating a new game
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
        private void NewGame(object sender, RoutedEventArgs e)
        {
            _currentShipSize = 2;
            ClearCanvas();
            _game = new Game();
            // Set Player Names
            var newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
            _game.AddPlayer(new Player(newGameWindow.Player1Name));
            _game.AddPlayer(new Player(newGameWindow.Player2Name));
            player1Name.Text = newGameWindow.Player1Name;
            player2Name.Text = newGameWindow.Player2Name;
            if (newGameWindow.Player2Name == "AI")
            {
                _isAI = true;
            }

            shipPlacementGrid.Visibility = Visibility.Visible;
            ShipToPlace.Content = $"Place ship {_currentShipSize}";
            // set the pointer to the first player
            _game.NextPlayer();
            if (!_isAI)
            {
                MessageBox.Show($"Ask {_game.NextPlayer().Name} to turn away and start your shipplacement turn!");
            }
            else
            {
                // TODO Generate AI's ship placement
                _game.NextPlayer();
            }
            // Set Player 1 ships...
            _game.NextPlayer();
            //UpdateScoreBoard();
        }

        /// <summary>
        /// Set the ship placement's direction to vertical
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
        private void SetVerticalPlacement(object sender, RoutedEventArgs e)
        {
            _currentDirection = IShips.Direction.Vertical;
        }

        /// <summary>
        /// Set the ship placement's direction to horizontal
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
        private void SetHorizontalPlacement(object sender, RoutedEventArgs e)
        {
            _currentDirection = IShips.Direction.Horizontal;
        }

        /// <summary>
        /// Opens the window to reach the database
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
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

        /// <summary>
        /// Checks is the Database is exists, then message the user about it's state
        /// </summary>
        /// <param name="sender">The object we clicked</param>
        /// <param name="e">Data of the mouse related event</param>
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

        /// <summary>
        /// If playing against AI pressing s will draw show it's ships location
        /// </summary>
        /// <param name="sender">The object we trigered</param>
        /// <param name="e">Data of the key related event</param>
        private void ShowAIShips(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                // TODO
                // if (typeof(_game.CurrentPlayer)!= AI)
                // foreach(AI.Ships)
                // {
                        //foreach(item.ship>parts)
                            //{ draw point}
                // }
                ClearCanvas();
                if (typeof(AI) != _game.CurrentPlayer)
                {
                    // TODO get AI ships and draw it
                    foreach (var AIships in _ai.Ships)
                    {
                        foreach (var parts in AIships.Parts)
                        {
                            DrawPoint(parts, Type.Ship);
                        }
                    }
                }
                //DrawPoint(new Coordinate(0, 0), Type.Hit);
            }
        }

        /// <summary>
        /// Like <see cref="ShowAIShips(object, KeyEventArgs)"/> just to hide it and redraw the player's battlefield
        /// </summary>
        /// <param name="sender">The object we trigered</param>
        /// <param name="e">Data of the key related event</param>
        private void HideAIShips(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                RedrawCanvas();
            }
        }
    }
}
