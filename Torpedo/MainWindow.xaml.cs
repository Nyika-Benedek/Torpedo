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

        private void DrawPoint(Coordinate position, Boolean isHit) 
        {
            var shape = new Rectangle();
            if (isHit)
                shape.Fill = Brushes.Red;
            else
                shape.Fill = Brushes.LightBlue;

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
            //csak akkor csináljon bármit ha nem egy már kirajzolt pontra kattintunk
            if (!(e.OriginalSource is Rectangle))
            {
                DrawPoint(GetMousePosition(e), true);
            }
        }

        private Coordinate GetMousePosition(MouseButtonEventArgs e)
        {
            int x = (int)(Mouse.GetPosition(canvas).X / FieldSize);
            int y = (int)(Mouse.GetPosition(canvas).Y / FieldSize);
            return new Coordinate(x, y);
        }
    }
}
