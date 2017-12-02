using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingSloth.LD40
{
    public class Loot : MonoBehaviour
    {
        public float value = 1f;

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}