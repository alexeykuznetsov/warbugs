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

namespace WarbugsLib.Lifeforms.Impl
{
    public class TestBug : Lifeform
    {

        GameTimer tmr;

        Random random = new Random();

        int wait = 0;

        int distance = 0;

        Direction _nextDirection;

        float _currentSpeed = 0f;

        bool _changeDirection = false;

        protected Dictionary<LayerType, int> drawnSpeeds = new Dictionary<LayerType, int>();

        public float Scale
        {
            get
            {
                return _sprites.Scale;
            }
            set
            {
                _sprites.Scale = value;
            }
        }

        public TestBug(GraphicsDevice device, ContentManager contentManager, Camera camera)
            : base(device, contentManager, camera)
        {

            _sprites = new CompositeSpriteBase(contentManager, device, camera,
                new LayerInfo() { Name = @"Parts\Bug1\LegsComp", Type = LayerType.Legs, ZIndex = 1, FramesCount = 8, FrameWidth = 512 },
                new LayerInfo() { Name = @"Parts\Bug1\CarapaceComp", Type = LayerType.Body, ZIndex = 4, FramesCount = 1, FrameWidth = 512 },
                new LayerInfo() { Name = @"Parts\Bug1\CarapaceTopComp", Type = LayerType.BodyTop, ZIndex = 6, FramesCount = 1, FrameWidth = 512 },
                new LayerInfo() { Name = @"Parts\Bug1\JawsComp", Type = LayerType.Jaws, ZIndex = 7, FramesCount = 6, FrameWidth = 512 },
                new LayerInfo() { Name = @"Parts\Bug1\EyesComp", Type = LayerType.Eyes, ZIndex = 9, FramesCount = 1, FrameWidth = 512 },
                new LayerInfo() { Name = @"Parts\Bug1\HeadComp", Type = LayerType.Head, ZIndex = 3, FramesCount = 1, FrameWidth = 512 }
                );



            CenterPoint = _sprites.CenterPoint;

            drawnSpeeds.Add(LayerType.Legs, 100);
            drawnSpeeds.Add(LayerType.Body, 0);
            drawnSpeeds.Add(LayerType.BodyTop, 0);
            drawnSpeeds.Add(LayerType.Eyes, 0);
            drawnSpeeds.Add(LayerType.Head, 0);
            drawnSpeeds.Add(LayerType.Jaws, 20);

            tmr = new GameTimer();

            tmr.Update += tmr_Update;


        }

        void tmr_Update(object sender, GameTimerEventArgs e)
        {
            if (wait > 0)
            {
                --wait;
                return;
            }

            if (_changeDirection)
            {
                _nextDirection = new Direction(Tools.Rand.Next(359));
                _changeDirection = false;
            }

            if (distance > 0)
            {
                --distance;

                float speedFactor = 50;

                var delta = Math.Abs(_nextDirection.Degrees - Direction.Degrees);

                if (delta > 10)
                {
                    drawnSpeeds[LayerType.Legs] = (int)speedFactor * 4;

                    _currentSpeed = speedFactor * 1.5f / delta;

                    Direction.Degrees += (int)speedFactor / 13 * Direction.GetRotationDir(_nextDirection.Degrees);

                }
                else
                {
                    _currentSpeed = speedFactor / 13 + Tools.Rand.Next(-1, 3);
                    drawnSpeeds[LayerType.Legs] = (int)_currentSpeed * 25;
                }


                if (drawnSpeeds[LayerType.Legs] != 200)
                    drawnSpeeds[LayerType.Legs] = (int)_currentSpeed * 25;

                Move(_currentSpeed);
            }
            else
            {
                drawnSpeeds[LayerType.Legs] = 0;
                wait = Tools.Rand.Next(1, 100);
                _currentSpeed = 1f;
                distance = Tools.Rand.Next(0, 200);
                _changeDirection = true;
            }

            if (Position.X < 2000)
                Position = new Vector2(2000, Position.Y);

            if (Position.Y < 2000)
                Position = new Vector2(Position.X, 2000);

        }



        public override void Live()
        {
            tmr.Start();
        }

        public void Update(Direction dir, float speedFactor)
        {

            if (speedFactor > 20)
            {
            }

            var delta = Math.Abs(Direction.Degrees - dir.Degrees);

            if (delta > 10)
            {
                drawnSpeeds[LayerType.Legs] = (int)speedFactor * 4;

                _currentSpeed = speedFactor * 1f /(delta/1.5f);

                Direction.Degrees += (int)speedFactor / 13 * Direction.GetRotationDir(dir.Degrees);

            }
            else
            {
                _currentSpeed = speedFactor / 13 + Tools.Rand.Next(-1, 3);
                drawnSpeeds[LayerType.Legs] = (int)_currentSpeed * 25;
            }

            Move(_currentSpeed);

            if (Position.X < 2000)
                Position = new Vector2(2000, Position.Y);

            if (Position.Y < 2000)
                Position = new Vector2(Position.X, 2000);


        }

    
        public override void RegisterOnDraw()
        {
            _sprites.RegisterOnDraw(Position, (float)(Direction.Radians), drawnSpeeds);
        }

        public override Rectangle BoundingRect
        {
            get
            {
                return new Rectangle((int)Position.X+175, (int)Position.Y+150, 512-300, 512-300);
            }
        }
    }
}
