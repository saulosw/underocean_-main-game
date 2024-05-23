using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // referência ao controlador do jogo de pesca
    private FishingGameController fishingGameController;

    void Start()
    {
        // encontra e armazena a referência ao controlador do jogo de pesca
        fishingGameController = FindObjectOfType<FishingGameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // verifica se o objeto que colidiu tem a tag "ObTrigger"
        if (other.CompareTag("ObTrigger"))
        {
            // se a referência ao controlador do jogo de pesca não for nula
            if (fishingGameController != null)
            {
                // chama o método ToggleFishingGame no controlador de pesca, ativando o jogo de pesca
                fishingGameController.ToggleFishingGame(true);
            }
        }
    }
}
