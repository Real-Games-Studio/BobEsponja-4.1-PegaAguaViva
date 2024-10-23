using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Idle : MonoBehaviour
{
    [SerializeField] GameObject instructionsScreen;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] Animator tutorialSprite;
    [SerializeField] GameObject particle;
    [SerializeField] bool disablePass = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (gameObject.activeSelf && !disablePass)
        StartCoroutine(StartGameTransition());
    }

    public IEnumerator StartGameTransition()
    {
        disablePass = true;
        particle.SetActive(true);
        yield return new WaitForSeconds(1f);
        tutorialScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        particle.SetActive(false);
        
        // Tutorial
        tutorialSprite.Play("Tutorial");
        yield return new WaitForSeconds(7.2f);

        particle.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Object To Start
        tutorialScreen.SetActive(false);
        if (!instructionsScreen.activeSelf && gameObject.activeSelf)
        {
            instructionsScreen.GetComponent<Collider2D>().enabled = false;
            instructionsScreen.SetActive(true);

            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
        
        yield return new WaitForSeconds(1f);
        particle.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        
        instructionsScreen.GetComponent<Collider2D>().enabled = true;
        particle.SetActive(false);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        
    }

    public void ResetGame()
    {
        if (!gameObject.activeSelf)
        {
            SceneManager.LoadScene(0);
        }
    }
}
