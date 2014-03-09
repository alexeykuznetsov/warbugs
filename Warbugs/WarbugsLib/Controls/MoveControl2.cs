using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;

namespace WarbugsLib.Controls
{
    public class MoveControl2: BaseObject, IControl
    {
        SpriteBatch _spriteBatch;

        Rectangle _screenBounds;

        private float _speedFactor;

        const float MaxSpeedFactor=70;

        public float SpeedFactor { get { return _speedFactor; }
            private set
            {
                //Dead zone при таче, чтобы не дергался при малейшем движении пальца
                if (value < 10)
                {
                    _speedFactor = 0;
                    return;
                }


                if (value <= MaxSpeedFactor)
                    _speedFactor = value;
                else _speedFactor = MaxSpeedFactor;
            }
        }

        public MoveControl2(GraphicsDevice device, ContentManager contentManager)
            : base()
        {
            _spriteBatch = new SpriteBatch(device);

            _screenBounds = new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height);

        }

        public void Update(Vector2 finger)
        {

            //Вычисляем градусы исходя из положения пальца на круге
            if (IsTaped(finger.X, finger.Y))
            {


                var ab = new Vector2(finger.X - CenterPoint.X, finger.Y - CenterPoint.Y);

                var cd = new Vector2(0, (CenterPoint.Y - 100) - CenterPoint.Y);

                var p = ab * cd;

                var cos = p.Y / (ab.Length() * cd.Length());

                var v = (int)MathHelper.ToDegrees((float)Math.Acos(cos));

                Direction.Degrees = finger.X > CenterPoint.X ? v : 360 - v;

                SpeedFactor = Vector2.Distance(finger, CenterPoint);

            }
            else
            {
                SpeedFactor = 0;
            }
        }

        public bool IsTaped(float x, float y)
        {
            return y > _screenBounds.Height/2;
        }
    }
}
