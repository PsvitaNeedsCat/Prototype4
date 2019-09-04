using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBitScript : MonoBehaviour
{
    public GameObject player;
    public float timeSinceSpawn = 0.0F;
    public float attractionForce = 500.0F;
    public float spawnForce = 5000.0F;
    public Rigidbody2D body;
    public int score = 10;
    private float acceleration = 0.1f;
    private float speed = 0.5f;

    void Awake()
    {
        float x = Random.Range(-1.0F, 1.0F);
        float y = Random.Range(-1.0F, 1.0F);
        Vector3 spawnForceDirection = new Vector3(x, y, 0.0F);
        body = GetComponent<Rigidbody2D>();
        body.AddForce(spawnForceDirection.normalized * spawnForce * Random.Range(0.5F, 1.0F));
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > 0.4F)
        {
            // Increase speed
            speed += acceleration;

            Vector2 direction = player.transform.position - this.transform.position;

            body.velocity = direction.normalized * speed;
        }
    }
}
