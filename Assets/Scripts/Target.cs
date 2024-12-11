using UnityEngine;

public class Target : MonoBehaviour
{
    private TargetManager targetManager;

    void Start()
    {
        targetManager = FindObjectOfType<TargetManager>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the TargetManager that this target is destroyed
            targetManager.TargetDestroyed(this);
            Hide();
        }
    }
}
