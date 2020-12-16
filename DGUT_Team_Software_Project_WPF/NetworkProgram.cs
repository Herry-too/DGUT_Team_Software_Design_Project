using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;

namespace DGUT_Team_Software_Project_WPF
{
    class NetworkProgram
    {
        GameBoard board = new GameBoard();
        WebClient MyWebClient = new WebClient();
        public string myKeygen;
        public string roomid;
        private bool MainPlayer = false;
        NetworkSetting networksetting;
        public NetworkProgram()
        {
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;
            networksetting = new NetworkSetting(this);
            networksetting.ShowDialog();
        }

        public Piece.Players getPlayer()
        {
            if (MainPlayer)
                return Piece.Players.red;
            return Piece.Players.black;
        }
        public GameBoard GetBoard()
        {
            return board;//Return the board
        }

        public void setmyKeygen(string str)
        {
            myKeygen = str;
        }
        public void setRoomid(string str)
        {
            roomid = str;
        }

        public bool updateStatus()
        {
            if(roomid == "" || myKeygen == "")
            {
                return false;
            }
            try
            {
                Byte[] pageData = MyWebClient.DownloadData("https://xiangqi.dci.herry001.cc/getRoom?roomid="
                    + roomid);
                string pageHtml = Encoding.UTF8.GetString(pageData);
                JObject obj = JObject.Parse(pageHtml);
                string boardjson = obj["json"].ToString();
                setBoard(boardjson);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool updateJson()
        {
            string msg;
            try
            {
                string jsonstring = System.Web.HttpUtility.UrlEncode(board.toJson().Replace("\n", "").Replace("\r", "").Replace(" ", ""));
                Byte[] pageData = MyWebClient.DownloadData("https://xiangqi.dci.herry001.cc/updateJson?roomid="
                    + roomid + "&keygen=" + myKeygen + "&json=" + jsonstring);
                string pageHtml = Encoding.UTF8.GetString(pageData);
                JObject obj = JObject.Parse(pageHtml);
                msg = obj["status"].ToString();
            }
            catch (Exception)
            {
                return false;
            }
            if(msg == "success")
            {
                return true;
            }
            return false;
        }
        public void setBoard(string str)
        {
            board = JsonConvert.DeserializeObject<GameBoard>(str, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
        public (string id,string keygen) createRoom()
        {
            string jsonstring = board.toJson().Replace("\n","").Replace("\r","").Replace(" ","");
            try
            {
                Byte[] pageData = MyWebClient.DownloadData("https://xiangqi.dci.herry001.cc/createRoom?default_json="
                    + System.Web.HttpUtility.UrlEncode(jsonstring));
                string pageHtml = Encoding.UTF8.GetString(pageData);
                JObject obj = JObject.Parse(pageHtml);
                string Keygen = obj["room_keygen"].ToString();
                string Roomid = obj["room_id"].ToString();
                MainPlayer = true;
                return (Roomid, Keygen);
            }catch (Exception)
            {
                return (null, null);
            }
        }

        string intArrtoStr(int column, int row)//Analog keyboard input
        {
            string returnStr = "";
            char alphabet = (char)(column + 97);
            returnStr = alphabet.ToString() + row.ToString();
            return returnStr;
        }
        public bool pieceClick(int column, int row)
        {
            if (getPlayer() != board.getPlayer())
                return false;
            //MessageBox.Show(board.toJson());
            if (!board.getGameStatus())//If game over ignore anything
            {
                return false;
            }
            //In console, row: 1-9，column: a-i
            if (board.getSelectedX() == -1)//If not selected, select
            {
                board.boolSelectPiece(intArrtoStr(column, row));
            }
            else
            {
                if (board.boolMovePiece(intArrtoStr(column, row)))//If move success, change the player
                {
                    board.SwitchPlayer();
                    if (board.getSelectedX() != column || board.getSelectedY() != row){
                        updateJson();
                    }
                }
            }
            return true;
        }
    }
}
