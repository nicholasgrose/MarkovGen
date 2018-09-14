using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MiscUtils
{
    public static class ExtensionMethods
    {
        public static double Map(this double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
    }
}
