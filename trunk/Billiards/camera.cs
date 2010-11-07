using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Billiards
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {

        float radius = 3.0f;
        float theta = (3.0f * MathHelper.Pi) / 2.0f;
        float phi = MathHelper.Pi;

        Game1 game;
        float azimuthAngle = 0, altitudeAngle = 0, distance = 4;
        public Matrix World { get; set; }
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        public Camera(Game1 game)
            : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            World = Matrix.CreateWorld(new Vector3(1, 1, 6), Vector3.Forward, Vector3.Up);

            float viewAngle = MathHelper.PiOver4;
            float aspectRatio = Game.GraphicsDevice.Viewport.AspectRatio;
            float nearPlane = 0.5f;
            float farPlane = 1000.0f;
            Projection = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearPlane, farPlane);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            // modified from directx code
            // http://www.gamedev.net/community/forums/topic.asp?topic_id=443548

            MouseState ms = Mouse.GetState();
          
            if (ms.LeftButton == ButtonState.Pressed)
            {

                theta += MathHelper.Clamp(0.1f + ((float)ms.X / Game.GraphicsDevice.Viewport.Width) * 0.2f, -.1f, .1f);
                phi += MathHelper.Clamp(0.1f + ((float)ms.X / Game.GraphicsDevice.Viewport.Height) * 0.2f, -.1f,.1f);
            }
          
            if (ms.RightButton == ButtonState.Pressed)
                radius = MathHelper.Clamp(1 + ((float)ms.Y / Game.GraphicsDevice.Viewport.Width) * 2, 1f, 5f);

            float Cx, Cy, Cz;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))

                theta -= 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                theta += 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.N))
                radius += 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                radius -= 0.1f;

            radius = MathHelper.Clamp(radius, 1f, 5f);



            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                phi += 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                phi -= 0.1f;

            if (phi >= (MathHelper.Pi / 9) * 8)
            {
                phi = (MathHelper.Pi / 9) * 8;
            }
            if (phi <= (MathHelper.Pi / 9))
            {
                phi = (MathHelper.Pi / 9);
            }

            Cx = (float)(radius * Math.Cos((double)theta) * Math.Sin((double)phi));
            Cy = (float)(radius * Math.Cos((double)phi));
            Cz = (float)(radius * Math.Sin((double)theta) * Math.Sin((double)phi));

            if (Cy > -0.2f)
                Cy = -0.2f;


            World = Matrix.CreateWorld(new Vector3(Cx, -Cy, Cz), Vector3.Forward, Vector3.Up);

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                View = Matrix.CreateLookAt(World.Translation, game.ballCollection.CueBall.World.Translation, Vector3.Up);
            }
            else
            {
                View = Matrix.CreateLookAt(World.Translation, new Vector3(0.001f, 0f, 0f), Vector3.Up);
            }
          






           
       



                    base.Update(gameTime);
        }
    }
}