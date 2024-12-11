using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetCombination
{
    public Target[] targets; // Array of targets for this combination
}

public class TargetManager : MonoBehaviour
{
    public AudioManager audioManager;
    public List<TargetCombination> targetCombinations; // List of target combinations
    private Target[] currentCombination;
    private int currentTargetIndex = 0;
    private bool targetActive = false;

    private Queue<float> decibelLevels = new Queue<float>();
    private const float trackingDuration = 5f; // Duration to track decibel levels (in seconds)
    private const int sampleRate = 10; // Number of samples per second

    void Start()
    {
        // Initialize with the first combination
        SelectRandomCombination();
        StartCoroutine(TrackDecibelLevels());
    }

    void Update()
    {
        float averageDecibelLevel = CalculateAverageDecibelLevel();
        float standardDeviation = CalculateStandardDeviation(averageDecibelLevel);

        if (audioManager.currentDecibelLevel > averageDecibelLevel + 2 * standardDeviation && !targetActive)
        {
            // Activate the next target in the current combination
            ActivateNextTarget();
        }
    }

    IEnumerator TrackDecibelLevels()
    {
        while (true)
        {
            if (decibelLevels.Count >= trackingDuration * sampleRate)
            {
                decibelLevels.Dequeue();
            }
            decibelLevels.Enqueue(audioManager.currentDecibelLevel);
            yield return new WaitForSeconds(1f / sampleRate);
        }
    }

    float CalculateAverageDecibelLevel()
    {
        float sum = 0f;
        foreach (float level in decibelLevels)
        {
            sum += level;
        }
        return sum / decibelLevels.Count;
    }

    float CalculateStandardDeviation(float average)
    {
        float sum = 0f;
        foreach (float level in decibelLevels)
        {
            sum += Mathf.Pow(level - average, 2);
        }
        return Mathf.Sqrt(sum / decibelLevels.Count);
    }

    void SelectRandomCombination()
    {
        // Randomly select a combination from the list
        currentCombination = targetCombinations[Random.Range(0, targetCombinations.Count)].targets;
        currentTargetIndex = 0;
    }

    void ActivateNextTarget()
    {
        if (currentTargetIndex < currentCombination.Length)
        {
            currentCombination[currentTargetIndex].Show();
            targetActive = true;
        }
    }

    public void TargetDestroyed(Target destroyedTarget)
    {
        // Play the breaking sound effect from the destroyed target's AudioSource
        AudioSource audioSource = destroyedTarget.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        targetActive = false;
        currentTargetIndex++;

        // If all targets in the current combination are destroyed, select a new combination
        if (currentTargetIndex >= currentCombination.Length)
        {
            SelectRandomCombination();
        }
    }
}
