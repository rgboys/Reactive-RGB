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
        public bool useAudio, isVert;

        /// <summary>
        /// New design calls that we assume 4 sections. Top, bottom, left, right. Only one section is going to get marked as 'use audio' if any.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isVert"></param>
        public Section(int x, int y, int width, int height, bool useAudio, bool isVert)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.useAudio = useAudio;
            this.isVert = isVert;
        }
    }
}
