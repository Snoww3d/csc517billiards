////////////////////////////////////////////////////////////////
// Creator: Thomas F. Hain
// Date:    12/7/09
// Purpose: Stack-tesselated model of sphere
////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HainSphere
{
    public class Sphere : DrawableGameComponent
    {
        #region Members
        private int nVertices;
        private int nIndices;
        private ushort tesselations;    // number latitude strips = number longitude slices/2
        private float radius;
        private GraphicsDevice graphics;
        #endregion

        #region Properties
        /// <summary>
        /// Sets or gets the texture of the sphere.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Sets or gets the effect of the sphere.
        /// </summary>
        public BasicEffect Effect { get; set; }
        /// <summary>
        /// Sets or gets the radius of the sphere.
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set { radius = value; LoadContent(); }
        }
        /// <summary>
        /// Sets or gets the number of tesselations (latitude strips = 2x longitudinal slices) of the sphere. 
        /// Keep less than or equal to 127, otherwise number of indices will exceed capacity of ushort.
        /// Typically, 20 suffices.
        /// </summary>
        public ushort Tesselations
        {
            get { return tesselations; }
            set { tesselations = value; LoadContent(); }
        }
        /// <summary>
        /// Sets or gets the world matrix of the sphere.
        /// </summary>
        public Matrix World { get; set; }
        /// <summary>
        /// Sets or gets the view matrix of the sphere.
        /// </summary>
        public Matrix View { get; set; }
        /// <summary>
        /// Sets or gets the projection matrix of the sphere.
        /// </summary>
        public Matrix Projection { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the sphere class with a texture wrapping the sphere.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="tesselations"></param>
        /// <param name="radius"></param>
        /// <param name="texture"></param>
        /// <param name="view"></param>
        /// <param name="projection"></param>
        public Sphere(Game game, ushort tesselations, float radius, Texture2D texture, Matrix world, Matrix view, Matrix projection)
            : base(game)
        {
            this.tesselations = tesselations;
            this.radius = radius;
            Texture = texture;
            World = world;
            Projection = projection;
            View = view;
        }
        #endregion

        #region Override
        /// <summary>
        /// Initializes the sphere (base).
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Generate and load the model content for the sphere.
        /// </summary>
        protected override void LoadContent()
        {
            graphics = base.GraphicsDevice;
            Effect = new BasicEffect(graphics, null);
            Effect.Projection = Projection;
            Effect.View = View;

            Effect.TextureEnabled = true;
            Effect.Texture = Texture;
            Effect.EnableDefaultLighting();
            Effect.SpecularColor = Vector3.Zero;

            graphics.VertexDeclaration = new VertexDeclaration(graphics, VertexPositionNormalTexture.VertexElements);
            nVertices = (2 * tesselations + 1) * (tesselations + 1);
            nIndices = (2 * tesselations + 1) * (tesselations * 2);
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[nVertices];
            ushort[] indices = new ushort[nIndices];
            createVertices(this.tesselations, this.radius, ref vertices, ref indices);
            VertexBuffer vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionNormalTexture), nVertices, BufferUsage.WriteOnly);
            IndexBuffer indexBuffer = new IndexBuffer(graphics, typeof(ushort), indices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
            indexBuffer.SetData(indices);
            graphics.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionNormalTexture.SizeInBytes);
            graphics.Indices = indexBuffer;
            base.LoadContent();
        }

        /// <summary>
        /// Updates the sphere.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the sphere.
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            if (base.Visible)
            {
                Effect.World = World;
                Effect.Begin();
                foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    graphics.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, nVertices, 0, nIndices - 2);
                    pass.End();
                }
                Effect.End();

                base.Draw(gameTime);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// For sphere, create each vertex's location, normal, and texture coordinate, and well as triangle strip indices.
        /// </summary>
        /// <param name="Tesselations"> Number of latitude strips</param>
        /// <param name="Radius">Radius of sphere</param>
        /// <param name="vertices">vertex information array</param>
        /// <param name="indices">index array</param>
        private void createVertices(ushort tesselations, float radius, ref VertexPositionNormalTexture[] vertices, ref ushort[] indices)
        {
            // Fill vertex array (with normal, position, and texture coordinate)
            float StackAngle = MathHelper.Pi / tesselations;
            float y, r, sliceAngle;
            ushort slices = (ushort)(2 * tesselations);  // longitudinal slices (2 * tesselations)

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
            for (ushort stack = 0, ndxVertex = 0, ndxIndex = 0; stack < tesselations; ++stack)
                for (ushort slice = 0; slice <= slices; ++slice, ++ndxVertex)
                {
                    indices[ndxIndex++] = ndxVertex;
                    indices[ndxIndex++] = ((ushort)(ndxVertex + slices + 1));
                }
        }
        #endregion
    }
}