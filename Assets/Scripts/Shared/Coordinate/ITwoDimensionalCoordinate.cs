using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Shared.Coordinate
{
    public interface ITwoDimensionalCoordinate
    {
        int GetX();
        void SetX(int x);
        int GetY();
        void SetY(int y);
    }
}
