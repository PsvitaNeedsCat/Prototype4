using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Public
    public enum AsteroidSize
    {
        Small,
        Large
    }
    public AsteroidSize size;

    private void Awake()
    {
        if (size == AsteroidSize.Large)
        {
            // Set scale
            this.transform.localScale *= 2;

            // Set mass
            this.GetComponent<Rigidbody2D>().mass *= 2;
        }
    }
}
