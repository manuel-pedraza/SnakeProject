using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;


namespace SnakeProject.Models.GameObjects
{

    /// <summary>
    /// Autor :       maxions100
    /// Description : Class that controls all the logic of the snake game
    /// Date :        20 may 2020
    /// </summary>
    public class SnakeGameController
    {

        #region Global_Variables
        private bool MoveThisFrame = true;
        private bool gameState = false;
        private Snake snake;
        private Random rnd = new Random();
        private sbyte FruitsEaten = 0;
        private List<Fruit> lstFruits;
        private const int _GAMESIZEX = 39;   // Squares X
        private const int _GAMESIZEY = 20;   // Squares Y
        #endregion

        #region Properties
        public Snake Snake { get { return snake; } }
        public List<Fruit> FruitsList { get { return lstFruits; } }
        public bool GameState { get { return gameState; } }
        public double CubeSize { get; set; }
        public double GameSizeX { get { return _GAMESIZEX; } }
        public double GameSizeY { get { return _GAMESIZEY; } }
        public List<Block> WorldObjects
        {
            get
            {
                List<Block> lstBlocks = new List<Block>();

                foreach (SnakeBlock sb in Snake.snakeParts)
                    lstBlocks.Add(sb);

                foreach (Fruit frt in lstFruits)
                    lstBlocks.Add(frt);


                return lstBlocks;

            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that initialize the game
        /// Date :        20 may 2020
        /// </summary>
        public void InitializeGame()
        {
            if (!gameState && snake == null && lstFruits == null)
            {
                snake = new Snake();
                lstFruits = new List<Fruit>();

                while (snake.NbSnakeParts != 3)
                    snake.AddBodyPart(CubeSize);

                CreateFruit();
                FruitsEaten = 0;
                gameState = true;
            }
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that create a fruit on a random place of the word
        /// Date :        21 may 2020
        /// </summary>
        private void CreateFruit()
        {
            Fruit frt = new Fruit();


            // Create logic to spawn fruit where there is not serpent or another fruit 
            // This could be a function if I need the coords again LOL
            List<double[]> tmpCoords = new List<double[]>();


            for (int i = 0; i < _GAMESIZEX; i++)
                for (int j = 0; j < _GAMESIZEY; j++)
                    tmpCoords.Add(new double[3] { i, j, 0 });

            foreach (Block b in WorldObjects)
                tmpCoords.First(c => c[0] == b.XCoord && c[1] == b.YCoord)[2] = 1;

            tmpCoords = tmpCoords.Where(c => c[2] == 0).ToList();
            double[] block = tmpCoords[rnd.Next(0, tmpCoords.Count())];

            frt.XCoord = block[0];
            frt.YCoord = block[1];
            //

            frt.SetRectangle(CubeSize, new SolidColorBrush(AppVariables.PLAYER.FruitPlayerColor));
            lstFruits.Add(frt);
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that create n amount of fruits on a random place of the word
        /// Date :        21 may 2020
        /// </summary>
        /// <param name="NbFruits"></param>
        private void CreateFruit(uint NbFruits)
        {
            if (NbFruits > _GAMESIZEX * _GAMESIZEY - snake.NbSnakeParts - lstFruits.Count())
                NbFruits = (uint)(_GAMESIZEX * _GAMESIZEY - snake.NbSnakeParts - lstFruits.Count());

            for (int i = 0; i <= NbFruits; i++)
                CreateFruit();
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method called every frame for the logic
        /// Date :        20 may 2020
        /// </summary>
        public void RenderGameLogic()
        {

            if (snake.Direction != Snake.Directions.Idle)
            {

                if (ValidateSnakePos(GetSnakeMovementVector(snake.Direction)))
                {
                    EndGame();
                    return;
                }

                ValidateSnakeEating();
                snake.UpdatePosValues(GetSnakeMovementVector(snake.Direction));
                //MoveThisFrame = true;

            }

            UpdateAppleRatio();
            snake.UpdateSizeValue(CubeSize);
            //MoveThisFrame = true;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that updates the ratio of each fruit on the game
        /// Date :        21 may 2020
        /// </summary>
        private void UpdateAppleRatio()
        {
            foreach (Fruit frt in lstFruits)
                frt.ChangeRectangleRatio(CubeSize);
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Validates the position of the snake
        /// Date :        21 may 2020
        /// </summary>
        /// <param name="newSnakeVect"></param>
        /// <returns></returns>
        private bool ValidateSnakePos(double[] newSnakeVect)
        {
            SnakeBlock sb = snake.snakeParts.Peek();
            double x = sb.XCoord;
            double y = sb.YCoord;

            x += newSnakeVect[0];
            y += newSnakeVect[1];

            int i = snake.NbSnakeParts - 1;

            int? iColider = snake.snakeParts.FirstOrDefault(b => b.XCoord == x && b.YCoord == y)?.indexLength ?? null;
            iColider = (iColider != i && iColider != 0) ? iColider : null;

            if (iColider != null || x < 0 || x >= _GAMESIZEX || y < 0 || y >= _GAMESIZEY)
                return true;

            return false;

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Validates if the snake is eating
        /// Date          21 may 2020
        /// </summary>
        private void ValidateSnakeEating()
        {
            SnakeBlock sb = snake.snakeParts.Peek();
            SnakeBlock sbLast = snake.snakeParts.Last();

            Fruit frt = lstFruits.FirstOrDefault(f => f.XCoord == sb.XCoord && f.YCoord == sb.YCoord);

            if (frt == null)
                return;

            FruitsEaten++;
            lstFruits.Remove(frt);
            snake.AddBodyPart(CubeSize, sbLast.XCoord, sbLast.YCoord);
            CreateFruit();

        }
        
        /// <summary>
        /// Autor :       maxions100
        /// Description : Change the direction of the snake 
        /// Date :        20 may 2020
        /// </summary>
        /// <param name="direct"></param>
        public void ChangeSnakeDirection(Snake.Directions direct)
        {
            if (!gameState || !ValidateSnakeDirection(direct))
            {
                return;
            }

            //MoveThisFrame = false;
            snake.Direction = direct;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method called when the game ends
        /// Date :        20 may 2020
        /// </summary>
        public void EndGame()
        {
            gameState = false;
            AppVariables.AddPlayerFruit(FruitsEaten);
            AppVariables.AddPlayerGamePlayed();
            
            if(Snake.NbSnakeParts >= GameSizeX * GameSizeY)
                AppVariables.AddPlayerGameMaxed();

            AppVariables.ChangePlayerRecord(Snake.NbSnakeParts);
            //snake = null;
            //lstFruits = null;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that transforms the actual snake direction to an vector
        /// Date :        20 may 2020
        /// </summary>
        /// <param name="direct"></param>
        /// <returns></returns>
        private double[] GetSnakeMovementVector(Snake.Directions direct)
        {
            double[] vectDirect = new double[2];


            switch (direct)
            {
                case Snake.Directions.Up:
                    vectDirect[0] = 0;
                    vectDirect[1] = -1;
                    break;
                case Snake.Directions.Down:
                    vectDirect[0] = 0;
                    vectDirect[1] = 1;
                    break;
                case Snake.Directions.Left:
                    vectDirect[0] = -1;
                    vectDirect[1] = 0;
                    break;
                case Snake.Directions.Right:
                    vectDirect[0] = 1;
                    vectDirect[1] = 0;
                    break;
                default:
                    vectDirect[0] = 0;
                    vectDirect[1] = 0;
                    break;
            }


            return vectDirect;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Validates the position of the snake
        /// Date :        20 may 2020
        /// </summary>
        /// <param name="direct"></param>
        /// <returns></returns>
        private bool ValidateSnakeDirection(Snake.Directions direct)
        {

            bool bTrueDirection = true;

            switch (direct)
            {
                case Snake.Directions.Up:
                    if (snake.Direction == Snake.Directions.Down)
                        bTrueDirection = false;
                    break;
                case Snake.Directions.Down:
                    if (snake.Direction == Snake.Directions.Up)
                        bTrueDirection = false;
                    break;
                case Snake.Directions.Left:
                    if (snake.Direction == Snake.Directions.Right)
                        bTrueDirection = false;
                    break;
                case Snake.Directions.Right:
                    if (snake.Direction == Snake.Directions.Left)
                        bTrueDirection = false;
                    break;
                default:
                    break;
            }

            return bTrueDirection;
        }
        #endregion

    }
}
