using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FishingGameController fishingGameController;

    void Start()
    {
        // Encontra o FishingGameController na cena
        fishingGameController = FindObjectOfType<FishingGameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador entrou no trigger
        if (other.CompareTag("ObTrigger"))
        {
            // Verifica se o FishingGameController foi encontrado
            if (fishingGameController != null)
            {
                // Ativa o jogo de pesca
                fishingGameController.ToggleFishingGame(true);
            }
        }
    }
}
