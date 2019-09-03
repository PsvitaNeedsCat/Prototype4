using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour
{
    //Central Text Variables
    public Text centText;
    public int centState = 0;
    private Animator centAnim;

    //TEST VARIABLES, FEEL FREE TO DELETE AND HOOK UP THE ACTUAL VARIABLES
    public float testTimer = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        centText.text = "";
        centAnim = centText.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CentralText();
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
