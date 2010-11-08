// FrameRateDisplay Component
// T, Hain, Oct 2010
// Diagnostic game component for displaying frame rate on window title bar
using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Billiards
{
    public sealed class FrameRateDisplay : GameComponent
    {
        private double elapsedTimeSinceLastDisplay;
        private float frameCount = 0;

        public FrameRateDisplay(Game game) : base(game)
        {
            Game.TargetElapsedTime = game.TargetElapsedTime;
            ((GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager))).SynchronizeWithVerticalRetrace = Game.IsFixedTimeStep = !Debugger.IsAttached;
        }

        public sealed override void Initialize()
        {
            base.Initialize();
        }

        public sealed override void Update(GameTime gameTime)
        {
            frameCount++;
            elapsedTimeSinceLastDisplay += gameTime.ElapsedRealTime.TotalSeconds;
            if (elapsedTimeSinceLastDisplay > 1.0f) // update every second
            {
                Game.Window.Title = String.Format("Frames/Sec = {0}", frameCount);
                elapsedTimeSinceLastDisplay = frameCount = 0;
            }
            base.Update(gameTime);
        }
    }
}
