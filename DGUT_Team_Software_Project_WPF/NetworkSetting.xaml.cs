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
    /// NetworkSetting.xaml 的交互逻辑
    /// </summary>
    public partial class NetworkSetting : Window
    {
        NetworkProgram networkProgram;

        public NetworkSetting(object networkProgram)
        {
            this.networkProgram = (NetworkProgram)networkProgram;

            InitializeComponent();
        }

        public void setRoomid(string str)
        {
            roomid.Text = str;
            roomid.IsReadOnly = true;
        }
        public void setKeygen(string str)
        {
            keygen.Text = str;
            keygen.IsReadOnly = true;
        }

        private void createroom_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            (string id,string keygen) = networkProgram.createRoom();
            setRoomid(id);
            setKeygen(keygen);
            joinroom.Content = "Done";
            MessageBox.Show("Please copy Room ID and Keygen to another player!");
        }

        private void joinroom_Click(object sender, RoutedEventArgs e)
        {
            networkProgram.setRoomid(roomid.Text);
            networkProgram.setmyKeygen(keygen.Text);
            if (networkProgram.updateStatus())
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Error! Could not Find this room!");
            }
        }
    }
}
