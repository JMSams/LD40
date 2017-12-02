using UnityEngine;
using System.Collections;

namespace FallingSloth.LD40
{
    public class LootSpawner : MonoBehaviour
    {
        public Loot lootPrefab;
        Pool<Loot> lootPool;

        void Start()
        {
            lootPool = new Pool<Loot>(lootPrefab, 10);
        }

        void Update()
        {
            // TODO: Spawn some loot
        }
    }
}