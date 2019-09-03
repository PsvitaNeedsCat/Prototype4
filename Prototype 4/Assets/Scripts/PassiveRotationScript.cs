using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRotationScript : MonoBehaviour
{
    public float speed;

    public bool amStation;

    // Start is called before the first frame update
    void Start()
    {
        if (!amStation)
        {
            speed = Random.Range(-7.0f, 7.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
    }
}
