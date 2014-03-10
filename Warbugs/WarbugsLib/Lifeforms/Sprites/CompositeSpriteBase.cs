using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Draw;
using WarbugsLib.Lifeforms.Sprites;
using WarbugsLib.Other;

namespace WarbugsLib.Lifeforms
{
    public class CompositeSpriteBase
    {
        SpriteBatch _spriteBatch;

        Camera _camera;

        private IEnumerable<SpriteRect> _sprites;

        public Vector2 CenterPoint { get; private set; }

        List<Texture2D> txtrs = new List<Texture2D>();

        public float Scale{get;set;}

        public CompositeSpriteBase(ContentManager contentManager, GraphicsDevice device, Camera camera, params LayerInfo[] layersInfo)
        {
            Scale = 1f;

            _sprites = 
                (from t in layersInfo
                select new SpriteRect(contentManager.Load<Texture2D>(t.Name),t)).OrderBy(x=>x.Info.ZIndex).ToList();

            CenterPoint = new Vector2(layersInfo[0].FrameWidth / 2+25, layersInfo[0].FrameWidth / 2);

            _spriteBatch = new SpriteBatch(device);
            
            _camera = camera;
        }

        public void RegisterOnDraw(Vector2 position, float rotation, Dictionary<LayerType, int> drawnSpeeds)
        {

            var drawPosition = new Vector2(position.X + CenterPoint.X, position.Y + CenterPoint.Y);

            foreach (var sprite in _sprites)
            {
                sprite.Speed = drawnSpeeds[sprite.Info.Type];
                Texture2D txtr = sprite.NextSprite.Texture;


                //Shadows
                if (sprite.Info.Type != LayerType.Legs)
                {
                    int zIndex = 0;

                    if (sprite.Info.Type == LayerType.BodyTop)
                        zIndex = 5;

                    Drawer.Instance.Add(new DrawInfo()
                    {
                        Texture = txtr,
                        Position = new Vector2(drawPosition.X + 30, drawPosition.Y + 5),
                        SourceRectangle = sprite.CurrentRectangle,
                        Color = Color.Black,
                        Roation = rotation,
                        Origin = CenterPoint,
                        Scale = Scale,
                        GlobalZIndex = zIndex
                    });
                }


                //Sprite
                Drawer.Instance.Add(new DrawInfo()
                {
                    Texture = txtr,
                    Position = new Vector2(drawPosition.X, drawPosition.Y),
                    SourceRectangle = sprite.CurrentRectangle,
                    Color = Color.White,
                    Roation = rotation,
                    Origin = CenterPoint,
                    Scale = Scale,
                    GlobalZIndex = sprite.Info.ZIndex
                });


            }
        }


    }
}
