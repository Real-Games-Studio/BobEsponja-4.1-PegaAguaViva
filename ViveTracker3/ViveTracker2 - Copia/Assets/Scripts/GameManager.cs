using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Volume postProcessing;
    float transitionValue = 0f;
    public AudioSource audio;

    private Coroutine spawnCoroutine;
    private Camera mainCamera;

    public Collider2D[] colliders;

    public GameObject TimeUpScreen;
    public GameObject CongratulationsScreen;
    public TextMeshProUGUI finalScore;

    public Image timer;

    private float minSpawnTime;
    private float maxSpawnTime;

    void Awake()
    {
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
        float[] spawnWeights = { 0.8f, 0.1f, 0.1f };

        while (true)
        {
            float waitTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject objectToSpawn = GetRandomObjectByWeight(spawnWeights);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(0, 20f, UnityEngine.Random.Range(0f, -20f)));
        }
    }

    GameObject GetRandomObjectByWeight(float[] weights)
    {
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
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

        return objectsToSpawn[objectsToSpawn.Length - 1];
    }

    Vector3 GetRandomSpawnPosition()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spawnY = mainCamera.transform.position.y + (cameraHeight / 2);

        float spawnX = UnityEngine.Random.Range(mainCamera.transform.position.x - (cameraWidth / 2), mainCamera.transform.position.x + (cameraWidth / 2));

        return new Vector3(spawnX, spawnY, 0);
    }

    public IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            timer.fillAmount = 1 - (float) currentTime / (float) startTime; 
            UpdateTimeText();
        }
        spawnObjects = false;

        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        
        TimeUpScreen.SetActive(true);
        audio.Play();
        DOTween.To(() => transitionValue, x => transitionValue = x, 1f, 0.2f)
        .OnUpdate(() => 
        {
            postProcessing.weight = transitionValue;
        });
        yield return new WaitForSeconds(5);
        TimeUpScreen.SetActive(false);
        CongratulationsScreen.SetActive(true);
        finalScore.text = score.ToString() + " PONTOS";
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
