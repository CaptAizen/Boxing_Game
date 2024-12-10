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

    void Start()
    {
        // Initialize with the first combination
        SelectRandomCombination();
    }

    void Update()
    {
        if (audioManager.currentDecibelLevel > -9 && !targetActive)
        {
            // Activate the next target in the current combination
            ActivateNextTarget();
        }
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

    public void TargetDestroyed()
    {
        targetActive = false;
        currentTargetIndex++;

        // If all targets in the current combination are destroyed, select a new combination
        if (currentTargetIndex >= currentCombination.Length)
        {
            SelectRandomCombination();
        }
    }
}
