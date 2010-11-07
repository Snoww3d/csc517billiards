using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Billiards
{
    class Rail
    {
        public Vector3 size;
        public Vector3 position;

        public Rail(Vector3 size, Vector3 position)
        {
            this.size = size;
            this.position = position;
        }
    }
}
