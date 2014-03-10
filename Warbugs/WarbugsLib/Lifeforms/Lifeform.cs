using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;
using WarbugsLib.Environment;
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
            CanMove = true;
        }

        public abstract void Live();

        public abstract void RegisterOnDraw();

        public Sector CurrentSector
        {
            get
            {
                return World.Instance.GetSectorByCoordinates(Position);
            }
        }

        public abstract Rectangle BoundingRect { get; }
        public bool IsIntersect()
        {
            return World.Instance.Spiecies.Any(x => x.BoundingRect.Intersects(BoundingRect) && x!=this);
        }

        public bool CanMove { get; set; }
        public override void Move(float distance)
        {
            if (CanMove)
                base.Move(distance);
            else
            {
              //  
                Position = LastPosition;
                CanMove = true;
            }
        }

    }
}
