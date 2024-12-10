using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array of footstep sounds
    public float stepDistance = 2.0f; // Distance between each footstep sound
    public LayerMask groundLayer; // Layer of the ground

    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float distanceTraveled;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTraveled >= stepDistance && IsGrounded())
        {
            PlayFootstepSound();
            distanceTraveled = 0f;
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.1f, groundLayer);
    }
}
