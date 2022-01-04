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

namespace Torpedo
{
    /// <summary>
    /// Interaction logic for ShipPlacementWindow.xaml
    /// </summary>
    public partial class ShipPlacementWindow : Window
    {
        public ShipPlacementWindow()
        {
            InitializeComponent();
        }

        public bool isHorizontal { get; private set; }
        private void GivePosition(object sender, RoutedEventArgs e)
        {
            if ((bool)Horizontal.IsChecked)
            {
                isHorizontal = true;
            }
            else
            {
                isHorizontal = false;
            }
            Close();
        }
    }
}
