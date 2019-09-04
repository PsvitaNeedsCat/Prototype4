using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Load game
            SceneManager.LoadScene("Art Scene 1");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
