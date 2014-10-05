using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid
{
    class GridObject
    {
        public HexVector Position;
        public int Clearance; //I'm planning on using this for "Clearance level"

        public int getDistance(GridObject other)
        {
            return (Position - other.Position).Length;
        }
    }
}
