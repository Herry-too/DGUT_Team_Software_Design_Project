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

namespace DGUT_Team_Software_Project_WPF
{
    /// <summary>
    /// GamemodeChoose.xaml 的交互逻辑
    /// </summary>
    public partial class GamemodeChoose : Window
    {
        Button normal = new Button();
        Button online = new Button();
        Button ai = new Button();
        public int gamemode { get; set; } = 0;
        public GamemodeChoose()
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.Title = "Gamemode Choose";
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/src/img/icon.ico", UriKind.RelativeOrAbsolute));

            InitializeComponent();
            Grid buttongrid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                buttongrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            buttongrid.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/src/img/3button.png")));

            Grid.SetColumn(normal, 1);
            Grid.SetColumn(online, 2);
            Grid.SetColumn(ai, 3);
            var infoStyle = FindResource("infoButtonStyle") as Style;//Find the pre-written style in xaml

            normal.Style = infoStyle;
            online.Style = infoStyle;
            ai.Style = infoStyle;
            normal.FontSize = 16;
            normal.Height = 40;//It's Height and width
            normal.Width = 80;
            normal.VerticalAlignment = VerticalAlignment.Center;//In the Center of the space
            normal.HorizontalAlignment = HorizontalAlignment.Center;
            online.FontSize = 16;
            online.Height = 40;//It's Height and width
            online.Width = 80;
            online.VerticalAlignment = VerticalAlignment.Center;//In the Center of the space
            online.HorizontalAlignment = HorizontalAlignment.Center;
            ai.FontSize = 16;
            ai.Height = 40;//It's Height and width
            ai.Width = 80;
            ai.VerticalAlignment = VerticalAlignment.Center;//In the Center of the space
            ai.HorizontalAlignment = HorizontalAlignment.Center;

            normal.Content = "Normal";
            online.Content = "Online";
            ai.Content = "AI";
            normal.Click += Button_Click;
            online.Click += Button_Click;
            ai.Click += Button_Click;
            this.Content = buttongrid;
            buttongrid.Children.Add(normal);
            buttongrid.Children.Add(online);
            buttongrid.Children.Add(ai);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "Normal")
                gamemode = 1;
            if (button.Content.ToString() == "Online")
                gamemode = 2;
            if (button.Content.ToString() == "AI")
                gamemode = 3;
            this.Visibility = Visibility.Hidden;
        }
    }
}
