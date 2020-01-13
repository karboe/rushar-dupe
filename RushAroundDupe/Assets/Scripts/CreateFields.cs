using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateFields : MonoBehaviour
{
    Vector3 middle = new Vector3(0, -1, 0);
    public GameObject field, currentTarget,game,canvas,endscreen;
    GameObject middleF, newGame;
    public AudioSource audioSource;
    public AudioClip[] numbers;

    TextMeshPro number;
    public static int currentNumber = 0;
    public static bool callNumber = false;
    int currentNumberOfFields = 0;
    bool isRestarted = false;
    bool isInstantiated = false;

    // Start is called before the first frame update
    void Start()
    {
        newGame = game;
        if (!isInstantiated)
        {
            currentTarget.SetActive(false);
            InstantiateFields(12);
            isInstantiated = true;
        }

       


    }

    private void Awake()
    {
       
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        if (callNumber)
        {
            callNumber = false;

            StartCoroutine(NewNumber());
        }
    }

    void InstantiateFields(int numberOfFields)
    {
       
        float degree = 360 / numberOfFields;
        for (int i = 0; i < numberOfFields; i++)
        {
            GameObject currentField = Instantiate(field, new Vector3(0, transform.position.y, 1.3f), Quaternion.identity);


            currentField.transform.RotateAround(middle, Vector3.up, degree + degree * i);
            currentField.transform.parent = transform;

            TextMeshPro n = currentField.GetComponentInChildren<TextMeshPro>();
            n.text = (i + 1).ToString();
            n.gameObject.transform.RotateAround(n.gameObject.transform.position, Vector3.up, -degree - degree * i);
            currentNumberOfFields++;

        }
      
        
        middleF = Instantiate(currentTarget, new Vector3(0, transform.position.y, 0), Quaternion.identity);
        middleF.transform.parent = transform;

        number = middleF.GetComponentInChildren<TextMeshPro>();
        middleF.SetActive(false);
        StartCoroutine(WaitFor(3));

    }

    IEnumerator NewNumber()
    {
        int newNumber = RandomNumber(1, 12);

        if (currentNumber == newNumber)
        {
            Debug.Log("Number");
            StartCoroutine(NewNumber());
            yield return null;



        }
        else
        {
            currentNumber = newNumber;
            number.text = currentNumber.ToString();
            yield return new WaitForSeconds(0.5f);
            audioSource.clip = numbers[currentNumber - 1];
            audioSource.Play();
        }



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
        StartCoroutine(NewNumber());
    }



}
