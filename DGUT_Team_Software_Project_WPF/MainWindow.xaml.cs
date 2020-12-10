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

namespace DGUT_Team_Software_Project_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Height = 600;
            this.Width = 450;
            Grid mainWindow = new Grid();
            mainWindow.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/src/img/board.png")));
            this.Content = mainWindow;
        }
    }
}
