﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Section
    {
        public int x, y, width, height, subSections;
        public bool useAudio, isVert;

        /// <summary>
        /// Section describes the parent section's rect coordinates/size
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="useAudio"></param>
        /// <param name="isVert"></param>
        /// <param name="subSections"></param>
        public Section(int x, int y, int width, int height, bool useAudio, bool isVert, int subSections)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.useAudio = useAudio;
            this.isVert = isVert;
            this.subSections = subSections;
        }
    }
}
