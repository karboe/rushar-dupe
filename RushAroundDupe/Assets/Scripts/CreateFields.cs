using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateFields : MonoBehaviour
{
    Vector3 middle;
    public GameObject field,currentTarget;
    GameObject middleF;
    public AudioSource audioSource;
    public AudioClip[] numbers;

    TextMeshPro number;
    public static int currentNumber = 0;
    public static bool callNumber = false;

    // Start is called before the first frame update
    void Start()
    {
        middle = this.transform.position;
        InstantiateFields(12);
        StartCoroutine(WaitFor(3));


        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // CallNumber();
        }
        if (callNumber)
        {
            callNumber = false;
            CallNumber();
        }
    }

    void InstantiateFields(int numberOfFields)
    {
        float degree = 360 / numberOfFields;
        for(int i = 0; i < numberOfFields; i++)
        {
            GameObject currentField =Instantiate(field, new Vector3(0, 0.001f, 1.3f),Quaternion.identity);

           
            currentField.transform.RotateAround(middle, Vector3.up, degree+degree * i);
            currentField.transform.parent = transform;

            TextMeshPro n = currentField.GetComponentInChildren<TextMeshPro>();
            n.text = (i + 1).ToString();

        }

         middleF = Instantiate(currentTarget);
        middleF.transform.parent = transform;
        number = middleF.GetComponentInChildren<TextMeshPro>();
        middleF.SetActive(false);


    }

    IEnumerator VoiceNumbers()
    {
        int i = 0;
        while (i < 12)
        {
            audioSource.clip = numbers[i];
            audioSource.Play();
            i++;
            yield return new WaitForSeconds(3.0f);
        }

        yield return null;
    }

    int RandomNumber(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
    static public void FieldTriggered()
    {

    }

    IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        middleF.SetActive(true);
        CallNumber();
    }
    void CallNumber()
    {
        int newNumber=RandomNumber(1, 12);

        if (currentNumber == newNumber)
        {

             CallNumber();


        }
        else
        {
            currentNumber = newNumber;
            number.text = currentNumber.ToString();
            audioSource.clip = numbers[currentNumber - 1];
            audioSource.Play();
        }
       
        
    }
}
