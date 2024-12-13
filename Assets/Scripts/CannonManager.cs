using UnityEngine;
using System.Collections;

public class CannonManager : MonoBehaviour
{
    [System.Serializable]
    public class Cannon
    {
        public Transform position;
        public float angle; // Angle in degrees
    }

    public Cannon[] cannons;
    public GameObject cannonballPrefab;
    public float initialSpeed = 10f;
    public float fireDelay = 1f;
    public int maxBalls = 10;
    [SerializeField] public Material[] materials; // Array of materials
    public Material finaleMaterial;
    public float destroyYValue = -10f; // Y value below which cannonballs are destroyed
    public GameObject entranceStairs;
    public AudioSource cannonBallMusic;
    public GameObject entranceText;
    public GameObject levelCompleteText;
    PlayerKeys playerKeys;
    DangerSign dangerSign;
    public float VolumeStopSpeed;


    public int ballsFired = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FireCannonballs());
            entranceStairs.SetActive(false);
            cannonBallMusic.Play();
            entranceText.SetActive(false);
        }
    }

    IEnumerator FireCannonballs()
    {
        while (ballsFired <= maxBalls)
        {
            // Choose a random cannon
            Cannon cannon = cannons[Random.Range(0, cannons.Length)];

            // Instantiate the cannonball
            GameObject cannonball = Instantiate(cannonballPrefab, cannon.position.position, Quaternion.identity);

            // Apply a random material
            Renderer renderer = cannonball.GetComponent<Renderer>();
            renderer.material = materials[Random.Range(0, materials.Length)];
            Rigidbody rb = cannonball.GetComponent<Rigidbody>();  
            if (ballsFired == maxBalls - 1)
            {
                renderer.material = finaleMaterial;
                rb.mass = 1000000;
            }

            // Calculate the initial velocity
            Vector3 direction = Quaternion.Euler(0, cannon.angle, 0) * Vector3.forward;
            rb.velocity = direction * initialSpeed;

            // Start coroutine to check for destruction
            StartCoroutine(CheckForDestruction(cannonball));

            ballsFired++;
            yield return new WaitForSeconds(fireDelay);

            if (ballsFired == maxBalls && dangerSign.Cannonballs <= 1)
            {
                entranceStairs.SetActive(true);
                levelCompleteText.SetActive(true);
                playerKeys.Positivity = true;
                StartCoroutine(MusicStopper());
            }
        }
    }

    IEnumerator CheckForDestruction(GameObject cannonball)
    {
        while (cannonball.transform.position.y > destroyYValue)
        {
            yield return null;
        }
        Destroy(cannonball);
    }
    IEnumerator MusicStopper()
    {
        while (cannonBallMusic.volume > 0)
        {
            cannonBallMusic.volume -= VolumeStopSpeed;
        }
        yield break;
    }
}
