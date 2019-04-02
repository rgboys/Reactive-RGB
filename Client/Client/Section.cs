using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Section
    {
        public int x, y, width, height;
        public float x_rat, y_rat;
        public bool isVert;

        public Section(int x, int y, int width, int height, float x_rat, float y_rat, bool isVert)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.x_rat = x_rat;
            this.y_rat = y_rat;
            this.isVert = isVert;
        }
    }
}
