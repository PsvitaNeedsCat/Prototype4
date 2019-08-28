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
    private bool boostInput = false;
    private Vector3 forceVector = new Vector3(0.0f, 0.0f, 0.0f);
    private float rAngle = 0.0f;

    private void FixedUpdate()
    {
        Movement();

        CheckRotation();


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
            transform.rotation = Quaternion.Euler(0.0F, 0.0F, 0.0F);
            float dotProduct = (Vector3.up.x * forceVector.x + Vector3.up.y * forceVector.y);
            float cosTheta = (dotProduct) / (forceVector.magnitude);
            // Make it rotate
            transform.RotateAround(transform.position, Vector3.forward, Mathf.Rad2Deg * -Mathf.Acos(cosTheta) * Mathf.Sign(forceVector.x));
        }
    }
}