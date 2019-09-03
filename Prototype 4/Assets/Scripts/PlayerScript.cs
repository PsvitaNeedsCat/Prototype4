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
    public int score = 0;
    Vector2 previousVelocity = new Vector2(0.0f, 0.0f);
    public float chargeMeter = 2.0F;
    public float chargeFull = 2.0F;
    public GameObject scoreTransferBit;
    public GameObject killer;

    public GameObject scoreText;

    // Private
    private float speed = 300.0f;
    private float xAxis = 0.0f;
    private float yAxis = 0.0f;
    private bool boostInput = false;
    private Vector3 forceVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody2D playerBody;
    private float breakSpeed = 2.0F;
    // Score
    private int largeAsteroidVal = 25;
    private int smallAsteroidVal = 10;

    private void Awake()
    {
        playerBody = this.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        // Update velocity
        previousVelocity = playerBody.velocity;

        //Update score
        string myScore = score.ToString();
        scoreText.GetComponent<TextMesh>().text = myScore;

        Movement();

        CheckRotation();

        if (ChargePressed() && chargeMeter >= chargeFull) { Charge(); }

        IncreaseChargeMeter();

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (playerNo == 1)
        {
            GameObject.Find("Player1ScoreDisplay").GetComponent<TextMesh>().text = "Player 1 Score: " + score;
        }
        else if (playerNo == 2)
        {
            GameObject.Find("Player2ScoreDisplay").GetComponent<TextMesh>().text = "Player 2 Score: " + score;
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

    private bool ChargePressed()
    {
        // Check player number
        if (playerNo == 1)
        {
            // Check if button is pressed
            if (Input.GetAxisRaw("Boost") == 1.0f)
            {
                return true;
            }
        }
        else if (playerNo == 2)
        {
            // Check if button is pressed
            if (Input.GetAxisRaw("Boost2") == 1.0f)
            {
                return true;
            }
        }

        return false;
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

    public void Kill()
    {
        if (score != 0)
        {
            uint numberBits = (uint)(score / 10);

            for (uint i = 0; i < numberBits; i++)
            {
                GameObject scoreBit = Instantiate(scoreTransferBit, this.transform.position, Quaternion.identity);
                scoreBit.GetComponent<ScoreBitScript>().player = killer;
            }

            score = 0;
        }

        // Reset velocity
        playerBody.velocity = new Vector2(0.0f, 0.0f);

        // Respawn //

        // Get all bases
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");

        // Check which one has the correct colour
        for (uint i = 0; i < bases.Length; i++)
        {
            // Check if correct base
            if (bases[i].GetComponent<BaseScript>().team == team)
            {
                // Respawn at that base
                this.transform.position = bases[i].transform.position;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collided with scorebit
        if (collision.tag == "ScoreBit")
        {
            score += collision.gameObject.GetComponent<ScoreBitScript>().score;
            // Destroy the scoreBit
            Destroy(collision.gameObject);
        }
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
                if (previousVelocity.magnitude >= breakSpeed)
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
            if (previousVelocity.magnitude >= breakSpeed)
            {
                AsteroidScript script = collision.gameObject.GetComponent<AsteroidScript>();
                script.killer = this.gameObject;
                // Destroy the asteroid.
                Destroy(collision.gameObject);
            }
        }

        // Collided with another player
        if (collision.collider.tag == "Player")
        {
            // Save enemy script
            PlayerScript enemyScript = collision.collider.GetComponent<PlayerScript>();

            // Save velocity
            Vector2 enemyVelocity = enemyScript.previousVelocity;

            // Check teams
            if (enemyScript.team != team)
            {
                if (previousVelocity.magnitude >= breakSpeed && enemyVelocity.magnitude >= breakSpeed)
                {
                    if (previousVelocity.magnitude > enemyVelocity.magnitude)
                    {
                        // This player wins
                        enemyScript.killer = this.gameObject;
                        enemyScript.Kill();
                    }
                    else
                    {
                        killer = collision.collider.gameObject;
                        this.Kill();
                    }
                }

                else if (previousVelocity.magnitude >= breakSpeed)
                {
                    enemyScript.killer = this.gameObject;
                    enemyScript.Kill();
                }

                else if (enemyVelocity.magnitude >= breakSpeed)
                {
                    killer = collision.collider.gameObject;
                    this.Kill();
                }
            }
        }
    }
}