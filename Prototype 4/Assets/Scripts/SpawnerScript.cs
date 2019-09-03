using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Public
    public GameObject asteroid;

    // Private
    private float timerMax = 5.0f;
    private float timer;

    private void Awake()
    {
        timer = timerMax;

        // Seed random
        Random.InitState((int)Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Tick 
        timer -= Time.fixedDeltaTime;

        if (timer <= 0.0f)
        {
            timer = timerMax;

            float randomX;
            float randomY;

            // X or Y
            if (Random.Range(0.0f, 1.0f) >= 0.5f)
            {
                // X

                // Random coordinates
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
            else
            {
                // Y

                // Random coordinates
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

            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(randomX, randomY));

            // Spawn asteroid
            GameObject newAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity);

            // Random size
            if (Random.Range(0.0f, 1.0f) <= 0.33f)
            {
                // Large (Default is small)
                newAsteroid.GetComponent<AsteroidScript>().MakeLargeSprite();
            }

            // Give asteroid random velocity
            float asteroidSpeed = Random.Range(8.0f, 20.0f);
            Vector3 tempForce = (new Vector3(0.0f, 0.0f) - newAsteroid.transform.position).normalized * asteroidSpeed;
            Vector2 actualForce = new Vector2(tempForce.x, tempForce.y);
            newAsteroid.GetComponent<AsteroidScript>().forceVector = tempForce;
        }
    }
}
