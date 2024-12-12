using UnityEngine;

public class BadButton : MonoBehaviour
{
    public GameObject Society;
    public GameObject Family;
    public GameObject You;

    void OnClick()
    {
        MoveObject(Society, Vector3.down);
        MoveObject(You, Vector3.up);
        Rigidbody rb = You.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void MoveObject(GameObject obj, Vector3 direction)
    {
        obj.transform.position += direction;
    }
}
