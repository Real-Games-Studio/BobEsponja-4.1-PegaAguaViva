using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleController : MonoBehaviour
{
    Vector2 initialPosition;
    private Camera mainCamera;
    [SerializeField] int controllerNumber;
    [SerializeField] float distanceToMove = 0.1f;
    [SerializeField] Idle idleScreen;
    [SerializeField] float idleTime = 5;
    [SerializeField] float timeSinceLastMove = 0;
    [SerializeField] IdleManager IM;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void ResetPosition()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (initialPosition == null || initialPosition == Vector2.zero)
        {
            ResetPosition();
        }

        float distanceMoved = Vector2.Distance(transform.position, initialPosition);

        if (distanceMoved > distanceToMove)
        {
            if (IsWithinHorizontalBounds())
            {
                idleScreen.StartGame();
            }

            timeSinceLastMove = 0;
            IM.controllersIdle[controllerNumber] = false;
            ResetPosition();
        }
        else
        {
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= idleTime)
            {
                //idleScreen.ResetGame();
                IM.controllersIdle[controllerNumber] = true;
            }
        }
    }

    bool IsWithinHorizontalBounds()
    {
        float cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        float leftBound = mainCamera.transform.position.x - (cameraWidth / 2);
        float rightBound = mainCamera.transform.position.x + (cameraWidth / 2);

        return transform.position.x >= leftBound && transform.position.x <= rightBound;
    }
}
