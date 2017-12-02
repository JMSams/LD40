using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FallingSloth.LD40
{
    public static class ExtentionMethods
    {
        public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(value.x, min.x, max.x),
                                Mathf.Clamp(value.y, min.y, max.y));
        }
    }
}
