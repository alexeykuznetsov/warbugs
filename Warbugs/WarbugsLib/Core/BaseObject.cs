using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WarbugsLib.Core
{
    public abstract class BaseObject : INotifyPropertyChanged
    {
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyPropertyChanged("Position");
            }
        }



        public Direction Direction { get; set; }

        public Vector2 CenterPoint { get; set; }

        public BaseObject()
        {
            Position = Vector2.Zero;
            Direction = new Direction(0);
        }

        /// <summary>
        /// Move to 
        /// </summary>
        /// <param name="direction">way to go in degrees</param>
        /// <param name="distance"></param>
        public void Move(Direction direction, float distance)
        {
            Direction = direction;
            Move(distance);
        }

        public void Move(float distance)
        {
            var x = (float)(distance * Math.Sin(Direction.Radians));
            var y = (float)(distance * Math.Cos(Direction.Radians));
            Position = new Vector2(Position.X + x, Position.Y - y);
        }

        /// <summary>
        /// Move object incrementing X and Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void Move(float x, float y)
        {
            Position = new Vector2(Position.X + x, Position.Y + y);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
