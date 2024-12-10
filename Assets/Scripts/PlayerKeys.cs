using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    public bool Positivity = false;
    public bool Repentance = false;
    public bool Courage = false;

    public GateOpener gateOpener; // Reference to the GateOpener script

    public void ObtainKey(string key)
    {
        switch (key)
        {
            case "Positivity":
                Positivity = true;
                break;
            case "Repentance":
                Repentance = true;
                break;
            case "Courage":
                Courage = true;
                break;
        }

        // Reset all gates when a new key is obtained
        gateOpener.ResetAllGates();
    }
}
