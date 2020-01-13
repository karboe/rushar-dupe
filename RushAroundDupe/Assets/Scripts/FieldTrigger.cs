using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldTrigger : MonoBehaviour
{

    public Color orginalColor,transitionColor;
    bool inside = false;
    float duration = 0.5f; // This will be your time in seconds.
    float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        orginalColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInside(GetComponent<Collider>(), Camera.main.transform.position) && CreateFields.currentNumber == int.Parse(transform.GetComponentInChildren<TextMeshPro>().text))
        {
            CreateFields.callNumber = true;
            Timer.addPoint = true;
            GetComponent<Animation>().Play();
            StartCoroutine(LerpColor());
        }

    }



    void OnMouseDown(
   )
    {

        if (CreateFields.currentNumber == int.Parse(transform.GetComponentInChildren<TextMeshPro>().text))
        {
            CreateFields.callNumber = true;
            Timer.addPoint = true;
            GetComponent<Animation>().Play();
            StartCoroutine(LerpColor());

        }

    }
    bool IsInside(Collider c, Vector3 point)
    {

        // Because closest=point if point is inside - not clear from docs I feel
        Vector2 colliderPos = new Vector2(c.transform.position.x, c.transform.position.z);
        Vector2 cameraPos = new Vector2(point.x, point.z);
        if (Vector2.Distance(colliderPos, cameraPos) <= 0.2f)
        {
            return true;
        }
        return false;
    }

   
    IEnumerator LerpColor()
    {
        float progress = 0; 
        float increment = smoothness / duration; 
        while (progress < 1)
        {
            currentColor = Color.Lerp(GetComponent<MeshRenderer>().material.color, transitionColor, progress);
            progress += increment;
            GetComponent<MeshRenderer>().material.color = currentColor;
            yield return new WaitForSeconds(smoothness);
        }
        progress = 0;
        while (progress<1)
        {
            currentColor = Color.Lerp(GetComponent<MeshRenderer>().material.color,orginalColor, progress);
            progress += increment;
            GetComponent<MeshRenderer>().material.color = currentColor;
            yield return new WaitForSeconds(smoothness);
        }
        progress = 0;

    }
   

}