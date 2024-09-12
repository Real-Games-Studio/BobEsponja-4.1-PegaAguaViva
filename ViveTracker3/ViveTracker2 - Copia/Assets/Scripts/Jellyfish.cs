using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Jellyfish : MonoBehaviour
{
    Collider2D collider;
    Renderer renderer;
    [SerializeField] string Name;
    [SerializeField] bool Collected;
    [SerializeField] Transform Bolha;
    public int score;
    public GameObject catchParticle;
    public List<AudioSource> feedbackAudio;

    float speed;
    bool speedDown;

    public float fallSpeed = 2f;
    public float lateralSpeed = 0.5f;
    private float currentLateralSpeed;
    private Camera mainCamera;
    private float lowerBound;
    private Vector3 currentVelocity;

    private GameObject spawnedParticle;
    private Tween currentTween;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();

        StartCoroutine(ChangeLateralDirection());
        mainCamera = Camera.main;
        lowerBound = mainCamera.transform.position.y - mainCamera.orthographicSize;

        Bolha = GameObject.Find("Bolha").transform;

        if(Name == "Rosa")
        {
            fallSpeed = JSONFile.Configclass.velocidadeQuedaRosa;
            lateralSpeed = JSONFile.Configclass.velocidadeLateralRosa;

            float rng = Random.Range(JSONFile.Configclass.tamanhoMinRosa, JSONFile.Configclass.tamanhoMaxRosa);
            transform.localScale = new Vector3(rng, rng, 0.5f);
        }
        else if (Name == "Azul")
        {
            fallSpeed = JSONFile.Configclass.velocidadeQuedaAzul;
            lateralSpeed = JSONFile.Configclass.velocidadeLateralAzul;

            float rng = Random.Range(JSONFile.Configclass.tamanhoMinAzul, JSONFile.Configclass.tamanhoMaxAzul);
            transform.localScale = new Vector3(rng, rng, 0.5f);
        }
        else if(Name == "Verde")
        {
            fallSpeed = JSONFile.Configclass.velocidadeQuedaVerde;
            lateralSpeed = JSONFile.Configclass.velocidadeLateralVerde;

            float rng = Random.Range(JSONFile.Configclass.tamanhoMinVerde, JSONFile.Configclass.tamanhoMaxVerde);
            transform.localScale = new Vector3(rng, rng, 0.5f);
        }

        Material instance = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = instance;
    }

    private void Update()
    {
        if (Collected)
        {
            if (score > 0)
            {
                if (currentTween == null)
                currentTween = transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutQuad).OnStart(() =>
                {
                    if (spawnedParticle == null)
                    {
                        feedbackAudio[Random.Range(0, feedbackAudio.Count)].Play();
                        spawnedParticle = Instantiate(catchParticle, transform.position, Quaternion.identity);
                    }
                }).OnComplete(() =>
                {
                    transform.localScale = Vector3.one * Random.Range(0.05f, 0.065f);

                    Vector2 randomPosition = Random.insideUnitCircle * Bolha.GetComponent<RadiusCircle>().radius / 2f;

                    transform.position = new Vector3(Bolha.position.x + randomPosition.x, Bolha.position.y + randomPosition.y, transform.position.z);

                    Material instance = GetComponent<SpriteRenderer>().material;
                    GetComponent<SpriteRenderer>().material = instance;

                    int[] possibleOrders = { 4, 3, 2 };
                    int randomOrder = possibleOrders[Random.Range(0, possibleOrders.Length)];
                    renderer.sortingOrder = randomOrder;

                    enabled = false;
                });
            }
            else
            {
                if (currentTween == null)
                currentTween = transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutQuad).OnStart(() =>
                {
                    if (spawnedParticle == null)
                    {
                        feedbackAudio[Random.Range(0, feedbackAudio.Count)].Play();
                        spawnedParticle = Instantiate(catchParticle, transform.position, Quaternion.identity);
                    }
                }).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
        else
        {
            Vector3 targetPos = transform.position;
            targetPos += Vector3.down * fallSpeed * Time.deltaTime;
            targetPos += Vector3.right * currentLateralSpeed * Time.deltaTime;

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, 1f * Time.deltaTime);

            ClampPositionWithinCamera();

            if (transform.position.y < lowerBound - 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Disable()
    {
        StartCoroutine(DisableAndEnable());
    }

    IEnumerator DisableAndEnable()
    {
        collider.enabled = false;
        renderer.enabled = false;

        yield return new WaitForSeconds(3);

        collider.enabled = true;
        renderer.enabled = true;
    }

    public void Collect()
    {
        collider.enabled = false;
        Collected = true;

        Vector3 direction = Bolha.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void SpeedController()
    {
        if (speed >= 1)
        {
            speedDown = true;
        }
        else if (speed <= 0.1f)
        {
            speedDown = false;
        }

        if (speedDown)
        {
            speed -= 1.5f * Time.deltaTime;
        }
        else
        {
            speed += 3 * Time.deltaTime;
        }
    }

    IEnumerator ChangeLateralDirection()
    {
        while (true)
        {
            currentLateralSpeed = Random.Range(-lateralSpeed, lateralSpeed);

            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void ClampPositionWithinCamera()
    {
        Vector3 position = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);

        if (viewportPosition.x <= 0.05f || viewportPosition.x >= 0.95f)
        {
            currentLateralSpeed = -currentLateralSpeed;

            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f);
            transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
        }
    }
}
