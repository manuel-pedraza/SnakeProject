using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SnakeProject.Models
{
    /// <summary>
    /// Autor :       maxions100  
    /// Description : Player class that will have the actual player data about the snake game
    /// Date :        7 june 2020
    /// </summary>
    public class Player
    {
        public string UserName { get; set; }

        public int SizeRecord { get; set; }
        public int FruitsEaten { get; set; }
        public int MaxedGames { get; set; }
        public int GamesPlayed { get; set; }

        public Color SnakePlayerColor { get; set; }
        public Color FruitPlayerColor { get; set; }

    }
}
