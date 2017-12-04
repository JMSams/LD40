using UnityEngine;
using System.Collections;

namespace FallingSloth.LD40
{
    public class LootSpawner : MonoBehaviour
    {
        public Loot lootPrefab;
        Pool<Loot> lootPool;

        public float minSpawnDelay = 0.25f, maxSpawnDelay = 1f;
        public float minLootValue = 1f, maxLootValue = 3f;

        [HideInInspector]
        public bool spawnLoot = true;

        Vector2 minSpawnPoint, maxSpawnPoint;

        void Start()
        {
            lootPool = new Pool<Loot>(lootPrefab, 10);

            minSpawnPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            maxSpawnPoint = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelRect.xMax, Camera.main.pixelRect.yMax));

            StartCoroutine(SpawnLoot());
        }

        IEnumerator SpawnLoot()
        {
            while (spawnLoot)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

                Loot temp = lootPool.GetObject();
                temp.value = Random.Range(minLootValue, maxLootValue);
                temp.transform.position = new Vector2(Random.Range(minSpawnPoint.x, maxSpawnPoint.x), Random.Range(minSpawnPoint.y, maxSpawnPoint.y));
                temp.gameObject.SetActive(true);
            }
        }
    }
}