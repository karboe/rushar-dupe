using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    Camera mycam;
    float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
           mycam = GetComponent<Camera>();
         
    }

    // Update is called once per frame
    void Update()
    {
    }
}
