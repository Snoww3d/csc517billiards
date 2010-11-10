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
        private const float CAMERA_OFFSET = 0.5f;
        private const float CAMERA_BOUNDS_MIN_X = -FLOOR_WIDTH / 2.0f;
        private const float CAMERA_BOUNDS_MAX_X = FLOOR_WIDTH / 2.0f;
        private const float CAMERA_BOUNDS_MIN_Y = CAMERA_OFFSET;
        private const float CAMERA_BOUNDS_MAX_Y = 4.0f;
        private const float CAMERA_BOUNDS_MIN_Z = -FLOOR_HEIGHT / 2.0f;
        private const float CAMERA_BOUNDS_MAX_Z = FLOOR_HEIGHT / 2.0f;
        private int windowWidth;
        private int windowHeight;

        private CameraComponent camera;
        public Camera_Old camera_old;
        public BallCollection ballCollection;
        public GraphicsDeviceManager graphics;
        public Vector3 CameraTarget = new Vector3(0, 0, 0);



        protected Model table;

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
            windowWidth = GraphicsDevice.DisplayMode.Width;
            windowHeight = GraphicsDevice.DisplayMode.Height;

            camera = new CameraComponent(this);
            Components.Add(camera);

            GraphicsDevice device = graphics.GraphicsDevice;
            float aspectRatio = (float)windowWidth / (float)windowHeight;

            camera.Perspective(CAMERA_FOV, aspectRatio, CAMERA_ZNEAR, CAMERA_ZFAR);
            camera.Position = new Vector3(0.0f, CAMERA_OFFSET, 0.0f);
            camera.OrbitMinZoom = 1.5f;
            camera.OrbitMaxZoom = 5.0f;
            camera.OrbitOffsetDistance = camera.OrbitMinZoom;
            camera.CurrentBehavior = Camera.Behavior.Orbit;
            camera.Rotate(0.0f, -30.0f, 0.0f);
            camera.LookAt(CameraTarget);

            ballCollection = new BallCollection(this, camera);
            Components.Add(ballCollection);



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


            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                CameraTarget = ballCollection.CueBall.World.Translation;
                camera.OrbitMinZoom = .2f;
            }
            else
            {
                CameraTarget = Vector3.Zero;
                camera.OrbitMinZoom = 1f;
            }


            if (camera.Position.Y == 0f)
            {
                camera.PreferTargetYAxisOrbiting = true; 
            }
            camera.LookAt(camera.Position, CameraTarget, Vector3.Up);


            base.Update(gameTime);


            // ball1.World *= Matrix.CreateTranslation(0, 0, -MathHelper.PiOver2);

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

            base.Draw(gameTime);
        }
    }
}
