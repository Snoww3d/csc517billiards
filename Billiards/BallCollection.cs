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

        public BallCollection(Billiards.Game1 game, CameraComponent camera)
            : base(game)
        {
            this.game = game;
            Matrix CueBallWorld = Matrix.CreateWorld(new Vector3(-.68f, 0f, 0f), Vector3.Right, Vector3.Up);

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
                                "CueBall",
                                20,
                                ballRadius,
                                game.Content.Load<Texture2D>(@"Images/Ballcue"),
                                ref CueBallWorld,
                                camera);
            
            ball1 = new Sphere(game,
                                  "1",
                                  20,
                                  ballRadius,
                                  game.Content.Load<Texture2D>(@"Images/Ball1"),
                                  ref Ball1World,
                                  camera);

            ball2 = new Sphere(game,
                                "2",
                                20,
                                ballRadius,
                                game.Content.Load<Texture2D>(@"Images/Ball2"),
                                ref Ball2World,
                                camera);

            ball3 = new Sphere(game,
                                "3",
                                20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball3"),
                               ref Ball3World,
                               camera);

            ball4 = new Sphere(game,
                                "4",
                                20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball4"),
                               ref Ball4World,
                               camera);

            ball5 = new Sphere(game,
                                "5",
                                20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball5"),
                               ref Ball5World,
                               camera);

            ball6 = new Sphere(game,
                                 "6",

                               20,
                               ballRadius,
                               game.Content.Load<Texture2D>(@"Images/Ball6"),
                               ref Ball6World,
                               camera);

            ball7 = new Sphere(game,
                            "7",

                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball7"),
                            ref Ball7World,
                            camera);

            ball8 = new Sphere(game,
                                 "8",
                                 20,
                              ballRadius,
                              game.Content.Load<Texture2D>(@"Images/Ball8"),
                              ref Ball8World,
                              camera);

            ball9 = new Sphere(game,
                            "9",
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball9"),
                            ref Ball9World,
                            camera);


            ball10 = new Sphere(game,
                             "10",
                             20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball10"),
                            ref Ball10World,
                            camera);

            ball11 = new Sphere(game,
                            "11",
                           20,
                           ballRadius,
                           game.Content.Load<Texture2D>(@"Images/Ball11"),
                           ref Ball11World,
                           camera);

            ball12 = new Sphere(game,
                            "12",
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball12"),
                            ref Ball12World,
                            camera);

            ball13 = new Sphere(game,
                               "13",
                              20,
                              ballRadius,
                              game.Content.Load<Texture2D>(@"Images/Ball13"),
                              ref Ball13World,
                              camera);

            ball14 = new Sphere(game,
                            "14",
                            20,
                            ballRadius,
                            game.Content.Load<Texture2D>(@"Images/Ball14"),
                            ref Ball14World,
                            camera);


            ball15 = new Sphere(game,
                            "15",
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

            Random g = new Random();

            for (int i = 0; i < StationaryBalls.Count; i++)
            {

                Sphere ball = StationaryBalls[i];
                game.Components.Add(ball);
                //SetSpeedandAngle(ball,3,45);
                //i--;

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
            if (MovingBalls.Count != 0)
            {
                CheckForCollisions();
                //shotTime = 0;
                //if (Keyboard.GetState().IsKeyDown(Keys.Q) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                //{
                // }

               // CueBall.SetSpeedandAngle(10, (float)MathHelper.PiOver4);

                // ball1.SetSpeedandAngle(5, (float)3 * MathHelper.PiOver2);
                //MovingBalls.Add(ball1);
                //MovingBalls.Add(CueBall);
                // StationaryBalls.Remove(ball1);
                //StationaryBalls.Remove(CueBall);
                // CheckForCollisions();


            }
            //if (shotTime == 0)
                //CheckForCollisions();
            shotTime += 1000 / 60;

            //if (Collisions.Count > 0)
            //{

            //    if (shotTime >= Collisions[0].collisionTime)
            //    {
            //        Collisions[0].collidee.SetSpeedandAngle(Collisions[0].collider.speed, MathHelper.PiOver2);
            //        Collisions[0].collider.SetSpeedandAngle(0, 0);
            //        MovingBalls.Remove(Collisions[0].collider);
            //        StationaryBalls.Add(Collisions[0].collider);
            //        MovingBalls.Add(Collisions[0].collidee);

            //        Collisions = new List<Collision>();
            //        CheckForCollisions();
            //    }
            //}

            for (int i = 0; i < MovingBalls.Count;i++)
            {
                if (MovingBalls[i].speed <= 0)
                {
                    MovingBalls[i].SetSpeedandAngle(0f, 0f);
                    StationaryBalls.Add(MovingBalls[i]);
                    MovingBalls.RemoveAt(i);

                    i--;
                }
            }

            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void CheckForCollisions()
        {
            Vector3 translation = Vector3.Zero;
            int timebetweenframes = 1000/60;
            int time = 0;
            while (time < timebetweenframes)
            {
                for (int i = 0; i < MovingBalls.Count;i++)
                {
                    MovingBalls[i].World *= Matrix.CreateTranslation(MovingBalls[i].speed * MovingBalls[i].direction / 100);
                    translation += MovingBalls[i].speed * MovingBalls[i].direction / 1000;
                    for (int j = 0; j < MovingBalls.Count; j++)
                    {
                        if (CheckForCollisions(MovingBalls[i], MovingBalls[j]))
                        {
                           Sphere mball = MovingBalls[i];
                            Sphere cball = MovingBalls[j];
                            float dx = mball.World.Translation.X - cball.World.Translation.X;
                            float dz = mball.World.Translation.Z - cball.World.Translation.Z;
                            float result = 0;
                            float angle =(float) Math.Acos(dz/Math.Sqrt(dx*dx + dz * dz));
                            if (mball.World.Translation.X < cball.World.Translation.X)
                            {
                                if (mball.World.Translation.Z < cball.World.Translation.Z)
                                    result = MathHelper.PiOver2 - angle;
                                else
                                    result = MathHelper.PiOver2 + angle;
                            }
                            else
                            {
                                if (mball.World.Translation.Z < cball.World.Translation.Z)
                                    result = 3 * MathHelper.PiOver2 + angle;
                                else
                                    result = 3 * MathHelper.PiOver2 - angle;
                            }
                            SetSpeedandAngle(MovingBalls[j],MovingBalls[i].speed, result);
                            MovingBalls[i].SetSpeedandAngle(0, -result);

                            //cball.World *= Matrix.CreateTranslation(cball.speed / 50 * cball.direction);
                            break;
                        }
                        //int time = CheckForCollisions(MovingBalls[i], cball);
                        //if (time > 0)
                        //{
                        //    System.Console.WriteLine(string.Format(" {0} -- {1} at time {2}", MovingBalls[i].name, cball.name, time));

                        //    Collisions.Add(new Collision(game, time, MovingBalls[i], cball));
                        //}
                        Collisions = Collisions.OrderBy(x => x.collisionTime).ToList();
                     }

                    for (int j = 0; j < StationaryBalls.Count; j++)
                    {
                        if (CheckForCollisions(MovingBalls[i], StationaryBalls[j]))
                        {
                            Sphere mball = MovingBalls[i];
                            Sphere cball = StationaryBalls[j];
                            float dx = mball.World.Translation.X - cball.World.Translation.X;
                            float dz = mball.World.Translation.Z - cball.World.Translation.Z;
                            float result = 0;
                            float angle =(float) Math.Acos(dx/Math.Sqrt(dx*dx + dz * dz));
                            if (mball.World.Translation.Z < cball.World.Translation.Z)
                            {
                                if (mball.World.Translation.X < cball.World.Translation.X)
                                    result = -MathHelper.PiOver2 - angle;
                                else
                                    result = -MathHelper.PiOver2 + angle;
                            }
                            else
                            {
                                if (mball.World.Translation.X < cball.World.Translation.X)
                                    result = 3 * MathHelper.PiOver2 + angle;
                                else
                                    result = 3 * MathHelper.PiOver2 - angle;
                                
                            }
                            SetSpeedandAngle(StationaryBalls[j],MovingBalls[i].speed * 150, result);
                            MovingBalls[i].SetSpeedandAngle(.5f, -result);
                            
                            cball.World *= Matrix.CreateTranslation(mball.Radius * 2f * cball.direction);
                            mball.World *= Matrix.CreateTranslation(mball.Radius * 2f * mball.direction);
                            break;
                        }
                        //    int time = -1;
                        //    time = CheckForCollisions(MovingBalls[i], cball);
                        //    if (time > 0)
                        //    {
                        //        System.Console.WriteLine(string.Format(" {0} -- {1} at time {2}", MovingBalls[i].name, cball.name, time));

                        //        Collisions.Add(new Collision(game, time, MovingBalls[i], cball));
                        //    }
                        //    Collisions = Collisions.OrderBy(x => x.collisionTime).ToList();
                        //}
                        //System.Console.WriteLine(string.Format("END LOOP"));
                    }
                    MovingBalls[i].World *= Matrix.CreateTranslation(-translation); 
                }
                time += 1;
                translation = Vector3.Zero;
            }
        }

        private bool CheckForCollisions(Sphere mball, Sphere cball)
        {
            //System.Console.WriteLine(string.Format("checking {0} -- {1}", mball.name, cball.name));
            if (mball.Equals(cball))
                return false;
            float distance =(float) Math.Sqrt(Math.Pow(mball.World.Translation.X - cball.World.Translation.X, 2) +
                Math.Pow(mball.World.Translation.Z - cball.World.Translation.Z, 2));
            if (distance < 2 *  mball.Radius && !(mball.lastCollider.Equals(cball)))
            {
                mball.lastCollider = cball;
                cball.lastCollider = mball;
                return true;
            }
            mball.lastCollider = mball;
            cball.lastCollider = cball;
            return false;
            //if (cball.Equals(MovingBalls[i]))
            //    return -1;

            

            //float a = -0.000015f * 60;
            //float d1y = MovingBalls[i].direction.Z;
            //float p1y = MovingBalls[i].World.Translation.Z;
            //float d1x = MovingBalls[i].direction.X;
            //float p1x = MovingBalls[i].World.Translation.X;
            //float u1 = MovingBalls[i].speed;
            //float d2y = cball.direction.Z;

            //float p2y = cball.World.Translation.Z;
            //float d2x = cball.direction.X;
            //float p2x = cball.World.Translation.X;
            //float u2 = cball.speed;
            //float cy = (.5f * a * (d1y - d2y));
            //float cx = (.5f * a * (d1x - d2x));
            //float by = u2 * d2x - u1 * d1x;
            //float bx = u2 * d2y - u1 * d1y;
            //float ay = p2y - p1y;
            //float ax = p2x - p1x;
            //float A = (float)(Math.Pow(cy, 2) + Math.Pow(cx, 2));
            //float B = 2 * ((by * cy) + (bx * cx));
            //float C = 2 * ((ay * cy) + (ax * cx)) + by * by + bx * bx;
            //float D = 2 * ((ay * by) + (ax * bx));
            //float E = (float)(2 * Math.Pow(ay, 2) + Math.Pow(ax, 2) - 4 * cball.Radius * cball.Radius);
            //double[] sol = new double[4];
            //double[] soli = new double[4];
            //double[] dd = { A, B, C, D, E };
            //int nSol = 0;
            //int ctime = 0;
            //QuarticClass.Quartic(dd, out sol, out soli, out nSol);
            //if (nSol == 4)
            //{
            //    double min = 9999999;
            //    foreach (double d in sol)
            //    {
            //        if (d > 0 && d < min)
            //            min = d;
            //    }
            //    return ctime = (int)((1000 * min) + shotTime);
            //}
            //else if (nSol == 2)
            //{
            //    double min = 9999999;

            //    for (int i = 0; i < 2; i++)
            //    {
            //        if (sol[i] > 0 && sol[i] < min)
            //            min = sol[i];
            //    }
            //    return ctime = (int)((10000 * min) + shotTime);
            //}

            //return -1;
        }

        public bool AllBallsStopped()
        {
            return this.MovingBalls.Count == 0;
        }

        public void SetSpeedandAngle(Sphere ball, float Speed, float Angle)
        {
            ball.SetSpeedandAngle(Speed, Angle);
            if (StationaryBalls.Contains(ball))
                StationaryBalls.Remove(ball);
            if(!MovingBalls.Contains(ball))
                MovingBalls.Add(ball);
        }

        public Vector3 getCueBallPosition()
        { 
          return CueBall.World.Translation;
        }
    }
}