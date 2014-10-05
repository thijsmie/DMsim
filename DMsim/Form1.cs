using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grid;

namespace DMsim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HexGrid hexgrid = new HexGrid();
            List<HexVector> p = hexgrid.getCanSee(new HexVector(0, 0), 3);
            List<String> s = new List<String>();
            foreach(HexVector k in p)
            {
                s.Add(k.ToString());
            }
            listBox1.Items.AddRange(s.ToArray());
        }
    }
}
