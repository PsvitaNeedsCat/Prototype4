using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Public
    public GameObject asteroidRef;

    // Private
    private float timer;
    private float timerMax = 5.0f;

    private void Awake()
    {
        Random.InitState((int)Time.deltaTime);
    }

    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0.0f)
        {
            timer = timerMax;

            // Spawn an asteroid at a random location off screen

            float randomX;
            float randomY;

            // Top or bottom
            if (Random.Range(0.0f, 1.0f) >= 0.5f)
            {
                randomX = Random.Range(0.0f, 1.0f);

                if (Random.Range(0.0f, 1.0f) >= 0.5f)
                {
                    // Top
                    randomY = 1.1f;
                }
                else
                {
                    // Bottom
                    randomY = -0.1f;
                }
            }
            // Left or right
            else
            {
                randomY = Random.Range(0.0f, 1.0f);

                if (Random.Range(0.0f, 1.0f) >= 0.5f)
                {
                    // Left
                    randomX = -0.1f;
                }
                else
                {
                    // Right
                    randomX = 1.1f;
                }
            }

            // Spawn asteroid
            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0.0f));

            GameObject newAsteroid = Instantiate(asteroidRef, spawnPos, Quaternion.identity);

            // Random size
            if (Random.Range(0.0f, 1.0f) <= 0.33)
            {
                newAsteroid.GetComponent<AsteroidScript>().MakeLarge();
            }

            // Random speed
            float asteroidSpeed = Random.Range(8.0f, 20.0f);

            // Add force towards centre
            Vector2 asteroidForce = (new Vector3(0.0f, 0.0f, 0.0f) - newAsteroid.transform.position).normalized * asteroidSpeed;

            newAsteroid.GetComponent<AsteroidScript>().forceVector = asteroidForce;
        }
    }
}
