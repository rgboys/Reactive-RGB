using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Section
    {
        public int x, y;

        public bool isVert;

        public Section(int x, int y, bool isVert)
        {
            this.x = x;
            this.y = y;
            this.isVert = isVert;
        }
    }
}
