using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Other;

namespace WarbugsLib.Environment
{
    public class World
    {
        public List<Sector> Sectors { get; set; }

        Texture2D _texture;

        Camera _camera;

        SpriteBatch _spriteBatch;

        public World(Texture2D texture, Camera camera, GraphicsDevice device)
        {

            Sectors = new List<Sector>();

            _texture = texture;
            _camera = camera;
            _spriteBatch = new SpriteBatch(device);

            for (int y = -10; y < 20; y++)
            {
                for (int x = -10; x < 20; x++)
                {
                    Sector sector = new Sector(new Rectangle(x * _texture.Width, y * _texture.Height, texture.Width, texture.Height));
                    Sectors.Add(sector);
                }
            }
        }

        public void Draw(Vector2 focused)
        {
            var currentSector = Sectors.Where(
                s =>
                  s.Rect.X <= focused.X
                && s.Rect.Y <= focused.Y
                && s.Rect.X + _texture.Width > focused.X
                && s.Rect.Y + _texture.Height > focused.Y).FirstOrDefault();

            _spriteBatch.Begin(
               SpriteSortMode.Texture,
               BlendState.AlphaBlend,
               null,
               null,
               null,
               null,
               _camera.TransformMatrix);

            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X, currentSector.Rect.Y), Color.White);
            
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X-_texture.Width, currentSector.Rect.Y), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X+_texture.Width, currentSector.Rect.Y), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X, currentSector.Rect.Y - _texture.Height), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X, currentSector.Rect.Y + _texture.Height), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X - _texture.Width, currentSector.Rect.Y - _texture.Height), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X + _texture.Width, currentSector.Rect.Y - _texture.Height), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X - _texture.Width, currentSector.Rect.Y + _texture.Height), Color.White);
            _spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X + _texture.Width, currentSector.Rect.Y + _texture.Height), Color.White);
            


            _spriteBatch.End();
        }




    }
}
