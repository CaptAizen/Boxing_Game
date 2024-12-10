using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Vector3 endPosition;
    public float desiredDuration;
    public string requiredKey; // The name of the required key (e.g., "Positivity", "Repentance", "Courage")
    private Vector3 startPosition;
    private float elapsedTime;
    private bool gatePassed = false;
    public PlayerKeys playerKeys; // Reference to the PlayerKeys script

    void Start()
    {
        startPosition = transform.position;
    }

    IEnumerator GateMoving()
    {
        while (elapsedTime < desiredDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            yield return null;
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
            if (string.IsNullOrEmpty(requiredKey) || IsKeyAvailable(requiredKey))
            {
                OpenGate();
            }
        }
    }

    bool IsKeyAvailable(string key)
    {
        switch (key)
        {
            case "Positivity":
                return playerKeys.Positivity;
            case "Repentance":
                return playerKeys.Repentance;
            case "Courage":
                return playerKeys.Courage;
            default:
                return false;
        }
    }
}
