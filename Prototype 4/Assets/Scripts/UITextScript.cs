using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UITextScript : MonoBehaviour
{
    //Central Text Variables
    public Text centText;
    public int centState = 0;
    private Animator centAnim;

    public GameObject blueBase;
    public GameObject redBase;
    public GameObject yellowBase;

    //TEST VARIABLES, FEEL FREE TO DELETE AND HOOK UP THE ACTUAL VARIABLES
    public float testTimer = 50.0f;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        centText.text = "";
        centAnim = centText.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (testTimer <= 0.0f && !gameOver)
        {
            // Get scores
            int blueScore = blueBase.GetComponent<BaseScript>().totalScore;
            int redScore = redBase.GetComponent<BaseScript>().totalScore;
            int yellowScore = yellowBase.GetComponent<BaseScript>().totalScore;

            // Blue won
            if (Mathf.Max(blueScore, redScore, yellowScore) == blueScore)
            {
                centState = 4;
            }
            // Red won
            else if (Mathf.Max(blueScore, redScore, yellowScore) == redScore)
            {
                centState = 3;
            }
            // Yellow won
            else
            {
                centState = 5;
            }
        }

        CentralText();

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            centState = 0;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            centState = 1;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            centState = 2;
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            centState = 3;
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            centState = 4;
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            centState = 5;
        }
    }

    void CentralText()
    {


        switch (centState)
        {
            case 0:
                centText.text = "";
                centAnim.SetBool("Bounce", false);
                break;

            case 1:
                centText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                centText.text = "START MATCH?";
                centAnim.SetBool("Bounce", true);
                break;

            case 2:
                //test variables
                testTimer -= Time.deltaTime;

                //ACTUAL CODE don't delete this part
                centText.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
                string timer = testTimer.ToString("F0");        //F0 ensure no decimals are shown
                centText.text = timer;
                centAnim.SetBool("Bounce", false);
                break;

            case 3:
                centText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                centText.text = "RED TEAM WINS";
                centAnim.SetBool("Bounce", true);
                break;

            case 4:
                centText.color = new Color(0.25f, 0.75f, 1.0f, 1.0f);
                centText.text = "BLUE TEAM WINS";
                centAnim.SetBool("Bounce", true);
                break;

            case 5:
                centText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                centText.text = "YELLOW TEAM WINS";
                centAnim.SetBool("Bounce", true);
                break;
        }
    }
}
