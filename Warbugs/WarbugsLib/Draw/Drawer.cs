using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;
using WarbugsLib.Other;

namespace WarbugsLib.Draw
{
    public class Drawer
    {

        private static Drawer _instance;

        public static Drawer Instance
        {
            get
            {
                return _instance;
            }
        }

        SpriteBatch _spriteBatch;

        List<DrawInfo> _items = new List<DrawInfo>();

        Camera _camera;

        GraphicsDevice _device;


        public static void Init(GraphicsDevice device, ContentManager contentManager, Camera camera)
        {
            _instance = new Drawer(device, contentManager, camera);
        }

        private Drawer(GraphicsDevice device, ContentManager contentManager, Camera camera)
        {
            _device = device;

            _camera = camera;

            _spriteBatch = new SpriteBatch(device);
        }

        /// <summary>
        /// Отрисовать все объекты
        /// </summary>
        public void DrawAll()
        {
            _spriteBatch.Begin(
              SpriteSortMode.Immediate,
              BlendState.AlphaBlend,
              null, null, null, null,
              _camera.TransformMatrix);

            var items = _items.OrderBy(x => x.GlobalZIndex);

            foreach (var item in items)
            {
                _spriteBatch.Draw(item.Texture, item.Position, item.SourceRectangle, item.Color, item.Roation, item.Origin, 1f, SpriteEffects.None, 0f);
            }


            _spriteBatch.End();

            _items.Clear();

        }

        /// <summary>
        /// Добавить объект для отрисовки
        /// </summary>
        /// <param name="?"></param>
        public void Add(DrawInfo drawInfo)
        {
            _items.Add(drawInfo);
        }

    }
}
