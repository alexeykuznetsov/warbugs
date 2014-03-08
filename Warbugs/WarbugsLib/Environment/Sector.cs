using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;

namespace WarbugsLib.Environment
{
    public class Sector
    {
        public List<BaseObject> objects = new List<BaseObject>();

        public Rectangle Rect;

        public Sector(Rectangle rect)
        {
            Rect = rect;
        }
    }
}
