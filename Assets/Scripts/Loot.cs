using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingSloth.LD40
{
    public class Loot : MonoBehaviour
    {
        public float value = 1f;

        float spawnTime;

        public float lifetime = 5f;

        void OnEnable()
        {
            spawnTime = Time.time;
        }

        void Update()
        {
            if (Time.time - spawnTime > lifetime)
                Destroy();
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}