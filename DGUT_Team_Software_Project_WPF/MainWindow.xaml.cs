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
        private delegate void UpdateUIDelegate();

        TextBlock textInfoLine0 = new TextBlock();//Information Line 1
        TextBlock textInfoLine1 = new TextBlock();//Information Line 2
        Grid gameboardGrid = new Grid();//gameboard grid(child grid of main grid)
        Grid infoGrid = new Grid();//infomation Grid, contain two button and two information line
        Program program;//A bridge (or converter) to console versions of code
        NetworkProgram networkProgram;
        System.Timers.Timer networkTimer = new System.Timers.Timer(5000);
        Button leftButton = new Button();
        Button rightButton = new Button();
        enum Lock
        {
            locked,
            unlock
        };
        Lock boardlocker = Lock.unlock;
        public MainWindow()
        {
            InitializeComponent();

            InitGameBoard();
            

        }

        void InitGameBoard()
        {
            //Set the Height and Width of this program
            this.Height = 713;
            this.Width = 533;
            
            //Set Title and Icon
            this.Title = "Chinese Chess";
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/src/img/icon.ico", UriKind.RelativeOrAbsolute));

            this.ResizeMode = System.Windows.ResizeMode.CanMinimize;        //Prevent users from modifying the window size

            Grid mainWindow = new Grid();//Create the main Grid
            /**
            Set the Column and Row of main Grid
            According to the UI design scheme, it is divided into three columns and five rows.
            The width ratio of the three columns is 92:527:92;
            The height ratio of the 5 rows is 1094:3517:218:650:226.(From top to bottom)
            **/
            ColumnDefinition columnDefinition0 = new ColumnDefinition();//Create a Column Definition, the same below
            columnDefinition0.Width = new GridLength(92, GridUnitType.Star);//Set the width ratio,the same below

            ColumnDefinition columnDefinition1 = new ColumnDefinition();
            columnDefinition1.Width = new GridLength(527, GridUnitType.Star);

            ColumnDefinition columnDefinition2 = new ColumnDefinition();
            columnDefinition2.Width = new GridLength(92, GridUnitType.Star);

            RowDefinition rowDefinition0 = new RowDefinition();//Create a Row definition, same below
            rowDefinition0.Height = new GridLength(1094, GridUnitType.Star);//Set the height ratio, same below

            RowDefinition rowDefinition1 = new RowDefinition();
            rowDefinition1.Height = new GridLength(3517, GridUnitType.Star);

            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(218, GridUnitType.Star);

            RowDefinition rowDefinition3 = new RowDefinition();
            rowDefinition3.Height = new GridLength(650, GridUnitType.Star);

            RowDefinition rowDefinition4 = new RowDefinition();
            rowDefinition4.Height = new GridLength(226, GridUnitType.Star);

            mainWindow.ColumnDefinitions.Add(columnDefinition0);//Add it to the Window Definition, same as below
            mainWindow.ColumnDefinitions.Add(columnDefinition1);
            mainWindow.ColumnDefinitions.Add(columnDefinition2);

            mainWindow.RowDefinitions.Add(rowDefinition0);
            mainWindow.RowDefinitions.Add(rowDefinition1);
            mainWindow.RowDefinitions.Add(rowDefinition2);
            mainWindow.RowDefinitions.Add(rowDefinition3);
            mainWindow.RowDefinitions.Add(rowDefinition4);

            //Sst the Column of the information Grid, same as above
            //there are two buttons are on both sides of the grid, and two lineinfo in the center of the grid.
            //Column: 1:2:1
            ColumnDefinition infoColumnDef0 = new ColumnDefinition();
            infoColumnDef0.Width = new GridLength(1, GridUnitType.Star);

            ColumnDefinition infoColumnDef1 = new ColumnDefinition();
            infoColumnDef1.Width = new GridLength(2, GridUnitType.Star);

            ColumnDefinition infoColumnDef2 = new ColumnDefinition();
            infoColumnDef2.Width = new GridLength(1, GridUnitType.Star);

            infoGrid.ColumnDefinitions.Add(infoColumnDef0);
            infoGrid.ColumnDefinitions.Add(infoColumnDef1);
            infoGrid.ColumnDefinitions.Add(infoColumnDef2);

            //Set the Column and Row of the gameboard Grid
            for (int i = 0; i < 9; i++)//Add 9 Rows and 9 Column
            {
                gameboardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                gameboardGrid.RowDefinitions.Add(new RowDefinition());
            }
            gameboardGrid.RowDefinitions.Add(new RowDefinition());//We need 10 Row

            //Use StackPanel to typeset two textInfoLine
            StackPanel infoStackPanel = new StackPanel();
            infoStackPanel.HorizontalAlignment = HorizontalAlignment.Center;//Set it as the center of the Horizontal
            infoStackPanel.VerticalAlignment = VerticalAlignment.Center;//Set it as the center of the Vertical
            Grid.SetColumn(infoStackPanel, 1);//Set Column
            infoGrid.Children.Add(infoStackPanel);//Add it to the infoGrid

            //Set subGrid's column and row
            Grid.SetColumn(gameboardGrid, 1);
            Grid.SetColumn(infoGrid, 1);

            Grid.SetRow(gameboardGrid, 1);
            Grid.SetRow(infoGrid, 3);

            //And add them to the mainGrid
            mainWindow.Children.Add(gameboardGrid);
            mainWindow.Children.Add(infoGrid);

            //Set the mainGrid's background to the gameboard picture
            mainWindow.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/src/img/board.png")));
            
            //Init the first infoLine
            textInfoLine0.Text = "Welcome!";//Set the InfoLine's Default Text
            textInfoLine0.FontSize = 20;//It's FontSize
            textInfoLine0.HorizontalAlignment = HorizontalAlignment.Center;//Set it as the center of the Horizontal
            infoStackPanel.Children.Add(textInfoLine0);//Add it to the stackPanel
            //And the second one, same as above
            textInfoLine1.Text = "Waiting...";
            textInfoLine1.FontSize = 18;
            textInfoLine1.VerticalAlignment = VerticalAlignment.Center;
            textInfoLine1.HorizontalAlignment = HorizontalAlignment.Center;
            textInfoLine1.TextAlignment = TextAlignment.Center;
            infoStackPanel.Children.Add(textInfoLine1);

            //Set two buttons
            var infoStyle = FindResource("infoButtonStyle") as Style;//Find the pre-written style in xaml
            leftButton.Content = "CLOSE";//Default Text
            leftButton.FontSize = 16;
            leftButton.Style = infoStyle;//Set the Style of this button
            leftButton.Height = 40;//It's Height and width
            leftButton.Width = 80;
            leftButton.VerticalAlignment = VerticalAlignment.Center;//In the Center of the space
            leftButton.HorizontalAlignment = HorizontalAlignment.Center;
            leftButton.Click += new RoutedEventHandler(left_button_Click);//Add a Event
            Grid.SetColumn(leftButton, 0);//Set it should be the left of the infoGrid

            rightButton.Content = "START";
            rightButton.FontSize = 16;

            rightButton.Style = infoStyle;
            rightButton.Height = 40;
            rightButton.Width = 80;
            rightButton.VerticalAlignment = VerticalAlignment.Center;
            rightButton.HorizontalAlignment = HorizontalAlignment.Center;
            rightButton.Click += new RoutedEventHandler(right_button_Click);
            Grid.SetColumn(rightButton, 2);

            infoGrid.Children.Add(leftButton);
            infoGrid.Children.Add(rightButton);

            this.Content = mainWindow;//Set the content to the mainGrid.
            
        }
        void left_button_Click(object sender, EventArgs e)
        {
            Button leftButton = (Button)sender;
            switch (leftButton.Content)
            {
                case "CLOSE":
                    this.Close();//Just Close the program.
                    break;
                case "UNDO"://Actually I want to write an Undo function previously...
                    if (!program.undoBoard())
                        MessageBox.Show("No History Record!");
                    update_gameboard();
                    break;
            }
        }

        private void NetworkTimerTask(object sender, System.Timers.ElapsedEventArgs e)
        {
            networkTimer.Stop();
            if (networkProgram.getPlayer() != networkProgram.GetBoard().getPlayer() && boardlocker == Lock.unlock)
            {
                networkProgram.updateStatus();
                if (networkProgram.getPlayer() == networkProgram.GetBoard().getPlayer())
                {
                    UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(update_network_gameboard);
                    this.Dispatcher.Invoke(updateUIDelegate);
                }
            }
            networkTimer.Start();
        }

        void right_button_Click(object sender, EventArgs e)
        {
            start_game();//Start Game
        }

        void start_game()
        {
            GamemodeChoose gamemodeChoose = new GamemodeChoose();
            gamemodeChoose.ShowDialog();
            switch (gamemodeChoose.gamemode)
            {
                case 1:
                    program = new Program();//Create the bridge, the bridge will create a Gameboard
                    rightButton.Content = "RESET";
                    leftButton.Content = "UNDO";
                    update_gameboard();
                    break;
                case 2:
                    networkProgram = new NetworkProgram();
                    rightButton.Visibility = Visibility.Hidden;
                    leftButton.Visibility = Visibility.Hidden;
                    networkTimer.Elapsed += NetworkTimerTask;
                    networkTimer.Enabled = true;
                    networkTimer.AutoReset = true;
                    networkTimer.Start();
                    update_network_gameboard();
                    break;
            }
            gamemodeChoose.Close();
        }

        void update_gameboard()//Normal gameboard
        {
            if(boardlocker == Lock.locked)
            {
                return;
            }
            boardlocker = Lock.locked;
            /**
            There are 6 styles in this program, Which located in the xaml file
            1 for the information Button, such as the "START" Button
            4 for the pieces, each color of the pieces have two styles determines whether they are highlighted when the mouse slides over
            1 for the Empty Button, when there is no pieces we still need a button to handle the position selection, which is transparent.
            **/

            //Just mark the styles
            var redStyle = FindResource("RedButton") as Style;
            var blackStyle = FindResource("BlackButton") as Style;
            var EmptyStyle = FindResource("EmptyButton") as Style;
            if (program.GetBoard().getPlayer() == Piece.Players.red)// If now is Red player's turn, Blackbutton should not be highlighted
            {
                blackStyle = FindResource("BlackButtonNoLight") as Style;
            }
            else//And vice versa
            {
                redStyle = FindResource("RedButtonNoLight") as Style;
            }
            if (!program.GetBoard().getGameStatus())//If gameover, no pieces could be selected and no highlighted.
            {
                blackStyle = FindResource("BlackButtonNoLight") as Style;
                redStyle = FindResource("RedButtonNoLight") as Style;
            }
            gameboardGrid.Children.Clear();//Clear all the Old Children, they are old pieces.
            for (int i = 0; i < 10; i++)//Each Row
            {
                for (int j = 0; j < 9; j++)//Each Column
                {
                    //IF:
                    //Already selected the pieces AND
                    //(No piece in this position OR this piece is another player) AND
                    //The selected pieces could move to this place
                    if (program.GetBoard().getSelectedX() != -1 &&
                        (program.GetBoard().getPieces()[i,j] == null || program.GetBoard().getPieces()[i, j].getPlayer() != program.GetBoard().getPlayer())
                        && program.GetBoard().getPieces()[program.GetBoard().getSelectedX(), program.GetBoard().getSelectedY()]
                        .ValidMoves(i, j, program.GetBoard()))
                    {
                        Rectangle rectangle = new Rectangle();//Create a Rectangle to Suggest pieces Location.
                        rectangle.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/src/img/flag.png")));//Fill it with pic.
                        Grid.SetRow(rectangle, i);
                        Grid.SetColumn(rectangle, j);
                        gameboardGrid.Children.Add(rectangle);//Add it to the right place.
                    }

                    if (program.GetBoard().getPieceName(i, j) != "")//If there are piece in this position
                    {
                        Button button = new Button();
                        button.Content = program.GetBoard().getPieceName(i, j);//button's name from the piece
                        if (program.GetBoard().getPiecePlayer(i, j) == Piece.Players.red)
                        {
                            button.Style = redStyle;//Set it to red style
                        }
                        else
                        {
                            button.Style = blackStyle;//Set it to black style
                        }
                        button.Click += piece_Click;//Add a Event
                        button.FontSize = 24;//Set font size
                        button.FontFamily = new FontFamily("隶书");//Set Font Family to LISHU
                        button.Tag = new int[] { i, j };//Create a int array to store the location and set it to the tag.
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        gameboardGrid.Children.Add(button);
                    }
                    else//If not
                    {
                        Button button = new Button();
                        button.Click += piece_Click;
                        button.Style = EmptyStyle;//Transparent button
                        button.Tag = new int[] { i, j };
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        gameboardGrid.Children.Add(button);
                    }
                }
            }

            textInfoLine0.Foreground = Brushes.Black;
            if (!program.GetBoard().getGameStatus())//If game over
            {
                textInfoLine1.Foreground = Brushes.Red;
                textInfoLine1.Text = "GAME OVER!";
                return;
            }

            if (program.GetBoard().getSelectedX() == -1)//If not select
            {
                textInfoLine1.Foreground = Brushes.Black;
                textInfoLine1.Text = "Please Select...";
            }
            else
            {
                textInfoLine1.Foreground = Brushes.Black;
                textInfoLine1.Text = "Please Move...";
            }
            if (program.GetBoard().ifDeliveredCheck())//If delivered a check
            {
                textInfoLine1.Foreground = Brushes.Red;
                textInfoLine1.Text += "\nDELIVERED A CHECK!";
            }
            if (program.GetBoard().getPlayer() == Piece.Players.red)//If the player is red
            {
                textInfoLine0.Foreground = Brushes.Red;
                textInfoLine0.Text = "Red Player";
            }
            else
            {
                textInfoLine0.Foreground = Brushes.Black;
                textInfoLine0.Text = "Black Player";
            }
            boardlocker = Lock.unlock;
        }

        private void piece_Click(object sender, RoutedEventArgs e)//Normal Gamemode Piece Clicked function
        {
            Button button = (Button)sender;
            int[] data = (int[])button.Tag;//get column and row
            program.pieceClick(data[1], data[0]);//Let the bridge to solve this selection
            update_gameboard();
        }

        private void piece_Network_Click(object sender, RoutedEventArgs e)//When Network Gamemode, piece clicked function
        {
            if(boardlocker == Lock.locked)
            {
                return;
            }
            boardlocker = Lock.locked;

            Button button = (Button)sender;
            int[] data = (int[])button.Tag;//get column and row
            networkProgram.pieceClick(data[1], data[0]);//Let the bridge to solve this selection
            boardlocker = Lock.unlock;
            update_network_gameboard();
        }
        void update_network_gameboard()//Similar to udpate_gameboard but optimized network access (such as adding locks to prevent information conflicts)
        {
            if (boardlocker == Lock.locked)
            {
                return;
            }
            boardlocker = Lock.locked;
            //Just mark the styles
            var redStyle = FindResource("RedButtonNoLight") as Style;
            var blackStyle = FindResource("BlackButtonNoLight") as Style;
            var EmptyStyle = FindResource("EmptyButton") as Style;
            if (networkProgram.GetBoard().getPlayer() == networkProgram.getPlayer())// If now is Current player's turn, hightlighted.
            {
                if (networkProgram.getPlayer() == Piece.Players.red)
                {
                    redStyle = FindResource("RedButton") as Style;
                }
                else
                {
                    blackStyle = FindResource("BlackButton") as Style;
                }
            }
            if (!networkProgram.GetBoard().getGameStatus())//If gameover, no pieces could be selected and no highlighted.
            {
                blackStyle = FindResource("BlackButtonNoLight") as Style;
                redStyle = FindResource("RedButtonNoLight") as Style;
            }
            gameboardGrid.Children.Clear();//Clear all the Old Children, they are old pieces.
            for (int i = 0; i < 10; i++)//Each Row
            {
                for (int j = 0; j < 9; j++)//Each Column
                {
                    //IF:
                    //Already selected the pieces AND
                    //(No piece in this position OR this piece is another player) AND
                    //The selected pieces could move to this place
                    if (networkProgram.GetBoard().getSelectedX() != -1 &&
                        (networkProgram.GetBoard().getPieces()[i, j] == null || networkProgram.GetBoard().getPieces()[i, j].getPlayer() != networkProgram.GetBoard().getPlayer())
                        && networkProgram.GetBoard().getPieces()[networkProgram.GetBoard().getSelectedX(), networkProgram.GetBoard().getSelectedY()]
                        .ValidMoves(i, j, networkProgram.GetBoard()))
                    {
                        Rectangle rectangle = new Rectangle();//Create a Rectangle to Suggest pieces Location.
                        rectangle.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/src/img/flag.png")));//Fill it with pic.
                        Grid.SetRow(rectangle, i);
                        Grid.SetColumn(rectangle, j);
                        gameboardGrid.Children.Add(rectangle);//Add it to the right place.
                    }

                    if (networkProgram.GetBoard().getPieceName(i, j) != "")//If there are piece in this position
                    {
                        Button button = new Button();
                        button.Content = networkProgram.GetBoard().getPieceName(i, j);//button's name from the piece
                        if (networkProgram.GetBoard().getPiecePlayer(i, j) == Piece.Players.red)
                        {
                            button.Style = redStyle;//Set it to red style
                        }
                        else
                        {
                            button.Style = blackStyle;//Set it to black style
                        }
                        button.Click += piece_Network_Click;//Add a Event
                        button.FontSize = 24;//Set font size
                        button.FontFamily = new FontFamily("隶书");//Set Font Family to LISHU
                        button.Tag = new int[] { i, j };//Create a int array to store the location and set it to the tag.
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        gameboardGrid.Children.Add(button);
                    }
                    else//If not
                    {
                        Button button = new Button();
                        button.Click += piece_Network_Click;
                        button.Style = EmptyStyle;//Transparent button
                        button.Tag = new int[] { i, j };
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        gameboardGrid.Children.Add(button);
                    }
                }
            }

            if (networkProgram.getPlayer() == Piece.Players.red)
            {
                textInfoLine0.Foreground = Brushes.Red;
                textInfoLine0.Text = "You are Red Player";
            }
            else
            {
                textInfoLine0.Foreground = Brushes.Black;
                textInfoLine0.Text = "You are Black Player";
            }

            if(networkProgram.getPlayer() == networkProgram.GetBoard().getPlayer())
            {
                if (networkProgram.GetBoard().getSelectedX() == -1)
                {
                    textInfoLine1.Text = "Please Select...";
                }
                else
                {
                    textInfoLine1.Text = "Please Move...";
                }
                if (networkProgram.GetBoard().ifDeliveredCheck())//If delivered a check
                {
                    textInfoLine1.Foreground = Brushes.Red;
                    textInfoLine1.Text += "\nDELIVERED A CHECK!";
                }
            }
            else
            {
                textInfoLine1.Text = "Waiting other Player...";
            }

            if (!networkProgram.GetBoard().getGameStatus())//If game over
            {
                textInfoLine1.Foreground = Brushes.Red;
                textInfoLine1.Text = "GAME OVER!";
                return;
            }
            boardlocker = Lock.unlock;
        }
    }
}
