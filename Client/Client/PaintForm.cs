using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class PaintForm
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public Pen p { get; set; }

        public PaintForm(Pen p, int x, int y, int width, int height)
        {
            this.p = p;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /*
        //If the objects are equal, don't add
        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(PaintForm))
            {
                PaintForm p = (PaintForm)obj;
                return (p.x == this.x && p.y == this.y && p.width == this.width && p.height == this.height);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        */
    }
}
