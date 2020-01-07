using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{
    public float distance;
    public Vector3 target = new Vector3(1, 0, 0);
    float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, target));
        if (Vector3.Distance(transform.position, target)>=0.001f)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            Vector2 oldPos = new Vector2(transform.position.x,transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            Vector2 newPos = new Vector2(transform.position.x, transform.position.z);
            distance += Vector3.Distance(oldPos, newPos);
        }
    }
}
