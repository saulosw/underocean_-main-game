using UnityEngine;
using UnityEngine.UI;

public class FishingBarController : MonoBehaviour
{
    // velocidade de movimento da barra de pesca
    public float moveSpeed = 100f;
    // referência ao componente RectTransform da barra de pesca
    private RectTransform rectTransform;

    // método chamado no início do jogo
    void Start()
    {
        // obtém o componente RectTransform associado a este GameObject
        rectTransform = GetComponent<RectTransform>();
    }

    // método chamado a cada frame
    void Update()
    {
        // obtém a entrada do eixo vertical (setas do teclado ou joystick)
        float input = Input.GetAxis("Vertical");
        // ajusta a posição da barra de pesca com base na entrada do jogador e na velocidade de movimento
        rectTransform.anchoredPosition += new Vector2(0, input * moveSpeed * Time.deltaTime);
        
        // restringe a posição vertical da barra de pesca dentro dos limites especificados
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            Mathf.Clamp(rectTransform.anchoredPosition.y, -150, 140) // limites de -150 a 140 unidades
        );
    }
}
