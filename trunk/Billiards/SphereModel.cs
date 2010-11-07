////////////////////////////////////////////////////////////////
// Creator: Thomas F. Hain
// Date:    12/7/09
// Purpose: Stack-tesselated model of sphere
////////////////////////////////////////////////////////////////
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Billiards
{
    public class Sphere : DrawableGameComponent
    {
        #region Members

        private int nVertices;
        private int nIndices;
        private short[] indices;
        private VertexPositionNormalTexture[] vertices;
        private short tesselations;    // number latitude strips = number longitude slices/2
        private float radius;
        private GraphicsDevice device;
        private VertexDeclaration vertexDeclaration;
        private Billiards.Camera camera;
        public float speed;
        public Vector3 direction;
        public double angle;
        #endregion

        #region Properties
        public Texture2D Texture { get; set; }
        public BasicEffect Effect { get; set; }
        public Matrix World { get; set; }
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        public float Radius
        {
            get { return radius; }
            set { radius = value; LoadContent(); }
        }

        // Sets or gets the number of tesselations (latitude strips = 2x longitudinal slices) of the sphere. 
        // Keep less than or equal to 127, otherwise number of indices will exceed capacity of short.
        // Typically, 20 suffices.
        public short Tesselations
        {
            get { return tesselations; }
            set { tesselations = value; LoadContent(); }
        }
        #endregion

        #region Constructors
        public Sphere(Billiards.Game1 game, short tesselations, float radius, Texture2D texture, ref Matrix world, Billiards.Camera camera)
            : base(game)
        {
            this.tesselations = tesselations;
            this.radius = radius;
            Texture = texture;
            World = world;
            this.camera = camera;
        }
        #endregion

        #region Overrides
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            device = ((Billiards.Game1)Game).GraphicsDevice;
            Effect = new BasicEffect(device, null);
            Effect.Projection = camera.Projection;
            Effect.View = camera.View;

            Effect.TextureEnabled = true;
            Effect.Texture = Texture;
            Effect.EnableDefaultLighting();
            Effect.SpecularColor = Vector3.Zero;

            vertexDeclaration = new VertexDeclaration(device, VertexPositionNormalTexture.VertexElements);
            nVertices = (2 * tesselations + 1) * (tesselations + 1);
            vertices = new VertexPositionNormalTexture[nVertices];
            nIndices = (2 * tesselations + 1) * (tesselations * 2);
            indices = new short[nIndices];
            createVertices(this.tesselations, this.radius, ref vertices, ref indices);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.speed > 0)
                this.speed -= 0.000015f;
            if (this.speed < 0)
                this.speed = 0;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.Visible)
            {
                Effect.Projection = camera.Projection;
                Effect.View = camera.View;
                Effect.World = World;
                Effect.Begin();
                foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    device.VertexDeclaration = vertexDeclaration;
                    device.DrawUserIndexedPrimitives(PrimitiveType.TriangleStrip, vertices, 0, nVertices, indices, 0, nIndices - 2);
                    pass.End();
                }
                Effect.End();

                base.Draw(gameTime);
            }
        }
        #endregion

        #region Private Methods
        private void createVertices(short tesselations, float radius, ref VertexPositionNormalTexture[] vertices, ref short[] indices)
        {
            // Fill vertex array (with normal, position, and texture coordinate)
            float StackAngle = MathHelper.Pi / tesselations;
            float y, r, sliceAngle;
            short slices = (short)(2 * tesselations);  // longitudinal slices (2 * tesselations)

            for (int stack = 0, ndxVertex = 0; stack <= tesselations; ++stack)
            {
                y = (float)Math.Cos((float)stack * StackAngle);
                r = (float)Math.Sqrt(1 - y * y);
                for (int slice = 0; slice <= slices; ++slice, ++ndxVertex)
                {
                    sliceAngle = StackAngle * slice;
                    vertices[ndxVertex].Normal = new Vector3(r * (float)Math.Sin(sliceAngle), y, -r * (float)Math.Cos(sliceAngle));
                    vertices[ndxVertex].Position = radius * vertices[ndxVertex].Normal;
                    vertices[ndxVertex].TextureCoordinate = new Vector2(1f - (float)slice / slices, (float)stack / tesselations);
                }
            }

            // Fill index array for TriangleStrip
            for (short stack = 0, ndxVertex = 0, ndxIndex = 0; stack < tesselations; ++stack)
                for (short slice = 0; slice <= slices; ++slice, ++ndxVertex)
                {
                    indices[ndxIndex++] = ndxVertex;
                    indices[ndxIndex++] = ((short)(ndxVertex + slices + 1));
                }
        }
        #endregion

        /// <summary>
        /// Sets the Speed and Angle of the ball!!
        /// </summary>
        /// <param name="Speed">Scale 0-10</param>
        /// <param name="Angle">in degrees on the z-axle</param>

        public void SetSpeedandAngle(float Speed, float Angle)
        {
            float TranslatedSpeed = Speed / 200f;
            this.speed = TranslatedSpeed;
            this.angle = MathHelper.ToRadians(Angle);
            this.direction = new Vector3((float)Math.Sin(this.angle), 0f, (float)Math.Cos(this.angle));

        }

    }
}