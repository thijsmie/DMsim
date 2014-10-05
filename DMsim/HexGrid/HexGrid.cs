using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid
    {
    class HexGrid
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
            double[,] ring = vex.RingD(maxRange);
            for(int i = 0;i<ring.Length-1;i++)
            {
                foreach(HexVector test in vex.Line(ring[i,0],ring[i,1]))
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
