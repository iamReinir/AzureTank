using System.Linq;
using UnityEngine;

namespace Const
{
    public static class Key
    {
        public const KeyCode LEFT = KeyCode.A;
        public const KeyCode RIGHT = KeyCode.D;
        public const KeyCode UP = KeyCode.W;
        public const KeyCode DOWN = KeyCode.S;
        public const KeyCode ROCKET = KeyCode.B;
    }
    public class CommonHelper
    {
        public float dropChance = 0.5f; // Drop chance is 50%
        
        // Ultility
        public System.Random random = new System.Random();
        public float Rand()
        {
            return ((float)random.NextDouble());
        }
        public GameObject GetRandomPickup(GameObject[] pickups)
        {
            if(pickups.Count() == 0) return null;
            int index = random.Next(pickups.Length);
            return pickups[index];
        }
    }
}
