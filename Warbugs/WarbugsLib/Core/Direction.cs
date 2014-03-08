using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarbugsLib.Core
{
    public class Direction
    {
        private int _degrees;
        public int Degrees
        {
            get { return _degrees; }
            set
            {
                int val = value;
                while (val < 0 || val > 360)
                {
                    if (val > 360) val -= 360;
                    if (val < 0) val += 360;
                }
                _degrees = val;
                Radians = (float)(value * (Math.PI / 180));
            }
        }

        private float _radians
        {
            get { return _radians; }
            set
            {
                _radians = value;
            }

        }
        public float Radians { get; private set; }

        public Direction(int degrees)
        {
            Radians = (float)(degrees * (Math.PI / 180));
            Degrees = degrees;
        }

        public int GetRotationDir(int degrees)
        {
            if (degrees > Degrees)
            {
                if (degrees - Degrees > 180) return -1;
                else return 1;
            }
            if (degrees < Degrees)
            {
                if (Degrees - degrees > 180) return 1;
                else return -1;
            }
            return 0;


        }



    }
}
