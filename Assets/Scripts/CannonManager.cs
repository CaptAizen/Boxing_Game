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


    private int ballsFired = 0;

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
        while (ballsFired < maxBalls)
        {
            // Choose a random cannon
            Cannon cannon = cannons[Random.Range(0, cannons.Length)];

            // Instantiate the cannonball
            GameObject cannonball = Instantiate(cannonballPrefab, cannon.position.position, Quaternion.identity);

            // Apply a random material
            Renderer renderer = cannonball.GetComponent<Renderer>();
            renderer.material = materials[Random.Range(0, materials.Length)];
            if (ballsFired == maxBalls - 1)
            {
                renderer.material = finaleMaterial;
            }

            // Calculate the initial velocity
            Vector3 direction = Quaternion.Euler(0, cannon.angle, 0) * Vector3.forward;
            Rigidbody rb = cannonball.GetComponent<Rigidbody>();
            rb.velocity = direction * initialSpeed;

            // Start coroutine to check for destruction
            StartCoroutine(CheckForDestruction(cannonball));

            ballsFired++;
            yield return new WaitForSeconds(fireDelay);

            if (ballsFired == maxBalls)
            {
                entranceStairs.SetActive(true);
                levelCompleteText.SetActive(true);
                playerKeys.Positivity = true;
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
}
