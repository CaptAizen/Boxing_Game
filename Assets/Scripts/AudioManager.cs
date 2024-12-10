using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    private float[] samples = new float[256];
    public float currentDecibelLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // Get the audio data
        audioSource.GetOutputData(samples, 0);

        // Calculate the RMS value
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        float rmsValue = Mathf.Sqrt(sum / samples.Length);

        // Convert RMS value to decibels
        currentDecibelLevel = 20 * Mathf.Log10(rmsValue / 0.1f);

        // Clamp the value to avoid negative infinity
        if (currentDecibelLevel < -80) currentDecibelLevel = -80;
    }
}
