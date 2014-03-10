using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Lifeforms;
using WarbugsLib.Other;

namespace WarbugsLib.Environment
{
    public class World
    {
        public static Sector[,] Sectors { get; set; }

        Texture2D _texture;

        Camera _camera;

        SpriteBatch _spriteBatch;

        public static World Instance { get; private set; }

        public List<Lifeform> Spiecies { get; private set; }

        public static void Init(Texture2D texture, Camera camera, GraphicsDevice device)
        {
            Instance = new World(texture, camera, device);
        }

        private World(Texture2D texture, Camera camera, GraphicsDevice device)
        {
            Spiecies = new List<Lifeform>();

            Sectors = new Sector[100, 100];

            _texture = texture;
            _camera = camera;
            _spriteBatch = new SpriteBatch(device);

            for (int y = 0; y < Sectors.GetLength(1); y++)
            {
                for (int x = 0; x < Sectors.GetLength(0); x++)
                {
                    Sector sector = new Sector(new Rectangle(x * _texture.Width, y * _texture.Height, texture.Width, texture.Height), texture, x, y);
                    Sectors[x, y] = sector;
                }
            }
        }

        public Sector GetSectorByCoordinates(Vector2 coords)
        {
            var x = (int)(coords.X / _texture.Width);
            var y = (int)(coords.Y / _texture.Height);


            if (x < 0) x = 0;

            if (y < 0) y = 0;

            if (x > 100) x = 100;

            if (y > 100) y = 100;

            return Sectors[x, y];
        }

        public void DrawAround(Sector sector)
        {

            _spriteBatch.Begin(
               SpriteSortMode.Texture,
               BlendState.AlphaBlend,
               null,
               null,
               null,
               null,
               _camera.TransformMatrix);

            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X, currentSector.Rect.Y), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X - _texture.Width, currentSector.Rect.Y), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X + _texture.Width, currentSector.Rect.Y), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X, currentSector.Rect.Y - _texture.Height), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X, currentSector.Rect.Y + _texture.Height), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X - _texture.Width, currentSector.Rect.Y - _texture.Height), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X + _texture.Width, currentSector.Rect.Y - _texture.Height), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X - _texture.Width, currentSector.Rect.Y + _texture.Height), Color.White);
            //_spriteBatch.Draw(_texture, new Vector2(currentSector.Rect.X + _texture.Width, currentSector.Rect.Y + _texture.Height), Color.White);


            sector.Draw(_spriteBatch);


            Sectors[sector.IndexX, sector.IndexY - 1].Draw(_spriteBatch);
            Sectors[sector.IndexX + 1, sector.IndexY - 1].Draw(_spriteBatch);
            Sectors[sector.IndexX + 1, sector.IndexY].Draw(_spriteBatch);
            Sectors[sector.IndexX + 1, sector.IndexY + 1].Draw(_spriteBatch);
            Sectors[sector.IndexX, sector.IndexY + 1].Draw(_spriteBatch);
            Sectors[sector.IndexX - 1, sector.IndexY + 1].Draw(_spriteBatch);
            Sectors[sector.IndexX - 1, sector.IndexY].Draw(_spriteBatch);
            Sectors[sector.IndexX - 1, sector.IndexY - 1].Draw(_spriteBatch);

            _spriteBatch.End();
        }




    }
}
