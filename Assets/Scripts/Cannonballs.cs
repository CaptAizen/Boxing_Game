using UnityEngine;

public class Cannonballs : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float decelerationRate = 0.1f;
    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * initialSpeed;
        audioSource = GetComponent<AudioSource>();

        // Play the firing sound effect
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        // Apply deceleration
        rb.velocity = rb.velocity * (1 - decelerationRate * Time.fixedDeltaTime);
    }
}
