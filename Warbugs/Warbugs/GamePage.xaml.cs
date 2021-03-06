﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WarbugsLib.Lifeforms.Impl;
using WarbugsLib.Lifeforms;
using WarbugsLib.Other;
using WarbugsLib.Core;
using WarbugsLib.Environment;
using WarbugsLib.Controls;
using Microsoft.Xna.Framework.Input.Touch;
using WarbugsLib.Draw;
using System.Diagnostics;

namespace Warbugs
{
    public partial class GamePage : PhoneApplicationPage
    {
        ContentManager contentManager;
        GameTimer timer;
        SpriteBatch spriteBatch;

        float testDegrees = 0f;

        Camera _camera;

        MoveControl2 _moveCopntrol;

        SpriteFont _text;

        Texture2D DEBUG_Rect;


        bool _isDragStarted;


        public GamePage()
        {

            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;

            _camera = new Camera(SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Bounds);



        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            timer.Start();

            World.Init(contentManager.Load<Texture2D>(@"Backgrounds\background13"), _camera, SharedGraphicsDeviceManager.Current.GraphicsDevice);

            _text = contentManager.Load<SpriteFont>(@"SpriteFont1");

            TestBug testBug = new TestBug(SharedGraphicsDeviceManager.Current.GraphicsDevice, contentManager, _camera);

            testBug.Scale = 1f;

            testBug.Position = new Vector2(Tools.Rand.Next(5000, 10000), Tools.Rand.Next(5000, 10000));

            _moveCopntrol = new MoveControl2(SharedGraphicsDeviceManager.Current.GraphicsDevice, contentManager);

            World.Instance.Spiecies.Add(testBug);


            for (int i = 0; i < 70; i++)
            {
                var tb = new TestBug(SharedGraphicsDeviceManager.Current.GraphicsDevice, contentManager, _camera);

                tb.Scale = Tools.Rand.Next(90, 110) / 100f;
                
                tb.Position = new Vector2(Tools.Rand.Next(5000, 10000), Tools.Rand.Next(5000, 10000));
                World.Instance.Spiecies.Add(tb);
                tb.Live();
            }

            _camera.Focus(testBug);

            Drawer.Init(SharedGraphicsDeviceManager.Current.GraphicsDevice, contentManager, _camera);

            TouchPanel.EnabledGestures = GestureType.FreeDrag | GestureType.DragComplete;

            base.OnNavigatedTo(e);
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();


                switch (gesture.GestureType)
                {

                    case GestureType.DragComplete:
                        _moveCopntrol.Update(Vector2.Zero);
                        _isDragStarted = false;
                        break;
                    case GestureType.FreeDrag:
                        if (!_isDragStarted)
                            _moveCopntrol.CenterPoint = gesture.Position;
                        _isDragStarted = true;
                        _moveCopntrol.Update(gesture.Position);
                        break;

                }
            }



            ((TestBug)World.Instance.Spiecies[0]).Update(_moveCopntrol.Direction, _moveCopntrol.SpeedFactor);

            testDegrees += 1f;

            if (testDegrees > 360.0f) testDegrees = 0.0f;



        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);

            //_world.Draw(_lifeforms[0].Position);

            World.Instance.DrawAround(World.Instance.Spiecies[0].CurrentSector);

            spriteBatch.Begin();
            spriteBatch.DrawString(_text, _moveCopntrol.Direction.Degrees.ToString(), Vector2.Zero, Color.White);
            spriteBatch.End();



            foreach (var item in World.Instance.Spiecies)
            {
                item.RegisterOnDraw();

                if (item.IsIntersect())
                {
                 //   DEBUG_Rect = new Texture2D(SharedGraphicsDeviceManager.Current.GraphicsDevice, 1, 1);
                 //  DEBUG_Rect.SetData(new Color[] { Color.White });
                  //  Drawer.Instance.DrawOne(DEBUG_Rect, item.BoundingRect, Color.White);
                    item.CanMove = false;
                  
                }

            }



            //if (v.Count() != 0)
            //{
            //    foreach (var item in v)
            //    {
            //        DEBUG_Rect = new Texture2D(SharedGraphicsDeviceManager.Current.GraphicsDevice,1,1);
            //        DEBUG_Rect.SetData(new Color[] { Color.White });
            //        Drawer.Instance.DrawOne(DEBUG_Rect, item.BoundingRect, Color.White);
            //        item.CanMove = false;
            //        item.Move(-1f);

            //    }
            //}

            Drawer.Instance.DrawAll();



        }
    }
}