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
    private float speed = 5.0f;
    private float xAxis = 0.0f;
    private float yAxis = 0.0f;
    private Vector3 forceVector = new Vector3(0.0f, 0.0f, 0.0f);
    private float rAngle = 0.0f;
    // Score
    private int score = 0;
    private int largeAsteroidVal = 25;
    private int smallAsteroidVal = 10;
    private float chargeMeter = 0.0f;

    private void FixedUpdate()
    {
        Movement();

        CheckRotation();

        // Charge
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Charge();
        }
        else
        {
            IncreaseChargeMeter();
        }
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

        this.GetComponent<Rigidbody2D>().AddForce(forceVector.normalized * speed);
    }

    private void CheckRotation()
    {
        // If movement has changed
        if (xAxis != 0 || yAxis != 0)
        {
            // Make it rotate
        }
    }

    private void IncreaseChargeMeter()
    {
        // Truncate
        if (chargeMeter > 10.0f)
        {
            chargeMeter = 10.0f;
        }
        else
        {
            // Increment
            chargeMeter += Time.fixedDeltaTime;
        }
    }

    private void Charge()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collided with base
        if (collision.collider.tag == "Base")
        {
            // On same team
            if (collision.collider.GetComponent<BaseScript>().team == team)
            {

            }
        }
    }
}
