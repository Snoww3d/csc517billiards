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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private HainSphere.Sphere BilliardBallTest;
        private HainSphere.Sphere BilliardBallTest2;
        protected Matrix world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Images";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //BilliardBallTest = new HainSphere.Sphere(this, 25, 20,
            //   Content.Load<Texture2D>("Ball10"),
            //   Matrix.Identity,
            //   Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 700.0f), Vector3.Zero, Vector3.Up),
            //   Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 1000.0f));
            //BilliardBallTest.Initialize();
            BilliardBallTest2 = new HainSphere.Sphere(this, 25, 20,
            Content.Load<Texture2D>("Ball1"),
            Matrix.Identity,
            Matrix.CreateLookAt(new Vector3(15.0f, 0.0f, 700.0f), Vector3.Zero, Vector3.Up),
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 1000.0f));
            BilliardBallTest2.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //this.Components.Add(BilliardBallTest);
            this.Components.Add(BilliardBallTest2);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            MouseState ms = Mouse.GetState();
            float yAngle = -180f + ((float)ms.X / GraphicsDevice.Viewport.Width) * 360f;
            float xAngle = -89f + ((float)ms.Y / GraphicsDevice.Viewport.Height) * 178f;
            world = Matrix.CreateRotationY(MathHelper.ToRadians(yAngle));
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(xAngle));
            //BilliardBallTest.World = world;
            BilliardBallTest2.World = world;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            base.Draw(gameTime);
        }
    }
}
