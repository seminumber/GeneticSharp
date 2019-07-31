using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspWpf
{
    public class TspCity
    {
        public double X { get; set; }
        public double Y { get; set; }

        public TspCity()
        {

        }

        public TspCity(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(TspCity other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
