using UnityEngine;

namespace _02___Magic_Words
{
    public static class Vector2Extensions
    {
        public static Vector2 WithValue(this Vector2 v, float? x = null, float? y = null)
        {
            if (x != null) v.x = (float)x;
            if (y != null) v.y = (float)y;
            return v;
        }
    }
}