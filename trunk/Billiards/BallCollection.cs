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
    public class BallCollection : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game1 game;
        private float ballRadius = 0.035f;
        private int shotTime = 0;
        public List<Sphere> StationaryBalls = new List<Sphere>();
        public List<Sphere> MovingBalls = new List<Sphere>();
        public List<Collision> Collisions = new List<Collision>();


        public Sphere CueBall;
        public Sphere ball1;
        public Sphere ball2;
        public Sphere ball3;
        public Sphere ball4;
        public Sphere ball5;
        public Sphere ball6;
        public Sphere ball7;
        public Sphere ball8;
        public Sphere ball9;
        public Sphere ball10;
        public Sphere ball11;
        public Sphere ball12;
        public Sphere ball13;
        public Sphere ball14;
        public Sphere ball15;

        public BallCollection(Billiards.Game1 game, Camera camera)
            : base(game)
        {
            this.game = game;
            Matrix CueBallWorld = Matrix.CreateWorld(new Vector3(-1.1f, 0f, 0f), Vector3.Right, Vector3.Up);

            Matrix Ball1World = Matrix.CreateWorld(new Vector3(.58f, 0f, 0f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball15World = Matrix.CreateWorld(new Vector3(.63f, 0f, 0.05f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball9World = Matrix.CreateWorld(new Vector3(.63f, 0f, -0.05f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball2World = Matrix.CreateWorld(new Vector3(.68f, 0f, 0.1f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball8World = Matrix.CreateWorld(new Vector3(.68f, 0f, 0.0f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball6World = Matrix.CreateWorld(new Vector3(.68f, 0f, -0.1f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball14World = Matrix.CreateWorld(new Vector3(.73f, 0f, 0.15f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball10World = Matrix.CreateWorld(new Vector3(.73f, 0f, 0.05f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball7World = Matrix.CreateWorld(new Vector3(.73f, 0f, -0.05f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball13World = Matrix.CreateWorld(new Vector3(.73f, 0f, -0.15f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball3World = Matrix.CreateWorld(new Vector3(.78f, 0f, 0.2f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball11World = Matrix.CreateWorld(new Vector3(.78f, 0f, 0.1f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball4World = Matrix.CreateWorld(new Vector3(.78f, 0f, 0.0f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball12World = Matrix.CreateWorld(new Vector3(.78f, 0f, -0.1f), new Vector3(1, -4f, 1), Vector3.Up);
            Matrix Ball5World = Matrix.CreateWorld(new Vector3(.78f, 0f, -0.2f), new Vector3(1, -4f, 1), Vector3.Forward);


            CueBall = new Sphere(game,
                                20,
                                ballRadius,
                                game.Content.Load<Texture2D>(@"Images/Ballcue"),
                                ref CueBallWorld,
                                camera);

            //CueBall.SetSpeedandAngle(1, 90);

            ball1 = new Sphere(game,
                                  20,
                                  ballRadius,
                                  game.Content.Load<Texture2D>(@"Images/Ball1"),
                                  ref Ball1World,
                                  camera);

            ball2 = new Sphere(game,
                                20,
                                ballRadius,
                                game.Content.Load<Texture2D>(@"Images/Ball2"),
                                ref Ball2World,
                                camera);

            ball3 = new Sphere(game,
                               20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball3"),
                               ref Ball3World,
                               camera);

            ball4 = new Sphere(game,
                               20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball4"),
                               ref Ball4World,
                               camera);

            ball5 = new Sphere(game,
                               20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball5"),
                               ref Ball5World,
                               camera);

            ball6 = new Sphere(game,
                               20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball6"),
                               ref Ball6World,
                               camera);

            ball7 = new Sphere(game,
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball7"),
                            ref Ball7World,
                            camera);

            ball8 = new Sphere(game,
                              20,
                              ballRadius,
                              game.Content.Load<Texture2D>(@"Images/Ball8"),
                              ref Ball8World,
                              camera);

            ball9 = new Sphere(game,
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball9"),
                            ref Ball9World,
                            camera);


            ball10 = new Sphere(game,
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball10"),
                            ref Ball10World,
                            camera);
            ball11 = new Sphere(game,
                           20,
                           ballRadius,
                           game.Content.Load<Texture2D>(@"Images/Ball11"),
                           ref Ball11World,
                           camera);

            ball12 = new Sphere(game,
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball12"),
                            ref Ball12World,
                            camera);

            ball13 = new Sphere(game,
                              20,
                              ballRadius,
                              game.Content.Load<Texture2D>(@"Images/Ball13"),
                              ref Ball13World,
                              camera);

            ball14 = new Sphere(game,
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball14"),
                            ref Ball14World,
                            camera);


            ball15 = new Sphere(game,
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball15"),
                            ref Ball15World,
                            camera);

            StationaryBalls.Add(CueBall);
            StationaryBalls.Add(ball1);
            StationaryBalls.Add(ball2);
            StationaryBalls.Add(ball3);
            StationaryBalls.Add(ball4);
            StationaryBalls.Add(ball5);
            StationaryBalls.Add(ball6);
            StationaryBalls.Add(ball7);
            StationaryBalls.Add(ball8);
            StationaryBalls.Add(ball9);
            StationaryBalls.Add(ball10);
            StationaryBalls.Add(ball11);
            StationaryBalls.Add(ball12);
            StationaryBalls.Add(ball13);
            StationaryBalls.Add(ball14);
            StationaryBalls.Add(ball15);

            foreach (Sphere ball in StationaryBalls)
            {
                game.Components.Add(ball);
            }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {


            shotTime += 1000 / 60;
            if (MovingBalls.Count == 0)
                shotTime = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //CueBall.SetSpeedandAngle(5, (float)(-1 * MathHelper.PiOver2 +
                    //Math.Asin((CueBall.camera.View.Translation.X - CueBall.World.Translation.X)
                   // / Math.Sqrt(Math.Pow((CueBall.camera.View.Translation.X
                   // - CueBall.World.Translation.X), 2) +
                   // Math.Pow((CueBall.camera.View.Translation.Y
                   // - CueBall.World.Translation.Y), 2)))));
                CueBall.SetSpeedandAngle(5, (float)MathHelper.PiOver2);
                MovingBalls.Add(CueBall);
                StationaryBalls.Remove(CueBall);
                CheckForCollisions();
            }
            CueBall.World *= Matrix.CreateTranslation(CueBall.direction * CueBall.speed);

            if (Collisions.Count > 0 && shotTime >= Collisions[0].collisionTime)
            {
                Collisions[0].collidee.SetSpeedandAngle(Collisions[0].collider.speed, MathHelper.PiOver2);
                Collisions[0].collider.SetSpeedandAngle(0, 0);
                MovingBalls.Remove(Collisions[0].collider);
                MovingBalls.Add(Collisions[0].collidee);
                Collisions.Remove(Collisions[0]);
                CheckForCollisions();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void CheckForCollisions()
        {
            foreach (Sphere mball in MovingBalls)
            {
                float a = .0005f;
                float d1y = mball.direction.Z;
                float p1y = mball.World.Translation.Z;
                float d1x = mball.direction.X;
                float p1x = mball.World.Translation.X;
                float u1 = mball.speed;
                foreach (Sphere cball in MovingBalls)
                {
                    float d2y = cball.direction.Z;
                    float p2y = cball.World.Translation.Z;
                    float d2x = cball.direction.X;
                    float p2x = cball.World.Translation.X;
                    float u2 = cball.speed;
                    float cy = (.5f * (d1y - d2y));
                    float cx = (.5f * (d1x - d2x));
                    float by = u2 * d2x - u1 * d1x;
                    float bx = u2 * d2y - u1 * d1y;
                    float ay = p2y;
                    float ax = p2x;
                    float A = (float)(Math.Pow(cy, 2) + Math.Pow(cx, 2));
                    float B = 2 * ((by * cy) + (bx * cx));
                    float C = 2 * ((ay * cy) + (ax * cx));
                    float D = 2 * ((ay * by) + (ax * bx));
                    float E = (float)(2 * Math.Pow(p2x, 2) + Math.Pow(p2x, 2) - 4 * cball.Radius * cball.Radius);
                    double[] sol = new double[4];
                    double[] soli = new double[4];
                    double[] dd = { A, B, C, D, E };
                    int nSol = 0;
                    int ctime = 0;
                    QuarticClass.Quartic(dd, out sol, out soli, out nSol);
                    if (nSol == 4)
                    {
                        ctime = (int)(1000 * Math.Min(Math.Min(sol[0], sol[1]), Math.Min(sol[2], sol[3])) + shotTime);
                    }
                    else if (nSol == 2)
                        ctime = (int)(1000 * Math.Min(sol[0], sol[1]) + shotTime);
                    else
                        continue;
                    Collisions.Add(new Collision(game, ctime, mball, cball));
                    Collisions = Collisions.OrderBy(x => x.collisionTime).ToList();
                }
                foreach (Sphere cball in StationaryBalls)
                {
                    float d2y = cball.direction.Z;
                    float p2y = cball.World.Translation.Z;
                    float d2x = cball.direction.X;
                    float p2x = cball.World.Translation.X;
                    float u2 = cball.speed;
                    float cy = (.5f * (d1y - d2y));
                    float cx = (.5f * (d1x - d2x));
                    float by = u2 * d2x - u1 * d1x;
                    float bx = u2 * d2y - u1 * d1y;
                    float ay = p2y;
                    float ax = p2x;
                    float A = (float)(Math.Pow(cy, 2) + Math.Pow(cx, 2));
                    float B = 2 * ((by * cy) + (bx * cx));
                    float C = 2 * ((ay * cy) + (ax * cx));
                    float D = 2 * ((ay * by) + (ax * bx));
                    float E = (float)(2 * Math.Pow(p2x, 2) + Math.Pow(p2x, 2) - 4 * cball.Radius * cball.Radius);
                    double[] sol = new double[4];
                    double[] soli = new double[4];
                    double[] dd = { A, B, C, D, E };
                    int nSol = 0;
                    int ctime = 0;
                    QuarticClass.Quartic(dd, out sol, out soli, out nSol);
                    if (nSol == 4)
                        ctime = (int)(1000 * Math.Min(Math.Min(sol[0], sol[1]), Math.Min(sol[2], sol[3])) + shotTime);
                    else if (nSol == 2)
                        ctime = (int)(1000 * Math.Min(sol[0], sol[1]) + shotTime);
                    else
                        continue;
                    Collisions.Add(new Collision(game, ctime, mball, cball));
                    Collisions = Collisions.OrderBy(x => x.collisionTime).ToList();
                }
            }
        }
    }
}