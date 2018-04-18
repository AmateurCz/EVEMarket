using System;
using System.Collections.Generic;
using System.Text;

namespace EVEMarket.Model
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public override string ToString()
        {
            return $"{X}; {Y}; {Z}";
        }
    }
}
