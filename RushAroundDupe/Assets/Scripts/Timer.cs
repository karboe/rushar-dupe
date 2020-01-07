using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentValue;
    string countdownText="60";
    public static int currentPoints = 0;

    public TextMeshProUGUI startCountdown, counter;
    public GameObject endScreen;
    public static bool addPoint = false;

    // Start is called before the first frame update
    void Start()
    {
       transform.GetComponent<TextMeshProUGUI>().text=countdownText;
        counter.text = currentPoints.ToString();
        GetComponent<TextMeshProUGUI>().enabled = false;
        StartCoroutine(StartCountdown(3,startCountdown));
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButtonDown(0))
         {
             currentPoints++;
             counter.text = currentPoints.ToString();
         }
         if (Input.GetMouseButtonDown(1))
         {
             currentPoints--;
             counter.text = currentPoints.ToString();
         }*/
        if (addPoint)
        {
            addPoint = false;
            currentPoints++;
            counter.text = currentPoints.ToString();
        }

    }
    IEnumerator StartCountdown(float countdownValue = 60)
    {

        currentValue = countdownValue;
        while (currentValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentValue--;
            transform.GetComponent<TextMeshProUGUI>().text = currentValue.ToString();
        }
        endScreen.gameObject.SetActive(true);
        endScreen.GetComponentInChildren<TextMeshProUGUI>().text = currentPoints.ToString();
        yield return null;
    }

    IEnumerator StartCountdown(float countdownValue, TextMeshProUGUI countdown)
    {

        currentValue = countdownValue;
        while (currentValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentValue--;
            countdown.text = currentValue.ToString();
        }
        countdown.transform.gameObject.SetActive(false);
        gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        StartCoroutine(StartCountdown());
    }




}
