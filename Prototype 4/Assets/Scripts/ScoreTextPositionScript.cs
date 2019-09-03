using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextPositionScript : MonoBehaviour
{
    Quaternion rot;

    public float offset;

    //Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rot;
        transform.position = new Vector3(this.transform.parent.transform.localPosition.x, this.transform.parent.transform.localPosition.y + offset, this.transform.parent.transform.localPosition.z);
    }
}
