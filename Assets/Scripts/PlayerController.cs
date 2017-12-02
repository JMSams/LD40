using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FallingSloth.LD40
{
    public class PlayerController : MonoBehaviour
    {
        new Rigidbody2D rigidbody2D;

        public float deadzone = .1f;

        public float moveSpeed = 2f;

        Pool<Projectile> bulletPool;

        public Projectile bulletPrefab;

        public float fireForce = 10f;

        public float delayBetweenShots = .1f;
        float timeOfLastShot = 0f;
        
        public float lootValue = 0f;
        public Text lootText;

        public float score = 0f;
        public Text scoreText;

        public float pointsPerLootPerSecond = 1f;

        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();

            bulletPool = new Pool<Projectile>(bulletPrefab, 20);
        }

        void Update()
        {
            #region Mouse Fire
#if UNITY_WEBGL || UNITY_EDITOR
            if (Input.GetMouseButton(0))
            { // If the left mouse button is being held down, Fire
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                Fire(direction.normalized);
            }
#endif
            #endregion
            #region Touch input Fire
#if UNITY_ANDROID
            // Android code for on-screen thumbsticks
#endif
            #endregion
            
            UpdateUI();
        }
        
        void FixedUpdate()
        {
            Vector2 moveDirection = Vector2.zero;

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > deadzone)
                moveDirection.x = Input.GetAxis("Horizontal");

            if (Mathf.Abs(Input.GetAxis("Vertical")) > deadzone)
                moveDirection.y = Input.GetAxis("Vertical");
            
            Vector2 v = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;

            rigidbody2D.velocity = v;
        }

        void Fire(Vector2 direction)
        {
            if (Time.time - timeOfLastShot >= delayBetweenShots)
            {
                Projectile temp = bulletPool.GetObject();
                temp.transform.position = transform.position + new Vector3(0f, 0f, temp.transform.position.z);
                temp.gameObject.SetActive(true);
                temp.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * fireForce, ForceMode2D.Impulse);
                timeOfLastShot = Time.time;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Loot":
                    Loot loot = collision.gameObject.GetComponent<Loot>();
                    lootValue += loot.value;
                    loot.Destroy();
                    break;
            }
        }

        void UpdateUI()
        {
            lootText.text = "Loot: " + lootValue;
            scoreText.text = "Score: " + score;
        }

        IEnumerator AddPoints()
        {
            yield return new WaitForSeconds(1f);

            score += lootValue * pointsPerLootPerSecond;
        }
    }
}