using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns a random value in between the vector2 min and max values.
        /// </summary>
        public static float RandomValue(this Vector2 vector2)
        {
            return Random.Range(vector2.x, vector2.y);
        }

        /// <summary>
        /// Converts an angle of radians to a Vector2 pointing towards the angle.
        /// </summary>
        /// <param name="radian">Angle of the Vector2</param>
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// Converts an angle of radians to a Vector2 pointing towards the angle of a given length.
        /// </summary>
        /// <param name="radian">Angle of the Vector2</param>
        /// <param name="length">Length of the Vector2</param>
        public static Vector2 RadianToVector2(float radian, float length)
        {
            return RadianToVector2(radian) * length;
        }

        /// <summary>
        /// Converts an angle of radians to a Vector2 pointing towards the angle.
        /// </summary>
        /// <param name="degree">Angle of the Vector2</param>
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Converts an angle of radians to a Vector2 pointing towards the angle of a given length.
        /// </summary>
        /// <param name="degree">Angle of the Vector2</param>
        /// <param name="length">Length of the Vector2</param>
        public static Vector2 DegreeToVector2(float degree, float length)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad) * length;
        }
    }
}

