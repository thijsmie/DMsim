using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.HexGrid
{
    struct HexVector
    {
        //Variables
        private int i, j;

        //Constructors
        public HexVector(int i, int j)
        {
           this.i = 0;
           this.j = 0;

           I = i;
           J = j;
        }

        public HexVector (int[] ij)
        {
           this.i = 0;
           this.j = 0;

           Array = ij;
        }

        public HexVector(HexVector v1)
        {
           this.i = 0;
           this.j = 0;

           I = v1.I;
           J = v1.J;
        }

        public HexVector(double q, double r)
        {
            this.i = 0;
            this.j = 0;

            int Q = (int)Math.Round(q);
            int R = (int)Math.Round(r);

            double k = -q - r;
            int K = (int)Math.Round(k);

            double dx = Math.Abs(Q - q);
            double dy = Math.Abs(R - r);
            double dz = Math.Abs(K - k);

            if (dx > dy && dx > dz)
            {
                I = -R - K;
                J = R;
            }
            else if (dy > dz)
            {
                I = Q;
                J = -Q - K;
            }
            else
            {
                I = Q;
                J = R;
            }
        }

        public HexVector(double[] qr)
        {
            this.i = 0;
            this.j = 0;
            double q = qr[0], r = qr[1];

            int Q = (int)Math.Round(q);
            int R = (int)Math.Round(r);

            

            double k = -q - r;
            int K = (int)Math.Round(k);

            double dx = Math.Abs(Q - q);
            double dy = Math.Abs(R - r);
            double dz = Math.Abs(K - k);

            if (dx > dy && dx > dz)
            {
                I = -Q - K;
                J = R;
            }
            else if (dy > dz)
            {
                I = Q;
                J = -Q - K;
            }
            else
            {
                I = Q;
                J = R;
            }
        }

        //Access Methods
        public int I
        {
            get { return i; }
            set { i = value; }
        }

        public int J
        {
            get { return j; }
            set { j = value; }
        }

        public int[] Array
        {
            get { return new int[] { i, j }; }
            set
            {
                if (value.Length == 2)
                {
                    i = value[0];
                    j = value[1];
                }
                else
                {
                    throw new ArgumentException(IS_NOT_2D);
                }
            }
        }

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: { return I; }
                    case 1: { return J; }
                    default: throw new ArgumentException(IS_NOT_2D, "index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: { I = value; break; }
                    case 1: { J = value; break; }
                    default: throw new ArgumentException(IS_NOT_2D, "index");
                }
            }
        }

        //Exceptions:
        private const string IS_NOT_2D = "A HexVector is 2D, please supply (i,j)";
        private const string ILLIGAL_OPERATION = "Length is read-only!";

        //Operations and calculations
        public int Length
        {
            get
            {
                return (Math.Abs(I) + Math.Abs(J) + Math.Abs(I + J)) / 2;
            }
            set
            {
                throw new ArgumentException(ILLIGAL_OPERATION); 
            }
        }

        public static HexVector operator +(HexVector v1, HexVector v2)
        {
            return
            (
               new HexVector
               (
                  v1.I + v2.I,
                  v1.J + v2.J
               )
            );
        }

        public static HexVector operator -(HexVector v1, HexVector v2)
        {
            return
            (
               new HexVector
               (
                  v1.I - v2.I,
                  v1.J - v2.J
               )
            );
        }

        public static HexVector operator -(HexVector v1)
        {
            return
            (
               new HexVector
               (
                  - v1.I,
                  - v1.J
               )
            );
        }

        public static HexVector operator +(HexVector v1)
        {
            return
            (
               new HexVector
               (
                  + v1.I,
                  + v1.J
               )
            );
        }

        //Comparisons
        public static bool operator <(HexVector v1, HexVector v2)
        {
            return v1.Length < v2.Length;
        }

        public static bool operator >(HexVector v1, HexVector v2)
        {
            return v1.Length > v2.Length;
        }

        public static bool operator <=(HexVector v1, HexVector v2)
        {
            return v1.Length <= v2.Length;
        }

        public static bool operator >=(HexVector v1, HexVector v2)
        {
            return v1.Length >= v2.Length;
        }

        public static bool operator ==(HexVector v1, HexVector v2)
        {
            return (v1.I == v2.I && v1.J == v2.J);
        }

        public static bool operator !=(HexVector v1, HexVector v2)
        {
            return !(v1 == v2);
        }

        //Advanced functionality
        
        ///<summary>
        /// Produce an array with HexVectors that form the line between the two supplied HexVectors
        ///</summary>
        public HexVector[] Line(HexVector v1,HexVector v2)
        {
            int N = (v1 - v2).Length;
            HexVector[] hexes = new HexVector[N+1];
            for(int i=0;i<=N;i++)
            {
                hexes[i] = new HexVector((double)v1.I * (1.0 - i / (double)N) + (double)v2.I * (i / (double)N), (double)v1.J * (1.0 - i / (double)N) + (double)v2.J * (i / (double)N));
            }
            return hexes;
        }

        ///<summary>
        /// Produce an array with HexVectors that forms the line to the other HexVector
        ///</summary>
        public HexVector[] Line(HexVector v1)
        {
            return Line(this, v1);
        }

        ///<summary>
        /// Produce an array with HexVectors that form the ring on distance R around the supplied Hex
        ///</summary>
        public HexVector[] Ring(HexVector v1,int R)
        {
            HexVector H = this.Neighbor(4, R);
            HexVector[] hexes = new HexVector[6*R];
            int k = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < R; j++ )
                {
                    hexes[k++] = H;
                    H = H.Neighbor(i);
                }
            return hexes;
        }

        ///<summary>
        /// Produce an array with HexVectors that form the ring on distance R around this Hex
        ///</summary>
        public HexVector[] Ring(int R)
        {
            return Ring(this, R);
        }

        public HexVector Neighbor(int direction,int distance = 1)
        {
            direction = direction % 6;
            switch(direction)
            {
                case 0:
                    return new HexVector(I, J - distance);
                case 1:
                    return new HexVector(I + distance, J - distance);
                case 2:
                    return new HexVector(I + distance, J);
                case 3:
                    return new HexVector(I, J + distance);
                case 4:
                    return new HexVector(I - distance, J + distance);
                case 5:
                    return new HexVector(I - distance, J);
                default:
                    return new HexVector(I, J);
            }
        }
    }
}
