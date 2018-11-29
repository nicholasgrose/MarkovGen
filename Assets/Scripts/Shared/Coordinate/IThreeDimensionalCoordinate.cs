using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Shared.Coordinate
{
    public interface IThreeDimensionalCoordinate : ITwoDimensionalCoordinate
    {
        int GetZ();
        void SetZ(int z);
    }
}
