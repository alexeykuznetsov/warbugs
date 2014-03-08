using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;
using WarbugsLib.Lifeforms.Sprites;
using WarbugsLib.Other;

namespace WarbugsLib.Lifeforms
{
    public abstract class Lifeform : BaseObject
    {
        protected CompositeSpriteBase _sprites;



        public Lifeform(GraphicsDevice device, ContentManager contentManager, Camera camera)
            : base()
        {
            
        }

        public abstract void Live();

        public abstract void Draw();
    }
}
