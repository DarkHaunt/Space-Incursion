using UnityEngine;
using System;
using System.Linq;

namespace Game.Scripts.Extensions
{
    using Random = UnityEngine.Random;

    public static class RandomExtensions
    {
        public static int integer
            => Random.Range(int.MinValue, int.MaxValue);

        public static float floating
            => Random.Range(float.MinValue, float.MaxValue);

        public static float seed
            => floating % 13;

        public static bool boolean
            => Random.value > 0.5f;

        public static float rotationInFloat
            => Random.Range(-180f, 180f);

        public static Color color
            => new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), 1f);
        
        public static T EnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var index = Random.Range(0, values.Length);

            return (T)values.GetValue(index);
        }
        
        public static T EnumValueExcept<T>(params T[] excludedValues) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().Except(excludedValues).ToList();
    
            if (values.Count == 0)
                throw new ArgumentException("No values available after exclusion");

            var randomIndex = Random.Range(0, values.Count);
            return values[randomIndex];
        }

        public static Vector3 GetRandomPointBetweenTwo(Vector3 first, Vector3 second)
        {
            var random = Random.value;

            return (1 - random) * first + (random * second);
        }

        public static Vector3 GetRandomInBox(Vector3 leftBottomPoint, Vector3 rightBottomPoint)
        {
            float randomX = Random.Range(leftBottomPoint.x, rightBottomPoint.x);
            float randomY = Random.Range(leftBottomPoint.y, rightBottomPoint.y);
            float randomZ = Random.Range(leftBottomPoint.z, rightBottomPoint.z);
            
            return new Vector3(randomX, randomY, randomZ);;
        }
    }
}