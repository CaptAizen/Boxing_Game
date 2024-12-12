using UnityEngine;
using System.Collections;

public class DangerSign : MonoBehaviour
{
    public int Cannonballs; // Count of GameObject1 instances
    public GameObject Platform; // GameObject2 instance
    public Material whiteMaterial;
    public Material yellowMaterial;
    public Material redMaterial;
    public Material blackMaterial;

    private Coroutine colorChangeCoroutine;

    void Update()
    {
        Renderer renderer = Platform.GetComponent<Renderer>();

        if (Cannonballs <= 5)
        {
            SetMaterial(renderer, whiteMaterial);
        }
        else if (Cannonballs > 5 && Cannonballs <= 10)
        {
            StartColorChange(renderer, whiteMaterial, yellowMaterial, .01f);
        }
        else if (Cannonballs > 10 && Cannonballs <= 13)
        {
            SetMaterial(renderer, yellowMaterial);
        }
        else if (Cannonballs > 13 && Cannonballs <= 15)
        {
            StartColorChange(renderer, yellowMaterial, redMaterial, .01f);
        }
        else if (Cannonballs > 15 && Cannonballs <= 17)
        {
            SetMaterial(renderer, redMaterial);
        }
        else if (Cannonballs > 17 && Cannonballs <= 19)
        {
            StartColorChange(renderer, redMaterial, blackMaterial, .01f);
        }
        else if (Cannonballs == 20)
        {
            Platform.SetActive(false);
        }
    }

    void SetMaterial(Renderer renderer, Material material)
    {
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
        renderer.material = material;
    }

    void StartColorChange(Renderer renderer, Material firstMaterial, Material secondMaterial, float duration1, float duration2 = 0f)
    {
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
        colorChangeCoroutine = StartCoroutine(ChangeColor(renderer, firstMaterial, secondMaterial, duration1, duration2));
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
        if (other.CompareTag("CannonBall"))
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
