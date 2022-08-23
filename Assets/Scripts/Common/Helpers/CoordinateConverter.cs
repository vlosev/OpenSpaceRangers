using System;
using UnityEngine;

namespace Helpers
{
    public struct PolarCoordinate
    {
        /// <summary>
        /// полярный радиус
        /// </summary>
        public float r;
        
        /// <summary>
        /// полярный угол
        /// </summary>
        public float phi;

        public PolarCoordinate(float r, float phi)
        {
            this.r = r;
            this.phi = phi;
        }
    }
    
    /// <summary>
    /// конвертер полярных координат в декартовы и обратно
    /// https://ru.wikipedia.org/wiki/%D0%9F%D0%BE%D0%BB%D1%8F%D1%80%D0%BD%D0%B0%D1%8F_%D1%81%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D0%B0_%D0%BA%D0%BE%D0%BE%D1%80%D0%B4%D0%B8%D0%BD%D0%B0%D1%82#%D0%A1%D0%B2%D1%8F%D0%B7%D1%8C_%D0%BC%D0%B5%D0%B6%D0%B4%D1%83_%D0%B4%D0%B5%D0%BA%D0%B0%D1%80%D1%82%D0%BE%D0%B2%D1%8B%D0%BC%D0%B8_%D0%B8_%D0%BF%D0%BE%D0%BB%D1%8F%D1%80%D0%BD%D1%8B%D0%BC%D0%B8_%D0%BA%D0%BE%D0%BE%D1%80%D0%B4%D0%B8%D0%BD%D0%B0%D1%82%D0%B0%D0%BC%D0%B8
    /// </summary>
    public static class CoordinateConverter
    {
        public static Vector2 PolarToCartesian(this PolarCoordinate polarCoordinate)
        {
            return PolarToCartesian(polarCoordinate.r, polarCoordinate.phi);
        }

        public static Vector2 PolarToCartesian(float r, float phi)
        {
            return new Vector2(r * Mathf.Cos(phi), r * Mathf.Sin(phi));
        }

        public static PolarCoordinate CartesianToPolar(this Vector2 coordinate)
        {
            return CartesianToPolar(coordinate.x, coordinate.y);
        }

        public static PolarCoordinate CartesianToPolar(float x, float y)
        {
            var r = Mathf.Sqrt(x * x + y * y);
            var phi = x switch
            {
                > 0f when y >= 0f => Mathf.Atan(y / x),
                > 0f => Mathf.Atan(y / x) + 2f * Mathf.PI,
                < 0f => Mathf.Atan(y / x) + Mathf.PI,
                _ => y switch
                {
                    > 0f => Mathf.PI / 2f,
                    < 0f => 3f * Mathf.PI / 2f,
                    _ => float.NaN
                }
            };

            return new PolarCoordinate(r, phi);
        }

        #region tests

        

        #endregion
    }
}