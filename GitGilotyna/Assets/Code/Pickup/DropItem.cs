using System;
using UnityEngine;

namespace Code.Pickup
{
    [Serializable]
    public struct DropItem
    {
        public GameObject lootObject;
        public int lootChance;
    }
}