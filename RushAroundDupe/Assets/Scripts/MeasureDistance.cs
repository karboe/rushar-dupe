using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{
    public float distance;
    public static int distanceInMetre;
    public Vector3 target = new Vector3(1, 0, 0);

    public Vector3 currentPos, newPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, target));

        newPosition = transform.position;
        Vector2 oldPos = new Vector2(currentPos.x, currentPos.z);
        Vector2 newPos = new Vector2(newPosition.x, newPosition.z);
        currentPos = newPosition;
        distance += Vector3.Distance(oldPos, newPos);
        distanceInMetre = (int)distance;

    }
}
