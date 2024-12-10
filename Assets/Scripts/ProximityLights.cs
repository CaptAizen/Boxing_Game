using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ProximityLights : MonoBehaviour
{
    AudioSource LightsOn;
    AudioSource LightsOff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(true);
            LightsOn.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            LightsOff.Play();
            }
    }
}
