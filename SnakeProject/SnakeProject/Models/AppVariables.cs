using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using System.Configuration;

namespace SnakeProject.Models
{
    /// <summary>
    /// Autor :       
    /// Description : 
    /// Date :        18 june 2020
    /// </summary>
    public static class AppVariables
    {
        //private static SnakeService.SnakeServiceClient _context = new SnakeService.SnakeServiceClient();
        //public static SnakeService.SnakeServiceClient CONTEXT { get { return _context; } }

        private static Player _player = new Player();
        public static Player PLAYER { get { return _player; } }


        #region Methods
        /// <summary>
        /// Autor :      maxions100
        /// Desciption : Method to sign in. Returns false if there is an error true if not
        /// Date :       18 june 2020
        /// </summary>
        /// <param name="username"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        //public static bool LogIn(string username, string psw)
        //{

        //    // Validate when the player logs in

        //    return false;
        //}


        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that loads the game variables from the .json file
        /// Date :        24 june 2020
        /// </summary>
        public static void LoadGame()
        {

            if (File.Exists(ConfigurationManager.AppSettings["dataPath"].ToString()))
            {
                string sJson = "";
                
                try
                {
                    StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["dataPath"].ToString());
                    sJson = sr.ReadToEnd();
                    sr.Close();

                    _player = JsonConvert.DeserializeObject<Player>(sJson);
                    _player = JsonConvert.DeserializeObject<Player>(sJson);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occur while loading the game!\r\n" + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                _player.SnakePlayerColor = Brushes.Blue.Color;
                _player.FruitPlayerColor = Brushes.Red.Color;
            }
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that saves the player's game
        /// Date :        24 june 2020
        /// </summary>
        public static void SaveGame()
        {
            try
            {
                if (File.Exists(ConfigurationManager.AppSettings["dataPath"].ToString()))
                    File.Delete(ConfigurationManager.AppSettings["dataPath"].ToString());


                using (StreamWriter writer = new StreamWriter(File.Create(ConfigurationManager.AppSettings["dataPath"].ToString())))
                {

                    writer.Write(JsonConvert.SerializeObject(_player));

                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occur while saving the game!\r\n" + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that changes the snake's color
        /// Date :        20 june 2020
        /// </summary>
        public static void ChangeSnakeColor(Color clr)
        {
            _player.SnakePlayerColor = clr;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that changes the color of the fruits in the game
        /// Date :        20 june 2020
        /// </summary>
        public static void ChangeFruitColor(Color clr)
        {
            _player.FruitPlayerColor = clr;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that increases the number of games played
        /// Date :        20 june 2020
        /// </summary>
        public static void AddPlayerGamePlayed()
        {
            _player.GamesPlayed++;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that increases the number of games where the player has reached the maximum of fruits ever possible?
        /// Date :        20 june 2020
        /// </summary>
        public static void AddPlayerGameMaxed()
        {
            _player.MaxedGames++;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that increases the number of fruits eaten by one
        /// Date :        20 june 2020
        /// </summary>
        public static void AddPlayerFruit()
        {
            _player.FruitsEaten++;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that increases the number of fruits eaten by the number passed as a param
        /// Date :        20 june 2020
        /// </summary>
        public static void AddPlayerFruit(int NbFruits)
        {
            _player.FruitsEaten += NbFruits;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that changes the record of the size of the player
        /// Date :        20 june 2020
        /// </summary>
        public static void ChangePlayerRecord(int NewSize)
        {
            if (NewSize > _player.SizeRecord)
                _player.SizeRecord = NewSize;

        }

        /// <summary>
        /// /// Autor :   maxions100
        /// Description : Method that changes the username of the player
        /// Date :        24 june 2020
        /// </summary>
        public static void ChangeName(string sNewName)
        {
            _player.UserName = sNewName;

        }

        #endregion

    }
}
