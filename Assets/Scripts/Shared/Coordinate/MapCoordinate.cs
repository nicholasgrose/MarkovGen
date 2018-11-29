using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Shared.Coordinate
{
    public class MapCoordinate : IThreeDimensionalCoordinate
    {
        private int _x;
        private int _y;
        private int _z;

        public MapCoordinate(int x, int y)
        {
            _x = x;
            _y = y;
            _z = 0;
        }

        public MapCoordinate(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public int GetX()
        {
            return _x;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public int GetY()
        {
            return _y;
        }

        public void SetY(int y)
        {
            _y = y;
        }

        public int GetZ()
        {
            return _z;
        }

        public void SetZ(int z)
        {
            _z = z;
        }
    }
}
