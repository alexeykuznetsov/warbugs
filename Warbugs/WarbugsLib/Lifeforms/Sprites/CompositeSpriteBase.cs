using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public CompositeSpriteBase(ContentManager contentManager, GraphicsDevice device, Camera camera, params LayerInfo[] layersInfo)
        {
        
            _sprites = 
                (from t in layersInfo
                select new SpriteRect(contentManager.Load<Texture2D>(t.Name),t)).OrderBy(x=>x.Info.ZIndex).ToList();

            CenterPoint = new Vector2(layersInfo[0].FrameWidth / 2, layersInfo[0].FrameWidth / 2);

            _spriteBatch = new SpriteBatch(device);
            
            _camera = camera;

            
        }

        public void Draw(Vector2 position, float rotation, Dictionary<LayerType,int> drawnSpeeds)
        {
            if (_sprites == null) return;

            var v = new DepthStencilState();

            _spriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                null,null,null,null,
                _camera.TransformMatrix);

            var drawPosition = new Vector2(position.X + CenterPoint.X, position.Y + CenterPoint.Y);


            List<Texture2D> txtrs = new List<Texture2D>();

            foreach (var sprite in _sprites)
            {
                sprite.Speed = drawnSpeeds[sprite.Info.Type];
                Texture2D txtr = sprite.NextSprite.Texture;

                if (sprite.Info.Type!= LayerType.Legs)
                _spriteBatch.Draw(txtr, new Vector2(drawPosition.X + 30, drawPosition.Y + 5), sprite.CurrentRectangle, Color.Black, rotation, CenterPoint, 1f, SpriteEffects.None, 1f);
                txtrs.Add(txtr);
            }

            int c = 0;
            foreach (var sprite in _sprites)
            {
                //sprite.Speed = drawnSpeeds[sprite.Info.Type];
                _spriteBatch.Draw(txtrs[c], drawPosition, sprite.CurrentRectangle, Color.White, rotation, CenterPoint, 1f, SpriteEffects.None, 1f);
                ++c;
            }

            _spriteBatch.End();
        }

    }
}
