using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WarbugsLib.Core;

namespace WarbugsLib.Other
{
    public class Camera : BaseObject
    {
        BaseObject _objToFocus;

        GameTimer tmr = new GameTimer();

        Rectangle _bounds;

        public Camera(Rectangle bounds)
            : base()
        {
            Zoom = 1f;

            _bounds = bounds;

            tmr.Update += tmr_Update;

            tmr.Start();
        }

        void tmr_Update(object sender, GameTimerEventArgs e)
        {
            if (_objToFocus == null) return;

            float moveX = (_objToFocus.Position.X + Position.X - _bounds.Width / 2 + _objToFocus.CenterPoint.X) / 5;

            float moveY = (_objToFocus.Position.Y + Position.Y - _bounds.Height / 2 + _objToFocus.CenterPoint.Y) / 5;

            Move(-moveX, -moveY);

        }

        public float Rotation { get; set; }
        public float Zoom { get; set; }



        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Zoom) *
                   Matrix.CreateTranslation(Position.X, Position.Y, 0);
            }
        }

        /// <summary>
        /// Focus on bug or another object
        /// </summary>
        /// <param name="obj"></param>
        public void Focus(BaseObject obj)
        {
            _objToFocus = obj;
        }
    }
}
