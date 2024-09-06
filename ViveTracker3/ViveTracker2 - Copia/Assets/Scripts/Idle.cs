using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Idle : MonoBehaviour
{
    [SerializeField] GameObject instructionsScreen;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (!instructionsScreen.activeSelf && gameObject.activeSelf)
        {
            instructionsScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ResetGame()
    {
        if (!gameObject.activeSelf)
        {
            SceneManager.LoadScene(0);
        }
    }
}
