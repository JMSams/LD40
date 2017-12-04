using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CnControls;
using FallingSloth.Audio;

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

        public float maxVelocity = 50f;
        
        public float lootValue = 0f;
        public Text lootText;

        public float score = 0f;
        public Text scoreText;

        public float pointsPerLootPerSecond = 1f;

        public string shootSoundName = "PlayerShoot";

        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();

            bulletPool = new Pool<Projectile>(bulletPrefab, 20);

            StartCoroutine(AddPoints());
        }

        void Update()
        {
            float x = CnInputManager.GetAxis("FireX");
            float y = CnInputManager.GetAxis("FireY");

            if (Mathf.Abs(x) > deadzone || Mathf.Abs(y) > deadzone)
                Fire(new Vector2(x, y).normalized);

            UpdateUI();
        }
        
        void FixedUpdate()
        {
            Vector2 moveDirection = Vector2.zero;

            if (Mathf.Abs(CnInputManager.GetAxis("Horizontal")) > deadzone)
                rigidbody2D.velocity = new Vector2(CnInputManager.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
            else
                rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);

            if (Mathf.Abs(CnInputManager.GetAxis("Vertical")) > deadzone)
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, CnInputManager.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime);
            else
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);

            rigidbody2D.velocity = rigidbody2D.velocity.Clamp(new Vector2(-maxVelocity, -maxVelocity), new Vector2(maxVelocity, maxVelocity));
        }

        void Fire(Vector2 direction)
        {
            if (Time.time - timeOfLastShot >= delayBetweenShots)
            {
                Projectile temp = bulletPool.GetObject();
                temp.transform.position = transform.position + new Vector3(0f, 0f, temp.transform.position.z);
                temp.gameObject.SetActive(true);
                temp.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * fireForce, ForceMode2D.Impulse);
                AudioManager.PlaySound(shootSoundName);
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
            lootText.text = string.Format("Loot: {0:0.0}", lootValue);
            scoreText.text = string.Format("Score: {0:0.0}", score);
        }

        IEnumerator AddPoints()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                score += lootValue * pointsPerLootPerSecond;
            }
        }
    }
}