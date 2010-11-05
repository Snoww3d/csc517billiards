using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Billiards
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        float azimuthAngle = 0, altitudeAngle = 0, distance = 4;
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        public Camera(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            float viewAngle = MathHelper.PiOver4;
            float aspectRatio = Game.GraphicsDevice.Viewport.AspectRatio;
            float nearPlane = 0.5f;
            float farPlane = 1000.0f;
            Projection = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearPlane, farPlane);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                azimuthAngle = MathHelper.Clamp(-MathHelper.Pi + ((float)ms.X / Game.GraphicsDevice.Viewport.Width) * MathHelper.TwoPi, -MathHelper.Pi, MathHelper.Pi);
                altitudeAngle = MathHelper.Clamp(((float)ms.Y / Game.GraphicsDevice.Viewport.Height) * MathHelper.PiOver2, .01f, MathHelper.PiOver2 - .01f);
            }
            else if (ms.RightButton == ButtonState.Pressed)
                distance = MathHelper.Clamp(2 + ((float)ms.Y / Game.GraphicsDevice.Viewport.Width) * 4, 2f, 6f);

            View = Matrix.CreateRotationY(azimuthAngle);
            View *= Matrix.CreateRotationX(altitudeAngle);
            View *= Matrix.CreateTranslation(0, 0, -distance);

            base.Update(gameTime);
        }
    }
}