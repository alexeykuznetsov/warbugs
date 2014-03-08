using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarbugsLib.Lifeforms.Sprites
{
    public class LayerInfo
    {
        public string Name { get; set; }

        public int ZIndex { get; set; }

        public LayerType Type { get; set; }

        public int FrameWidth { get; set; }

        public int FramesCount { get; set; }

    }
}
