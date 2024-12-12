using UnityEngine;
using System.Collections;

public class DangerSign : MonoBehaviour
{
    public float Cannonballs; // Array of GameObject1 instances
    public GameObject Platform; // GameObject2 instance
    public Material whiteMaterial;
    public Material yellowMaterial;
    public Material redMaterial;
    public Material blackMaterial;

    void Update()
    {
        float count = Cannonballs;
        Renderer renderer = Platform.GetComponent<Renderer>();

        switch (count)
        {
            case <= 5:
                renderer.material = whiteMaterial;
                break;
            case > 5 and <= 10:
                StartCoroutine(ChangeColor(renderer, whiteMaterial, yellowMaterial, 2f));
                break;
            case > 10 and <= 13:
                renderer.material = yellowMaterial;
                break;
            case > 13 and <= 15:
                StartCoroutine(ChangeColor(renderer, yellowMaterial, redMaterial, 2f));
                break;
            case > 15 and <= 17:
                renderer.material = redMaterial;
                break;
            case > 17 and <= 19:
                StartCoroutine(ChangeColor(renderer, redMaterial, blackMaterial, 1f, 2f));
                break;
            case 20:
                Platform.SetActive(false);
                break;
        }
    }

    IEnumerator ChangeColor(Renderer renderer, Material firstMaterial, Material secondMaterial, float duration1, float duration2 = 0f)
    {
        renderer.material = firstMaterial;
        yield return new WaitForSeconds(duration1);
        renderer.material = secondMaterial;
        if (duration2 > 0f)
        {
            yield return new WaitForSeconds(duration2);
            renderer.material = firstMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CannonBall"))
        {
            Cannonballs += 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CannonBall"))
        {
            Cannonballs -= 1;
        }
    }
}

