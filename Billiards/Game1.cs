using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Billiards
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private const float FLOOR_WIDTH = 8.0f;
        private const float FLOOR_HEIGHT = 8.0f;
        private const float CAMERA_FOV = 90.0f;
        private const float CAMERA_ZNEAR = 0.01f;
        private const float CAMERA_ZFAR = 100.0f;
        private const float CAMERA_OFFSET = 0.0f;
        private const float CAMERA_BOUNDS_MIN_X = -FLOOR_WIDTH / 2.0f;
        private const float CAMERA_BOUNDS_MAX_X = FLOOR_WIDTH / 2.0f;
        private const float CAMERA_BOUNDS_MIN_Y = CAMERA_OFFSET;
        private const float CAMERA_BOUNDS_MAX_Y = 4.0f;
        private const float CAMERA_BOUNDS_MIN_Z = -FLOOR_HEIGHT / 2.0f;
        private const float CAMERA_BOUNDS_MAX_Z = FLOOR_HEIGHT / 2.0f;
        private int windowWidth;
        private int windowHeight;

        private CameraComponent camera;

        public BallCollection ballCollection;
        public GraphicsDeviceManager graphics;
        public bool LineUpCueBall = true;
        public bool AllStopped = true;
        public bool CueBallMode = true;

        public Vector3 CameraTarget = new Vector3(0, 0, 0);
        KeyboardState prevState = new KeyboardState();
        KeyboardState currState = new KeyboardState();

        protected Model table;
        Model Skybox;

        protected List<Vector3> StartingPositions;
        protected Matrix TableWorld = Matrix.CreateWorld(new Vector3(0f, -0.035f, 0f), Vector3.Forward, Vector3.Up);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Setup the window to be a quarter the size of the desktop.

            windowWidth = GraphicsDevice.DisplayMode.Height;
            windowHeight = GraphicsDevice.DisplayMode.Height;
          graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            camera = new CameraComponent(this);
            Components.Add(camera);

            GraphicsDevice device = graphics.GraphicsDevice;


            /// GraphicsDeviceManager graphicsManger; 

            float aspectRatio = (float)windowWidth / (float)windowHeight;
            camera.Perspective(CAMERA_FOV, aspectRatio, CAMERA_ZNEAR, CAMERA_ZFAR);
            camera.OrbitMinZoom = 1.5f;
            camera.OrbitMaxZoom = 5.0f;
            camera.PreferTargetYAxisOrbiting = true;
            camera.OrbitOffsetDistance = camera.OrbitMinZoom;
            camera.CurrentBehavior = Camera.Behavior.Orbit;
            camera.Rotate(-90.0f, -7.0f, 0.0f);



            ballCollection = new BallCollection(this, camera);
            Components.Add(ballCollection);
            SetCueballMode();


            FrameRateDisplay frameRateDisplay = new FrameRateDisplay(this);
            Components.Add(frameRateDisplay);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            table = Content.Load<Model>(@"PoolTable/pooltable");

            // Model and Textures from  http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series2/Skybox.php
            Skybox = Content.Load<Model>(@"Models/SkyBox");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();



            currState = Keyboard.GetState();
            AllStopped = ballCollection.AllBallsStopped() ? true : false;
            if (currState != prevState && currState.IsKeyDown(Keys.F))
            {
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }
            if (AllStopped)
            {
                if (currState != prevState && currState.IsKeyDown(Keys.Q))
                {
                    CueBallMode = !CueBallMode;
                }


                if (CueBallMode)
                {
                    SetCueballMode();
                 
                    if ((currState != prevState && (currState.IsKeyDown(Keys.Space)) || (Mouse.GetState().LeftButton == ButtonState.Pressed)))
                    {
                        //  float Power = MathHelper.Clamp(camera.OrbitOffsetDistance * 4, 2, 10);
                        float Power = 8;
                        float ShootingAngle = GetShootingAngle();
                        ballCollection.SetSpeedandAngle(ballCollection.CueBall, Power, ShootingAngle);

                        camera.CurrentBehavior = Camera.Behavior.Spectator;
                        camera.Position = new Vector3(0,1 , 0);
                        
                        camera.OrbitOffsetDistance = 2f;
                        camera.CurrentBehavior = Camera.Behavior.Orbit;
                        camera.OrbitTarget = Vector3.Zero;
                        //camera.Rotate(0, -30f, 0);
                        //LineUpCueBall = true;

                    }
                }
                else
                {
                    SetGlobalMode();

                }
            }
            else
            {
                SetGlobalMode();
                CueBallMode = false;
            }

            camera.LookAt(camera.Position, CameraTarget, Vector3.Up);

            prevState = currState;
            base.Update(gameTime);


        }

        private float GetShootingAngle()
        {
            double result = 45;

            Vector3 cameraPosition = camera.Position;
            Vector3 ballPosition = ballCollection.getCueBallPosition();

            float a = Math.Abs(ballPosition.Z - cameraPosition.Z);
            float b = Math.Abs(ballPosition.X - cameraPosition.X);
            float c = (float)Math.Sqrt(a * a + b * b);
            float ac = a / c;
            float angleA = (float)Math.Asin(ac);

            if (cameraPosition.X < ballPosition.X)
            {
                if (cameraPosition.Z < ballPosition.Z)
                    result = MathHelper.PiOver2 - angleA;
                else
                    result = MathHelper.PiOver2 + angleA;
            }
            else
            {
                if (cameraPosition.Z < ballPosition.Z)
                    result = 3 * MathHelper.PiOver2 + angleA;
                else
                    result = 3 * MathHelper.PiOver2 - angleA;
            }


            return (float)result;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // Draw The Table
            foreach (ModelMesh mesh in table.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = TableWorld;
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }
                mesh.Draw();
            }

            foreach (ModelMesh mesh in Skybox.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    Matrix skyboxWorld = TableWorld * Matrix.CreateTranslation(new Vector3(0, 8.1f, 0));
                    skyboxWorld *= Matrix.CreateScale(0.4f);
                    effect.World = skyboxWorld;
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }
                mesh.Draw();
            }

            base.Draw(gameTime);
        }


        public void SetCueballMode()
        {
            CueBallMode = true;
            CameraTarget = ballCollection.getCueBallPosition();
            camera.OrbitMinZoom = .2f;
            camera.LookAt(camera.Position, CameraTarget, Vector3.Up);
        }

        private void SetGlobalMode()
        {
            //CueBallMode = false;

            camera.Move(Vector3.Right, Vector3.One * 4);

            //

            CameraTarget = Vector3.Zero;
            camera.OrbitMinZoom = 1f;
        }

    }
}
