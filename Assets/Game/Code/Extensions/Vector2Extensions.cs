using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Create(float value)
            => new Vector2(value, value);

        public static Vector2 Direction(Vector2 start, Vector2 end)
            => (end - start).normalized;
        
        public static Vector2 WithX(this Vector2 self, float xValue)
        {
            self.Set(xValue, self.y);
            return self;
        }

        public static Vector2 WithY(this Vector2 self, float yValue)
        {
            self.Set(self.x, yValue);
            return self;
        }
        
        public static Vector2 GetIncreased(this Vector2 self, float value)
        {
            self.x += value;
            self.y += value;

            return self;
        }    
    
        public static Vector2 GetIncreasedX(this Vector2 self, float value)
        {
            self.x += value;

            return self;
        }  
    
        public static Vector2 GetIncreasedY(this Vector2 self, float value)
        {
            self.y += value;

            return self;
        } 
    }
}