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
        TextBlock textInfoLine0 = new TextBlock();
        TextBlock textInfoLine1 = new TextBlock();
        Grid gameboardGrid = new Grid();
        Grid infoGrid = new Grid();
        Program program;

        public MainWindow()
        {
            InitializeComponent();

            InitGameBoard();
            

        }

        void InitGameBoard()
        {
            this.Height = 713;
            this.Width = 533;

            this.ResizeMode = System.Windows.ResizeMode.CanMinimize;        //Prohibit MainWindow changeSize

            Grid mainWindow = new Grid();//主窗口
            //下面为设置主窗口的分格
            ColumnDefinition columnDefinition0 = new ColumnDefinition();
            columnDefinition0.Width = new GridLength(92, GridUnitType.Star);

            ColumnDefinition columnDefinition1 = new ColumnDefinition();
            columnDefinition1.Width = new GridLength(527, GridUnitType.Star);

            ColumnDefinition columnDefinition2 = new ColumnDefinition();
            columnDefinition2.Width = new GridLength(92, GridUnitType.Star);

            RowDefinition rowDefinition0 = new RowDefinition();
            rowDefinition0.Height = new GridLength(1094, GridUnitType.Star);

            RowDefinition rowDefinition1 = new RowDefinition();
            rowDefinition1.Height = new GridLength(3517, GridUnitType.Star);

            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(218, GridUnitType.Star);

            RowDefinition rowDefinition3 = new RowDefinition();
            rowDefinition3.Height = new GridLength(650, GridUnitType.Star);

            RowDefinition rowDefinition4 = new RowDefinition();
            rowDefinition4.Height = new GridLength(226, GridUnitType.Star);

            mainWindow.ColumnDefinitions.Add(columnDefinition0);
            mainWindow.ColumnDefinitions.Add(columnDefinition1);
            mainWindow.ColumnDefinitions.Add(columnDefinition2);

            mainWindow.RowDefinitions.Add(rowDefinition0);
            mainWindow.RowDefinitions.Add(rowDefinition1);
            mainWindow.RowDefinitions.Add(rowDefinition2);
            mainWindow.RowDefinitions.Add(rowDefinition3);
            mainWindow.RowDefinitions.Add(rowDefinition4);
            //下面为设置信息窗口的分格
            ColumnDefinition infoColumnDef0 = new ColumnDefinition();
            infoColumnDef0.Width = new GridLength(1, GridUnitType.Star);

            ColumnDefinition infoColumnDef1 = new ColumnDefinition();
            infoColumnDef1.Width = new GridLength(2, GridUnitType.Star);

            ColumnDefinition infoColumnDef2 = new ColumnDefinition();
            infoColumnDef2.Width = new GridLength(1, GridUnitType.Star);

            infoGrid.ColumnDefinitions.Add(infoColumnDef0);
            infoGrid.ColumnDefinitions.Add(infoColumnDef1);
            infoGrid.ColumnDefinitions.Add(infoColumnDef2);

            for (int i = 0; i < 9; i++)
            {
                gameboardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                gameboardGrid.RowDefinitions.Add(new RowDefinition());
            }
            gameboardGrid.RowDefinitions.Add(new RowDefinition());//9列10行

            //下面为信息grid的中间的文字分格
            StackPanel infoStackPanel = new StackPanel();
            infoStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            infoStackPanel.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(infoStackPanel, 1);
            infoGrid.Children.Add(infoStackPanel);

            Grid.SetColumn(gameboardGrid, 1);
            Grid.SetColumn(infoGrid, 1);

            Grid.SetRow(gameboardGrid, 1);
            Grid.SetRow(infoGrid, 3);

            mainWindow.Children.Add(gameboardGrid);
            mainWindow.Children.Add(infoGrid);

            mainWindow.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/src/img/board.png")));
            

            textInfoLine0.Text = "Welcome!";
            textInfoLine0.FontSize = 20;
            textInfoLine0.HorizontalAlignment = HorizontalAlignment.Center;
            infoStackPanel.Children.Add(textInfoLine0);

            textInfoLine1.Text = "Waiting...";
            textInfoLine1.FontSize = 20;
            textInfoLine1.HorizontalAlignment = HorizontalAlignment.Center;
            infoStackPanel.Children.Add(textInfoLine1);
            //设置左右两个按钮
            var infoStyle = FindResource("infoButtonStyle") as Style;
            Button leftButton = new Button();
            leftButton.Content = "CLOSE";
            leftButton.Style = infoStyle;
            leftButton.Click += new RoutedEventHandler(left_button_Click);
            Grid.SetColumn(leftButton, 0);

            Button rightButton = new Button();
            rightButton.Content = "START";
            rightButton.Style = infoStyle;
            rightButton.Click += new RoutedEventHandler(right_button_Click);
            Grid.SetColumn(rightButton, 2);

            infoGrid.Children.Add(leftButton);
            infoGrid.Children.Add(rightButton);

            this.Content = mainWindow;
            
        }
        void left_button_Click(object sender, EventArgs e)
        {
            Button leftButton = (Button)sender;
            switch (leftButton.Content)
            {
                case "CLOSE":
                    MessageBox.Show("Bye!");
                    this.Close();
                    break;
                case "UNDO":
                    break;
            }
        }

        void right_button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Start Now!");
            start_game();
            update_gameboard();
        }

        void start_game()
        {
            program = new Program();
        }

        void update_gameboard()
        {
            var redStyle = FindResource("RedButton") as Style;
            var blackStyle = FindResource("BlackButton") as Style;
            gameboardGrid.Children.Clear();//清空
            for(int i = 0; i < 10; i++)//Row, 行
            {
                for(int j = 0; j < 9; j++)//Column,列
                {
                    if(program.GetBoard().getPieceName(i,j) != "")//有棋子
                    {
                        Button button = new Button();
                        button.Content = program.GetBoard().getPieceName(i, j);
                        if(program.GetBoard().getPiecePlayer(i,j) == "red")
                        {
                            button.Style = redStyle;
                        }
                        else
                        {
                            button.Style = blackStyle;
                        }
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        gameboardGrid.Children.Add(button);
                    }
                }
            }
        }
    }
}
