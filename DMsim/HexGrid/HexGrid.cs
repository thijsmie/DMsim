using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.HexGrid
{
    class TheGrid
    {
        public Dictionary<HexVector,GridObject> Grid = new Dictionary<HexVector,GridObject>();

        public void addSolid(HexVector vex)
        {
            if (Grid.ContainsKey(vex)) 
                return;
            Grid.Add(vex, new Solid());
        }

        public List<HexVector> getCanSee(HexVector vex,int maxRange)
        {
            List<HexVector> output = new List<HexVector>();
            foreach(HexVector outer in vex.Ring(maxRange))
            {
                foreach(HexVector test in vex.Line(outer))
                {
                    if (Grid.ContainsKey(test))
                    {
                        if (Grid[test] is Solid) 
                            break;
                    }
                    output.Add(test);
                }
            }
            return output;
        }
    }
}
