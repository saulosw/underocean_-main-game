using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FishingGameController fishingGameController;

    void Start()
    {
        fishingGameController = FindObjectOfType<FishingGameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObTrigger"))
        {
            if (fishingGameController != null)
            {
                fishingGameController.ToggleFishingGame(true);
            }
        }
    }
}
