using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Lifeforms.Sprites;

namespace WarbugsLib.Lifeforms
{
    public class SpriteRect
    {
        Rectangle[] _rectangles;

        public Texture2D Texture{get;set;}

        public Rectangle CurrentRectangle{get;private set;}

        private int _currentRectNum= 0;

        private DateTime _lastUpdate = DateTime.Now;

        public int Speed { get; set; }

        public LayerInfo Info {get;set;}

        public SpriteRect(Texture2D texture, LayerInfo info)
        {
            Texture = texture;

            Info = info;

            List<Rectangle> rects = new List<Rectangle>();

            int counter = 0;

            for (int y = 0; y < texture.Height; y += Info.FrameWidth)
            {
                for (int x = 0; x < texture.Width; x+=Info.FrameWidth)
                {

                    if (counter >= Info.FramesCount) break;
                    rects.Add(new Rectangle(x, y, Info.FrameWidth, Info.FrameWidth));

                    ++counter;
                }
            }

            _rectangles = rects.ToArray();
        }

        public SpriteRect NextSprite
        {
            get
            {
                if ((DateTime.Now - _lastUpdate) > TimeSpan.FromMilliseconds(200 - Speed) && Speed!=0)
                {
                    _currentRectNum++;
                    if (_currentRectNum >= Info.FramesCount) _currentRectNum = 0;

                    _lastUpdate = DateTime.Now;
                }


                CurrentRectangle = _rectangles[_currentRectNum];
                return this;
            }
        }


    }
}
