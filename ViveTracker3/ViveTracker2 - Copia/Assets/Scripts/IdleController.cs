using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] int controllerNumber;
    [SerializeField] Vector2 initialPosition;
    [SerializeField] float distanceToMove = 0.1f;
    [SerializeField] Idle idleScreen;
    [SerializeField] float idleTime = 5;
    [SerializeField] float timeSinceLastMove = 0;
    [SerializeField] IdleManager IM;
    bool canCheckForMovement;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(waitForCheck());
    }

    public void ResetPosition()
    {
        initialPosition = transform.position;
        Debug.Log("Updating controller "+ controllerNumber+" initialPosition to " + initialPosition);
    }

    void Update()
    {
        if (initialPosition == new Vector2(999,999))
        {
            ResetPosition();
        }
    }

    private void LateUpdate()
    {
        if (canCheckForMovement)
        {
            float distanceMoved = Vector2.Distance(transform.position, initialPosition);

            if (distanceMoved > distanceToMove)
            {
                if (IsWithinHorizontalBounds())
                {
                    Debug.Log(controllerNumber + " = " + distanceMoved + ". Position is " + transform.position + ", initialPos is " + initialPosition);
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
        else
        {
            float distanceMoved = Vector2.Distance(transform.position, initialPosition);

            if (distanceMoved > distanceToMove)
            {
                ResetPosition();
            }
        }
    }

    IEnumerator waitForCheck()
    {
        yield return new WaitForSeconds(1);
        canCheckForMovement = true;
    }

    bool IsWithinHorizontalBounds()
    {
        float cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        float leftBound = mainCamera.transform.position.x - (cameraWidth / 2);
        float rightBound = mainCamera.transform.position.x + (cameraWidth / 2);

        return transform.position.x >= leftBound && transform.position.x <= rightBound;
    }
}
