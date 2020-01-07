using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldTrigger : MonoBehaviour
{
    bool inside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInside(GetComponent<Collider>(),Camera.main.transform.position)&& CreateFields.currentNumber == int.Parse(transform.GetComponentInChildren<TextMeshPro>().text))
        {
            CreateFields.callNumber = true;
            Timer.addPoint = true;
        }
    }

 

    void OnMouseOver(
   )
    {

        /*if (Input.GetMouseButtonDown(0)&& CreateFields.currentNumber==int.Parse(transform.GetComponentInChildren<TextMeshPro>().text))
         {

            // Debug.Log("Here");
             CreateFields.callNumber = true;
             Timer.addPoint = true;
         }*/
       
    }
     bool IsInside(Collider c, Vector3 point)
    {

        // Because closest=point if point is inside - not clear from docs I feel
        Vector2 colliderPos = new Vector2(c.transform.position.x, c.transform.position.z);
        Vector2 cameraPos = new Vector2(point.x, point.z);
        if (Vector2.Distance(colliderPos, cameraPos) <= 0.1f)
        {
            return true;
        }
        return false;
    }


}
