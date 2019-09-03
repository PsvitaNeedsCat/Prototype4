using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextPositionScript : MonoBehaviour
{
    Quaternion rot;
    //Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rot = transform.rotation;
        //pos = new Vector2(this.transform.localPosition.x, this.transform.parent.transform.localPosition.y + 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rot;
        transform.position = new Vector3(this.transform.parent.transform.localPosition.x, this.transform.parent.transform.localPosition.y + 0.4f, this.transform.parent.transform.localPosition.z);
    }
}
