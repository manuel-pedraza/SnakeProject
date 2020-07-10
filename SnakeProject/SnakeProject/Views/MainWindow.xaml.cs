using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SnakeProject.Models.GameObjects;
using System.Windows.Threading;
using SnakeProject.Views;
using SnakeProject.Models;
using Xceed.Wpf.Toolkit;


namespace SnakeProject
{


    /// <summary>
    /// Autor :       maxions100
    /// Description : Window that manage all the game
    /// Date :        20 may 2020
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Enums

        private enum ColorPickerState
        {
            Snake,
            Fruit,
            None
        }

        #endregion

        //SnakeService.SnakeServiceClient _context = new SnakeService.SnakeServiceClient();

        #region Global_Variables

        private const double BORDER = 6;
        private bool _IsMouseEnter = false;
        private ColorPickerState _colorPickerState = ColorPickerState.None;

        #endregion

        #region Methods

        /// <summary>
        /// Autor :       maxions100
        /// Description : Method that updates the values on the screen
        /// Date :        24 june 2020
        /// </summary>
        private void UploadValues()
        {
            AppVariables.SaveGame();

            txtName.Text = AppVariables.PLAYER.UserName;
            lblPlayerColor.Background = new SolidColorBrush(AppVariables.PLAYER.SnakePlayerColor);
            lblPlayerFruitColor.Background = new SolidColorBrush(AppVariables.PLAYER.FruitPlayerColor);
            lblRecord.Content = AppVariables.PLAYER.SizeRecord.ToString();
            lblFruitsEaten.Content = AppVariables.PLAYER.FruitsEaten.ToString();
            lblMaxedGames.Content = AppVariables.PLAYER.MaxedGames.ToString();
            lblGamesPlayed.Content = AppVariables.PLAYER.GamesPlayed.ToString();
        }
        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //this.Visibility = Visibility.Hidden;
        }


        #region Events
        /// <summary>
        /// Autor :       maxions100
        /// Description : Event to start the snake game when the button is clicked
        /// Date :        30 may 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartSnake_Click(object sender, RoutedEventArgs e)
        {
            frmGameWindow frmSnake = new frmGameWindow();
            frmSnake.Owner = this;

            bool? bDidGameClose = null;

            bDidGameClose = frmSnake.ShowDialog();

            if (bDidGameClose == true)
            {
                //Something

            }
            else
            {
                //this.Close();
            }

            UploadValues();
            
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event called when the game's main menu window has just been loaded
        /// Date :        30 may 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AppVariables.LoadGame();

            UploadValues();
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is over the reactangle to make a border
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerColor_MouseEnter(object sender, MouseEventArgs e)
        {
            // I should do a method to not repeat this thing 4 times :V

            lblPlayerColor.Height += BORDER;
            lblPlayerColor.Width +=  BORDER;
            double left = lblPlayerColor.Margin.Left - BORDER / 2;
            double top = lblPlayerColor.Margin.Top - BORDER / 2;
            double right = lblPlayerColor.Margin.Right - BORDER / 2;
            double bot = lblPlayerColor.Margin.Bottom - BORDER / 2;

            lblPlayerColor.Margin = new Thickness(left, top, right, bot);
            lblPlayerColor.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblPlayerColor.BorderThickness = new Thickness(BORDER / 2, BORDER / 2, BORDER / 2, BORDER / 2);
            lblPlayerColor.BorderBrush = Brushes.SkyBlue;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is no longer over the reactangle, this removes the border
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerColor_MouseLeave(object sender, MouseEventArgs e)
        {
            lblPlayerColor.Height -= BORDER;
            lblPlayerColor.Width -= BORDER;
            double left = lblPlayerColor.Margin.Left + BORDER / 2;
            double top = lblPlayerColor.Margin.Top + BORDER / 2;
            double right = lblPlayerColor.Margin.Right + BORDER / 2;
            double bot = lblPlayerColor.Margin.Bottom + BORDER / 2;


            lblPlayerColor.Margin = new Thickness(left, top, right, bot);
            lblPlayerColor.BorderThickness = new Thickness(0,0,0,0);
            lblPlayerColor.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblPlayerColor.ClipToBounds = true;

            lblPlayerColor.BorderBrush = Brushes.Transparent;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is over the reactangle to make a border
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerFruitColor_MouseEnter(object sender, MouseEventArgs e)
        {
            lblPlayerFruitColor.Height += BORDER;
            lblPlayerFruitColor.Width += BORDER;
            double left = lblPlayerFruitColor.Margin.Left - BORDER / 2;
            double top = lblPlayerFruitColor.Margin.Top - BORDER / 2;
            double right = lblPlayerFruitColor.Margin.Right - BORDER / 2;
            double bot = lblPlayerFruitColor.Margin.Bottom - BORDER / 2;


            lblPlayerFruitColor.Margin = new Thickness(left, top, right, bot);
            lblPlayerFruitColor.BorderThickness = new Thickness(BORDER / 2, BORDER / 2, BORDER / 2, BORDER / 2);
            lblPlayerColor.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblPlayerColor.ClipToBounds = true;

            lblPlayerFruitColor.BorderBrush = Brushes.SkyBlue;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is no longer over the reactangle, this removes the border
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerFruitColor_MouseLeave(object sender, MouseEventArgs e)
        {
            lblPlayerFruitColor.Height -= BORDER;
            lblPlayerFruitColor.Width -= BORDER;
            double left = lblPlayerFruitColor.Margin.Left + BORDER / 2;
            double top = lblPlayerFruitColor.Margin.Top + BORDER / 2;
            double right = lblPlayerFruitColor.Margin.Right + BORDER / 2;
            double bot = lblPlayerFruitColor.Margin.Bottom + BORDER / 2;


            lblPlayerFruitColor.Margin = new Thickness(left, top, right, bot);
            lblPlayerFruitColor.BorderThickness = new Thickness(0, 0, 0, 0);
            lblPlayerColor.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblPlayerFruitColor.BorderBrush = Brushes.Transparent;
        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is no longer holded over the color of the player
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerColor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _IsMouseEnter = true;
            clrColor.Visibility = Visibility.Visible;
            clrColor.SelectedColor = AppVariables.PLAYER.SnakePlayerColor;
            _colorPickerState = ColorPickerState.Snake;


        }


        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is no longer holded over the color of the fruit
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerFruitColor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _IsMouseEnter = true;
            clrColor.Visibility = Visibility.Visible;
            clrColor.SelectedColor = AppVariables.PLAYER.FruitPlayerColor;
            _colorPickerState = ColorPickerState.Fruit;

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event that changes the color selector to false
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clrColor_MouseLeave(object sender, MouseEventArgs e)
        {
            if(_IsMouseEnter)
                _IsMouseEnter = false;   

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event that changes the color selector to true
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clrColor_MouseEnter(object sender, MouseEventArgs e)
        {
            _IsMouseEnter = true;   
        }


        /// <summary>
        /// Autor :       maxions100
        /// Description : Event when the mouse is clicked to remove the color selector
        /// Date :        16 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clrColor.Visibility = Visibility.Hidden;
            _colorPickerState = ColorPickerState.None;

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Event called when the main color of the color picker has changed
        /// Date :        18 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clrColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            switch (_colorPickerState)
            {
                case ColorPickerState.Snake:
                    AppVariables.ChangeSnakeColor(e.NewValue.GetValueOrDefault());
                    lblPlayerColor.Background = new SolidColorBrush(AppVariables.PLAYER.SnakePlayerColor);

                    break;
                case ColorPickerState.Fruit:
                    AppVariables.ChangeFruitColor(e.NewValue.GetValueOrDefault());
                    lblPlayerFruitColor.Background = new SolidColorBrush(AppVariables.PLAYER.FruitPlayerColor);

                    break;
                case ColorPickerState.None:
                    break;
            }

            UploadValues();
        }


        /// <summary>
        /// Autor :       maxions100
        /// Description : Event called when the username is changed
        /// Date :        24 june 2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            AppVariables.ChangeName(txtName.Text);
            AppVariables.SaveGame();
        }
        #endregion 

    }
}
