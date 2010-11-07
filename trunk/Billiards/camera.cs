using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Billiards
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
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
            azimuthAngle = 0;
            altitudeAngle = 0;
            distance = 2;

            MouseState ms = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                azimuthAngle = MathHelper.Pi / 60;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                azimuthAngle = -MathHelper.Pi / 60;
               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                altitudeAngle = MathHelper.Pi / 60;
            
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                altitudeAngle = -MathHelper.Pi / 60;
            }
            if (ms.LeftButton == ButtonState.Pressed)
            {
                
                //azimuthAngle = MathHelper.Clamp(-MathHelper.Pi + ((float)ms.X / Game.GraphicsDevice.Viewport.Width) * MathHelper.TwoPi, -MathHelper.Pi, MathHelper.Pi);
              //  altitudeAngle = MathHelper.Clamp(((float)ms.Y / Game.GraphicsDevice.Viewport.Height) * MathHelper.PiOver2, .01f, MathHelper.PiOver2 - .01f);
            }
            else if (ms.RightButton == ButtonState.Pressed)
                distance = MathHelper.Clamp(2 + ((float)ms.Y / Game.GraphicsDevice.Viewport.Width) * 4, 2f, 6f);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                float distance2 = 1;
                if(ms.RightButton == ButtonState.Pressed)
                    distance2 = MathHelper.Clamp(1 + ((float)ms.Y / Game.GraphicsDevice.Viewport.Width) * 2, 1f, 3f);  
                View = Matrix.CreateLookAt(new Vector3(0, distance2, 0), game.ballCollection.CueBall.World.Translation, Vector3.Up);
               
                
               // View *= Matrix.CreateRotationY(azimuthAngle);
             

             

            }
            else
            {
                World *= Matrix.CreateRotationY(azimuthAngle);
                World *= Matrix.CreateRotationX(altitudeAngle);
               //World *= Matrix.CreateFromYawPitchRoll(
               //  azimuthAngle,
               //  0,
               //  0);
               //// ;
               // World *= Matrix.CreateRotationX(altitudeAngle);
            // World *= Matrix.CreateTranslation(0, 0, -distance / 100);
                //View = World;
               View = Matrix.CreateLookAt(World.Translation, new Vector3(0.001f, 0f, 0f), Vector3.Up);
            }
            base.Update(gameTime);
        }
    }
}