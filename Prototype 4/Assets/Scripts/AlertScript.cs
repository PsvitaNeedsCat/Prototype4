using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertScript : MonoBehaviour
{
    public Image scroll;
    public Image alertBG;
    public Text alert;
    private Animator alertAnim;

    public GameObject fullalert;

    public bool active;

    public int baseAtt = 0;

    private bool countdown = false;
    private float cdMax = 2.5f;
    private float cdCur;

    // Start is called before the first frame update
    void Awake()
    {
        cdCur = cdMax;
        alertAnim = fullalert.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            fullalert.SetActive(true);
            /*  scroll.gameObject.SetActive(true);
              alertBG.gameObject.SetActive(true);
              alert.gameObject.SetActive(true);*/
        }
        else
        {

            fullalert.SetActive(false);
            /*scroll.gameObject.SetActive(false);
            alertBG.gameObject.SetActive(false);
            alert.gameObject.SetActive(false);*/
        }

        switch (baseAtt)
        {
            case 0:
                scroll.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                alertBG.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                alert.text = "RED BASE UNDER ATTACK";
                countdown = true;
                break;

            case 1:
                scroll.color = new Color(0.25f, 0.75f, 1.0f, 1.0f);
                alertBG.color = new Color(0.25f, 0.75f, 1.0f, 1.0f);
                alert.text = "BLUE BASE UNDER ATTACK";
                countdown = true;
                break;

            case 2:
                scroll.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                alertBG.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                alert.text = "YELLOW BASE UNDER ATTACK";
                countdown = true;
                break;
        }

        if (countdown)
        {
            if (cdCur > 0)
            {
                cdCur -= Time.deltaTime;
            }
            else
            {
                alertAnim.SetBool("Close", true);
                cdCur = cdMax;
            }
        }
        else
        {
            if (cdCur != cdMax)
            {
                alertAnim.SetBool("Close", false);
                cdCur = cdMax;
            }
        }

        if (alertAnim.GetCurrentAnimatorStateInfo(0).IsName("alertInvisible"))
        {
            active = false;
        }
    }
}
