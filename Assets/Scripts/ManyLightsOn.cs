using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivator : MonoBehaviour
{
    public GameObject[] lights; // Array of lights to activate

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }
        }
    }
}
