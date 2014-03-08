using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarbugsLib.Controls
{
    interface IControl
    {
        bool IsTaped(float x, float y);
    }
}
