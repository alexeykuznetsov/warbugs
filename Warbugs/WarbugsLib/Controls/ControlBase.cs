using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarbugsLib.Core;

namespace WarbugsLib.Controls
{
    public abstract class ControlBase:BaseObject
    {
        public abstract bool IsTaped(float x, float y);
    }
}
