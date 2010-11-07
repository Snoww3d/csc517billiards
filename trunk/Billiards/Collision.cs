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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Collision : Microsoft.Xna.Framework.GameComponent
    {
        public int collisionTime;
        public Sphere collider;
        public Sphere collidee;
        public bool collideeMoving;

        public Collision(Game game, int collisionTime, Sphere collider, Sphere collidee)
            : base(game)
        {
            this.collisionTime = collisionTime;
            this.collider = collider;
            this.collidee = collidee;
            if (collidee.speed != 0)
                collideeMoving = true;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}