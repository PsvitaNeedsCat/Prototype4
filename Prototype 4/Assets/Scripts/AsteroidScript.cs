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
    public AsteroidSize size = AsteroidSize.Small;
    public GameObject killer;
    public GameObject scoreTransferBit;
    // Sprites
    public Sprite smallSprite;
    public Sprite largeSprite;

    public void MakeLarge()
    {
        // Set scale
        this.transform.localScale *= 2;

        // Set mass
        this.GetComponent<Rigidbody2D>().mass *= 2;

        this.GetComponent<SpriteRenderer>().sprite = largeSprite;

        size = AsteroidSize.Large;
    }

    private void OnDestroy()
    {
        int numberBits;
        if (size == AsteroidSize.Small)
        {
            numberBits = 3;
        }
        else
        {
            numberBits = 10;
        }

        for (int i = 0; i < numberBits; i++)
        {
            GameObject scoreBit = Instantiate(scoreTransferBit, this.transform.position, Quaternion.identity);
            scoreBit.GetComponent<ScoreBitScript>().player = killer;
        }
    }
}
