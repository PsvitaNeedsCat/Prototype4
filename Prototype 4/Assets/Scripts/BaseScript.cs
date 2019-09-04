using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    // Public
    public PlayerScript.TeamColour team;
    public int totalScore = 0;
    public ParticleSystem bankedScore;

    public TextMesh scoreText;

    private void Awake()
    {
        scoreText = this.gameObject.GetComponentInChildren<TextMesh>();
    }

    private void FixedUpdate()
    {
        string myScore = totalScore.ToString();
        scoreText.text = myScore;
    }

    public void PlayParticles()
    {
        bankedScore.Play();
    }

}
