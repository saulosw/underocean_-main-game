using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour
{
    public float speed = 1.0f; // Velocidade de movimento do botão
    public float maxY = 200.0f; // Altura máxima que o botão pode alcançar
    public float minY = 0.0f; // Altura mínima que o botão pode alcançar
    public float waitTime = 2.0f; // Tempo de espera antes de mudar de direção (em segundos)

    private bool movingUp = true; // Indica se o botão está se movendo para cima
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // Inicia a corrotina para mover o botão
        StartCoroutine(MoveTrash());
    }

    IEnumerator MoveTrash()
    {
        while (true)
        {
            if (movingUp)
            {
                // Move o botão para cima
                while (rectTransform.anchoredPosition.y < maxY)
                {
                    rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
                    yield return null;
                }
                // Espera um tempo antes de mudar de direção
                yield return new WaitForSeconds(waitTime);
                movingUp = false;
            }
            else
            {
                // Move o botão para baixo
                while (rectTransform.anchoredPosition.y > minY)
                {
                    rectTransform.anchoredPosition += Vector2.down * speed * Time.deltaTime;
                    yield return null;
                }
                // Espera um tempo antes de mudar de direção
                yield return new WaitForSeconds(waitTime);
                movingUp = true;
            }
        }
    }
}