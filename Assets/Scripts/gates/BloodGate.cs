using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Vector3 endPosition;
    public float desiredDuration;
    private Vector3 startPosition;
    private float elapsedTime;
    private bool gatePassed = false;
    PlayerKeys playerKeys;

    void Start()
    {
        startPosition = transform.position;
    }

    IEnumerator GateMoving()
    {
        if (playerKeys.Courage)
        {
            while (elapsedTime < desiredDuration)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / desiredDuration;
                transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
                yield return null;
            }
        }
    }

    public void OpenGate()
    {
        if (!gatePassed)
        {
            gatePassed = true;
            elapsedTime = 0f;
            StartCoroutine(GateMoving());
        }
    }

    public void ResetGate()
    {
        gatePassed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
                OpenGate();
            
        }
    }
}
