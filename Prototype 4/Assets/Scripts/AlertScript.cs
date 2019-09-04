using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertScript : MonoBehaviour
{
    public Image scroll;
    public Image alertBG;
    public Text alert;

    public GameObject fullalert;

    public bool active;

    public int baseAtt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
                break;

            case 1:
                scroll.color = new Color(0.25f, 0.75f, 1.0f, 1.0f);
                alertBG.color = new Color(0.25f, 0.75f, 1.0f, 1.0f);
                alert.text = "BLUE BASE UNDER ATTACK";
                break;

            case 2:
                scroll.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                alertBG.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                alert.text = "YELLOW BASE UNDER ATTACK";
                break;
        }
    }
}
