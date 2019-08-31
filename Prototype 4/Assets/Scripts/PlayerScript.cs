using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Public
    public int playerNo;
    public enum TeamColour
    {
        RED,
        BLUE,
        YELLOW
    }
    public TeamColour team;

    // Private
    private float speed = 300.0f;
    private float xAxis = 0.0f;
    private float yAxis = 0.0f;
    private bool boostInput = false;
    private Vector3 forceVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody2D playerBody;
    private float breakSpeed = 1.0F;
    // Score
    private int score = 0;
    private int largeAsteroidVal = 25;
    private int smallAsteroidVal = 10;
    public float chargeMeter = 2.0F;
    public float chargeFull = 2.0F;

    private void Awake()
    {
        playerBody = this.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Movement();

        CheckRotation();

        if (1.0F == Input.GetAxisRaw("Boost") && chargeMeter >= chargeFull) { Charge(); }

        IncreaseChargeMeter();
    }

    private void Movement()
    {
        // Update X and Y speed
        if (playerNo == 1)
        {
            xAxis = Input.GetAxisRaw("Horizontal1");
            yAxis = Input.GetAxisRaw("Vertical1");
        }
        else if (playerNo == 2)
        {
            xAxis = Input.GetAxisRaw("Horizontal2");
            yAxis = Input.GetAxisRaw("Vertical2");
        }

        forceVector = new Vector3(xAxis, yAxis, 0.0f);

        playerBody.AddForce(forceVector.normalized * Time.fixedDeltaTime * speed);
    }

    private void CheckRotation()
    {
        // If movement has changed
        if (xAxis != 0 || yAxis != 0)
        {
            Vector3 velocityDirection = playerBody.velocity.normalized;
            Vector3 lookDirection = forceVector.normalized;
            if (velocityDirection.magnitude > 0)
            {
                lookDirection = velocityDirection;
            }

            // make the object face up
            transform.rotation = Quaternion.Euler(0.0F, 0.0F, 0.0F);
            
            // calculate the difference between up and the direction we want to look in
            float dotProduct = (Vector3.up.x * lookDirection.x + Vector3.up.y * lookDirection.y);
            float cosTheta = (dotProduct) / (lookDirection.magnitude);
            // Make it rotate
            float degTheta = Mathf.Rad2Deg * Mathf.Acos(cosTheta) * -Mathf.Sign(lookDirection.x);
            // rotate the player by degTheta around their position, along the axis defined by the forward vector.
            transform.RotateAround(transform.position, Vector3.forward, degTheta);
        }
    }

    private void IncreaseChargeMeter()
    {
        // Truncate
        if (chargeMeter > chargeFull)
        {
            chargeMeter = chargeFull;
        }
        else if (chargeMeter < chargeFull)
        {
            // Increment
            chargeMeter += Time.fixedDeltaTime;
        }
    }

    private void Charge()
    {
        chargeMeter = 0.0F;
        // do the charge
        playerBody.AddForce(this.transform.up.normalized * Time.fixedDeltaTime * speed * 150.0F);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collided with base
        if (collision.collider.tag == "Base")
        {
            BaseScript collidedScript = collision.collider.GetComponent<BaseScript>();

            // On same team
            if (collidedScript.team == team)
            {
                // If player has some score to bank
                if (score != 0)
                {
                    // Bank score
                    collidedScript.totalScore += score;

                    // Empty score
                    score = 0;
                }
            }
            // Enemy base
            else
            {
                // Check if fast enough
                if (playerBody.velocity.magnitude >= breakSpeed)
                {
                    // Take 10% of enemy points
                    int stolenScore = (int)Mathf.Ceil(collidedScript.totalScore * 0.10f);

                    // Remove score from base
                    collidedScript.totalScore -= stolenScore;

                    // Add score to player's bank
                    score += stolenScore;
                }
            }
        }

        // Collided with asteroid
        if (collision.collider.tag == "Asteroid")
        {
            // On same team
            if (playerBody.velocity.magnitude >= breakSpeed)
            {
                AsteroidScript script = collision.gameObject.GetComponent<AsteroidScript>();
                // Destroy the asteroid.
                if (script.size == AsteroidScript.AsteroidSize.Small)
                {
                    score += 25;
                }
                else
                {
                    score += 100;
                }
                Destroy(collision.gameObject);
            }
        }
    }
}