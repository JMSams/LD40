using UnityEngine;
using System.Collections;

namespace FallingSloth.LD40
{
    public class Projectile : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Border")
            {
                Die();
            }
        }

        void Die()
        {
            gameObject.SetActive(false);
        }
    }
}