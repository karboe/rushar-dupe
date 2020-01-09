using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
   
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        NewGame();
         
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NewGame()
    {
        Instantiate(game);
    }
}
