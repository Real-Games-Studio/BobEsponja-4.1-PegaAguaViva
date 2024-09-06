using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int startTime = 10;
    private int currentTime;
    private int score;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

    public GameObject[] objectsToSpawn;
    public bool spawnObjects = false;

    private Coroutine spawnCoroutine;
    private Camera mainCamera;

    public Collider2D[] colliders;

    public GameObject TimeUpScreen;
    public GameObject CongratulationsScreen;

    private float minSpawnTime;
    private float maxSpawnTime;

    void Awake()
    {
        // Implementação do padrão Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        minSpawnTime = JSONFile.Configclass.tempoDeSpawnMinimo;
        maxSpawnTime = JSONFile.Configclass.tempoDeSpawnMaximo;

        mainCamera = Camera.main;
        score = 0;
        
        startTime = JSONFile.Configclass.tempoDeJogo;
        currentTime = startTime;

        UpdateTimeText();
        UpdateScoreText();
    }

    void Update()
    {
        if (spawnObjects && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnObjects());
        }
        else if (!spawnObjects && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
    IEnumerator SpawnObjects()
    {
        // Define os pesos para cada objeto
        float[] spawnWeights = { 0.8f, 0.1f, 0.1f };

        while (true)
        {
            // Espera um intervalo aleatório entre minSpawnTime e maxSpawnTime
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // Gera um objeto aleatório no topo da tela, fora da visão da câmera
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject objectToSpawn = GetRandomObjectByWeight(spawnWeights);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    GameObject GetRandomObjectByWeight(float[] weights)
    {
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                Debug.Log(randomValue);
                return objectsToSpawn[i];
            }
        }

        // Retorna o último objeto como fallback (não deve acontecer se os pesos estiverem corretos)
        return objectsToSpawn[objectsToSpawn.Length - 1];
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Obtém os limites da câmera
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calcula a posição Y logo acima da câmera
        float spawnY = mainCamera.transform.position.y + (cameraHeight / 2);

        // Calcula uma posição X aleatória dentro dos limites da câmera
        float spawnX = Random.Range(mainCamera.transform.position.x - (cameraWidth / 2), mainCamera.transform.position.x + (cameraWidth / 2));

        return new Vector3(spawnX, spawnY, 0);
    }

    public IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            UpdateTimeText();
            //Debug.Log("Tempo restante: " + currentTime);
        }
        //Debug.Log("Tempo esgotado!");
        spawnObjects = false;

        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        
        TimeUpScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        TimeUpScreen.SetActive(false);
        CongratulationsScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    public int GetCurrentTime()
    {
        return currentTime;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = currentTime.ToString();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
