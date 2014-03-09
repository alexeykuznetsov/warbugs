using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;

namespace WarbugsLib.Environment
{
    public class Sector
    {
        Texture2D _texture;

        public int IndexX { get; private set; }

        public int IndexY { get; private set; }

        public List<BaseObject> objects = new List<BaseObject>();

        public Rectangle Rect;

        public Sector(Rectangle rect, Texture2D texture, int iX, int iY)
        {
            Rect = rect;
            _texture = texture;

            IndexX = iX;

            IndexY = iY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(Rect.X,Rect.Y), Color.White);
        }
    }
}
