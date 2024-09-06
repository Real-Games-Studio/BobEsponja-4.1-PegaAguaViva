using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Jellyfish : MonoBehaviour
{
    Collider2D collider;
    Renderer renderer;
    [SerializeField] string Name;
    [SerializeField] bool Collected;
    [SerializeField] Transform Bolha;
    public int score;

    float speed;
    bool speedDown;

    public float fallSpeed = 2f; // Velocidade de queda
    public float lateralSpeed = 0.5f; // Velocidade lateral máxima

    private float currentLateralSpeed;
    private Camera mainCamera;
    private float lowerBound;

    // Start is called before the first frame update
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
    }

    private void Update()
    {
        if (Collected)
        {
            //if(Vector2.Distance(transform.position, Bolha.position) > 0.1f)
            //{
            //    transform.position = Vector2.MoveTowards(transform.position, Bolha.position, speed * Time.deltaTime);
            //}
            //else
            //{
            if(score > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                transform.localScale = new Vector3(0.05f, 0.05f, 0.5f);

                Vector2 randomPosition = Random.insideUnitCircle * Bolha.GetComponent<RadiusCircle>().radius / 2f;

                transform.position = new Vector3(Bolha.position.x + randomPosition.x, Bolha.position.y + randomPosition.y, transform.position.z);

                int[] possibleOrders = { 4, 3, 2 };
                int randomOrder = possibleOrders[Random.Range(0, possibleOrders.Length)];
                renderer.sortingOrder = randomOrder;

                this.enabled = false;
            }
            else
            {
                Destroy(gameObject);
            }
            
            //}

            //SpeedController();
        }
        else
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            transform.position += Vector3.right * currentLateralSpeed * Time.deltaTime;

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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
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
            // Define uma nova velocidade lateral aleatória
            currentLateralSpeed = Random.Range(-lateralSpeed, lateralSpeed);

            // Espera um intervalo aleatório entre 2 e 4 segundos
            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void ClampPositionWithinCamera()
    {
        Vector3 position = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);

        // Garante que o objeto esteja dentro dos limites da câmera
        if (viewportPosition.x <= 0.05f || viewportPosition.x >= 0.95f)
        {
            // Inverte a direção lateral
            currentLateralSpeed = -currentLateralSpeed;

            // Garante que o objeto não saia dos limites
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f);
            transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
        }
    }
}
