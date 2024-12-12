using UnityEngine;

public class EntranceToTheTower : MonoBehaviour
{
    public GameObject[] Context; // Make sure to assign these in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in Context)
            {
                obj.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in Context)
            {
                obj.SetActive(true);
            }

        }
    }
}
