using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace UnityEngine
{
    public static class Vector3Extension
    {
        public static float GetDistanceSq( this Vector3 vec, Vector3 other )
        {
            float dx = vec.x - other.x;
            float dy = vec.y - other.y;
            float dz = vec.z - other.z;
            return dx * dx + dy * dy + dz * dz;
        }
    }

}