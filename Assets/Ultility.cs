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

    public static class Tag
    {
        public const string player = "Player";
        public const string entity = "Entity";
        public const string enemy = "Enemy";
    }

    public class Layer
    {
        public const int data = 0;
        public const int background = 1;
        public const int subBackground = 2;
        public const int bullet = 6;
        public const int body = 6;
        public const int turret = 7;
    }

    public static class Scence
    {
        public const string CHAP1_1 = "Chapter1.1";
        public const string CHAP1_2 = "Chapter1.2";
        public const string CHAP1_3 = "Chapter1.3";
        public const string CHAP1_4 = "Chapter1.4";
        public const string CHAP1_5 = "Chapter1.5";
        public const string CHAP1_6 = "Chapter1.6";
    }
}
