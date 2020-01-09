using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentValue;
    string countdownText = "60";
    public static int currentPoints = 0;

    public TextMeshProUGUI startCountdown, counter,metre;
    public GameObject endScreen, game;
    GameObject newGame;
    public static bool addPoint = false;
    bool isInstantiated = false;
    bool isRestarted = false;

    // Start is called before the first frame update
    void Start()
    {

        if (!isInstantiated)
        {
            currentPoints = 0;
            newGame = game;
            endScreen.SetActive(false);
            Debug.Log("Timer");
            transform.GetComponent<TextMeshProUGUI>().text = countdownText;
            counter.text = currentPoints.ToString();
            GetComponent<TextMeshProUGUI>().enabled = false;
            StartCoroutine(StartCountdown(3, startCountdown));
           
        }
    }
    private void Awake()
    {


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
        metre.text = MeasureDistance.distanceInMetre.ToString();
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
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator Game()
    {
        GameObject n = Instantiate(newGame);

        yield return null;
        Destroy(game);

    }



}




