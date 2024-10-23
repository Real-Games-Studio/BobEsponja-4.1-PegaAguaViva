using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject tres;
    [SerializeField] GameObject dois;
    [SerializeField] GameObject um;
    [SerializeField] GameObject valendo;
    [SerializeField] GameObject tempo;
    [SerializeField] GameObject pontos;
    [SerializeField] AudioSource countdownSound;

    void OnEnable()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        GameManager.Instance.startTime = JSONFile.Configclass.tempoDeJogo;
        yield return new WaitForSeconds(0.7f);
        countdownSound.Play();
        tres.SetActive(true);
        yield return new WaitForSeconds(1);
        tres.SetActive(false);
        dois.SetActive(true);
        yield return new WaitForSeconds(1);
        dois.SetActive(false);
        um.SetActive(true);
        yield return new WaitForSeconds(1);
        um.SetActive(false);
        valendo.SetActive(true);
        yield return new WaitForSeconds(1);
        valendo.SetActive(false);
        tempo.SetActive(true);
        pontos.SetActive(true);
        GameManager.Instance.spawnObjects = true;
        StartCoroutine(GameManager.Instance.Countdown());
    }

    public void EndgameCanvas()
    {
        tempo.SetActive(false);
        pontos.SetActive(false);
    }
}
