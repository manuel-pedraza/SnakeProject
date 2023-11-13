using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace SnakeProject.Models.GameObjects
{
    /// <summary>
    /// Autor :       maxions100
    /// Description : Snake class for the main player snake
    /// Date :        20 may 2020
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Enum for directions
        /// </summary>
        public enum Directions
        {
            Up, Down, Left, Right, Idle
        }

        private Queue<SnakeBlock> snakeBody = new Queue<SnakeBlock>();

        public Directions Direction { get; set; } = Directions.Idle;
        public int NbSnakeParts { get { return snakeBody.Count();} }
        public Queue<SnakeBlock> snakeParts { get { return snakeBody;} }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Add a part to the snake
        /// Date :        20 may 2020
        /// </summary>
        /// <param name="CubeSize"></param>
        public void AddBodyPart(double CubeSize)
        {
            AddBodyPart(CubeSize, 0 , 0);
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Add a part to the snake ans set its x and y location
        /// Date :        20 may 2020
        /// </summary>
        /// <param name="CubeSize"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddBodyPart(double CubeSize, double x, double y)
        {
            SnakeBlock sb = new SnakeBlock();

            sb.SetRectangle(CubeSize, new SolidColorBrush(AppVariables.PLAYER.SnakePlayerColor));

            sb.XCoord = x;
            sb.YCoord = y;

            sb.indexLength = snakeBody.Count;

            snakeBody.Enqueue(sb);

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Update all the body parts of the snake
        /// Date :        21 may 2020
        /// </summary>
        /// <param name="CubeSize"></param>
        public void UpdateSizeValue(double CubeSize)
        {
            foreach (SnakeBlock sb in snakeParts)
               sb.ChangeRectangleRatio(CubeSize);

        }

        public void UpdateColor(Brush brush)
        {

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Update the position for each body part of the snake
        /// Date :        21 may 2020
        /// </summary>
        /// <param name="ad"></param>
        public void UpdatePosValues(double[] ad)
        {

            if (ad[0] == 0 && ad[1] == 0)
                return;

            SnakeBlock sb = null;
            double[] sbCoords = new double[2];

            for (int i = 0; i < NbSnakeParts; i++)
            {

                sb = snakeBody.Dequeue();           

                if(sb.indexLength != 0)
                {
                    ad[0] = sbCoords[0] - sb.XCoord;
                    ad[1] = sbCoords[1] - sb.YCoord;
                }

                sbCoords[0] = sb.XCoord;
                sbCoords[1] = sb.YCoord;


                sb.XCoord += ad[0];
                sb.YCoord += ad[1];

                snakeBody.Enqueue(sb);
            }

        }
    }
}
