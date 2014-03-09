using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarbugsLib.Draw
{
    public struct DrawInfo
    {
        
        public Texture2D Texture{get;set;}

        public Vector2 Position{get;set;}

        public Rectangle SourceRectangle{get;set;}

        public Color Color{get;set;}

        public float Roation{get;set;}

        public Vector2 Origin{get;set;}

        public float Scale{get;set;}

        public int GlobalZIndex { get; set; }



    }
}
