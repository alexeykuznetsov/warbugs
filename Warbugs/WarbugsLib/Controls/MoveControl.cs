using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;
using WarbugsLib.Lifeforms;
using WarbugsLib.Lifeforms.Sprites;
using WarbugsLib.Other;

namespace WarbugsLib.Controls
{
    public class MoveControl : BaseObject, IControl
    {
        SpriteBatch _spriteBatch;

        Texture2D _texture;

        Color _color = Color.White;

        private float _speedFactor;

        public float SpeedFactor { get { return _speedFactor; }
            private set
            {
                if (value < 10)
                {
                    _speedFactor = 0;
                    return;
                }

                if (value <= _texture.Width / 2-30)
                    _speedFactor = value;
                else _speedFactor = _texture.Width / 2-30;
            }
        }

        public MoveControl(GraphicsDevice device, ContentManager contentManager)
            : base()
        {
            _spriteBatch = new SpriteBatch(device);

            _texture = contentManager.Load<Texture2D>(@"Controls\ControlCircle");

            Position = new Microsoft.Xna.Framework.Vector2(0, device.Viewport.Height - _texture.Height);

            CenterPoint = new Vector2(Position.X + _texture.Width / 2,Position.Y+ _texture.Height / 2);

        }

        public void Update(Vector2 finger)
        {

            //Вычисляем градусы исходя из положения пальца на круге
            if (IsTaped(finger.X, finger.Y))
            {
                _color = Color.Black;



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
                _color = Color.White;
                SpeedFactor = 0;
            }
        }

        public void Draw()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, Position,_color);
            _spriteBatch.End();
        }

        public bool IsTaped(float x, float y)
        {
            return (x < Position.X+_texture.Width && y > Position.Y);
        }
    }
}
