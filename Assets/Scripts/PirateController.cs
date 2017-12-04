using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingSloth.LD40
{
    public class PirateController : MonoBehaviour
    {
        [HideInInspector]
        public PirateManager manager;

        void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "PlayerProjectile":
                    // Add Points
                    break;
            }
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}