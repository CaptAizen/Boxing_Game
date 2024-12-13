using UnityEngine;

public class GoodButton : MonoBehaviour
{
    public GameObject Society;
    public GameObject Family;
    public GameObject You;

    void OnClick()
    {
        MoveObject(Society, Vector3.up);
        MoveObject(Family, Vector3.up);
        MoveObject(You, Vector3.down);
    }

    void MoveObject(GameObject obj, Vector3 direction)
    {
        obj.transform.position += direction;
    }
}
