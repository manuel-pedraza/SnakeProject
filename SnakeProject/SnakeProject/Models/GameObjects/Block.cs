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
    /// Description : Block class
    /// Date :        21 june 2020
    /// </summary>
    public abstract class Block
    {
        public double XCoord { get; set; } = 0;
        public double YCoord { get; set; } = 0;
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Set the block by passing the size cube
        /// Date :        21 may 2020
        /// </summary>
        /// <param name="CubeSize"></param>
        public void ChangeRectangleRatio(double CubeSize)
        {
            Rectangle.Height = CubeSize;
            Rectangle.Width = CubeSize;
            Rectangle.Margin = new Thickness(XCoord * CubeSize, YCoord * CubeSize, 0, 0);
        }


        /// <summary>
        /// Autor         maxions100
        /// Description : Set the block by passing the size cube and color
        /// Date :        21 may 2020
        /// </summary>
        /// <param name="CubeSize"></param>
        /// <param name="br"></param>
        public void SetRectangle(double CubeSize, Brush br)
        {
            Rectangle rect = new Rectangle();

            rect.Fill = br;
            rect.HorizontalAlignment = HorizontalAlignment.Left;
            rect.VerticalAlignment = VerticalAlignment.Center;
            rect.Height = CubeSize;
            rect.Width = CubeSize;

            rect.Margin = new Thickness(XCoord * CubeSize, YCoord * CubeSize, 0, 0);
            Rectangle = rect;

        }

        /// <summary>
        /// Autor :       maxions100
        /// Description : Mehtod that changes the color of the rectangle
        /// Date :        20 june 2020
        /// </summary>
        /// <param name="br"></param>
        public void SetRectangleColor(Brush br)
        {
            Rectangle.Fill = br;
        }
    }
}
