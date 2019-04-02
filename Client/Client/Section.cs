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
        public bool isVert;

        public Section(int x, int y, int width, int height, bool isVert)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.isVert = isVert;
        }
    }
}
