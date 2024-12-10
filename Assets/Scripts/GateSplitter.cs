using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public List<Gate> gates; // List of gates to manage

    public void ResetAllGates()
    {
        foreach (Gate gate in gates)
        {
            gate.ResetGate();
        }
    }
}
