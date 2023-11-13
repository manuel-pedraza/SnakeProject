using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SnakeProject.Models.GameObjects;

namespace SnakeProject.Views
{
    /// <summary>
    /// Autor :       maxions100
    /// Description : Window of the snake game
    /// Date :        20 may 2020
    /// </summary>
    public partial class frmGameWindow : Window
    {

        #region Global_Variables
        Dictionary<Snake.Directions, bool> KeyPressValue = new Dictionary<Snake.Directions, bool>();
        private DispatcherTimer gameTimer = null;
        private SnakeGameController snakeController = null;
        #endregion


        /// <summary>
        /// ctor
        /// </summary>
        public frmGameWindow()
        {
            InitializeComponent();
            snakeController = new SnakeGameController();
            snakeController.InitializeGame();
            gameTimer = new DispatcherTimer();
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            gameTimer.Start();
            ResizeMode = ResizeMode.CanResizeWithGrip;

            for (Snake.Directions i = Snake.Directions.Up; i < Snake.Directions.Idle; i++)
                KeyPressValue.Add(i, false);   
        }

        #region Methods


        /// <summary>
        /// Autor :      maxions100
        /// Descrption : Method to draw the background over the canvas
        /// Date :       20 may 2020
        /// </summary>
        private void SetBackground()
        {
            for (int i = 0; i < snakeController.GameSizeX; i++)
            {
                for (int j = 0; j < snakeController.GameSizeY; j++)
                {
                    if ((j - (i % 2)) % 2 == 0)
                        continue;

                    Rectangle rect = new Rectangle();
                    rect.Fill = Brushes.LimeGreen;
                    rect.HorizontalAlignment = HorizontalAlignment.Left;
                    rect.VerticalAlignment = VerticalAlignment.Center;
                    rect.Height = snakeController.CubeSize;
                    rect.Width = snakeController.CubeSize;
                    rect.Margin = new Thickness(i * snakeController.CubeSize, j * snakeController.CubeSize, 0, 0);
                    cnvGameBoard.Children.Add(rect);
                }
            }
        }

        /// <summary>
        /// Autor :      maxions100
        /// Descrption : Method that render all the objects in game
        /// Date :       20 may 2020
        /// </summary>
        private void RenderGameWindow()
        {

            cnvGameBoard.Children.Clear();
            SetBackground();

            if (snakeController.GameState)
            {
                snakeController.RenderGameLogic();

                foreach (Fruit frt in snakeController.FruitsList)
                    cnvGameBoard.Children.Add(frt.Rectangle);

                foreach (SnakeBlock sb in snakeController.Snake.snakeParts)
                    cnvGameBoard.Children.Add(sb.Rectangle);


            }

            //double BlockUnitsMoved = snakeController.CubeSize * snakeController.CubeSize / (cnvGameBoard.ActualWidth * cnvGameBoard.ActualHeight);

            //foreach (Models.GameObjects.Block block in snakeController.WorldObjects)
            //{
            //    block.XCoord += BlockUnitsMoved;
            //    block.YCoord += BlockUnitsMoved;
            //}

            

        }
        #endregion

        #region Events

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event called for the gameTimer (gloabal variable)
        /// Date :        20 may 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            RenderGameWindow();

            if (!snakeController.GameState)
            {
                gameTimer.Stop();
            }


        }

        /// <summary>
        /// Autor :      maxions100
        /// Descrption : Event called when the size of the window is changed
        /// Date :       20 may 2020
        /// </summary>
        private void frmGameWin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double cnvArea = (cnvGameBoard.ActualWidth * cnvGameBoard.ActualHeight);

            snakeController.CubeSize = Math.Sqrt(cnvArea / (snakeController.GameSizeY * snakeController.GameSizeX));

            RenderGameWindow();
        }

        /// <summary>
        /// Autor :      maxions100
        /// Descrption : Event when a key is pressed 
        /// Date :       21 may 2020
        /// </summary>
        private void frmGameWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
            }


            if (!snakeController.GameState)
                return;


            switch (e.Key)
            {
                case Key.Left:
                    if(!KeyPressValue[Snake.Directions.Left])
                    {
                        snakeController.ChangeSnakeDirection(Snake.Directions.Left);
                        KeyPressValue[Snake.Directions.Left] = true;
                    }
                    break;

                case Key.Up:
                    if (!KeyPressValue[Snake.Directions.Up])
                    {
                        snakeController.ChangeSnakeDirection(Snake.Directions.Up);
                        KeyPressValue[Snake.Directions.Up] = true;
                    }
                    break;

                case Key.Right:
                    if (!KeyPressValue[Snake.Directions.Right])
                    {
                        snakeController.ChangeSnakeDirection(Snake.Directions.Right);
                        KeyPressValue[Snake.Directions.Right] = true;
                    }
                    break;

                case Key.Down:
                    if (!KeyPressValue[Snake.Directions.Down])
                    {
                        snakeController.ChangeSnakeDirection(Snake.Directions.Down);
                        KeyPressValue[Snake.Directions.Down] = true;
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Autor :      maxions100
        /// Descrption : Event whent a key is released
        /// Date :       20 may 2020
        /// </summary>
        private void frmGameWin_KeyUp(object sender, KeyEventArgs e)
        {

            switch(e.Key)
            {
                case Key.Left:
                    KeyPressValue[Snake.Directions.Left] = false;
                    break;
                case Key.Up:
                    KeyPressValue[Snake.Directions.Up] = false;
                    break;
                case Key.Right:
                    KeyPressValue[Snake.Directions.Right] = false;
                    break;
                case Key.Down:
                    KeyPressValue[Snake.Directions.Down] = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
