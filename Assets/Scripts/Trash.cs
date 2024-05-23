using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour
{
    // define a velocidade de movimento do objeto
    public float speed = 1.0f;

    // define a posição máxima no eixo y que o objeto pode alcançar
    public float maxY = 200.0f;

    // define a posição mínima no eixo y que o objeto pode alcançar
    public float minY = 0.0f;

    // define o tempo de espera ao alcançar as posições máxima e mínima
    public float waitTime = 2.0f;

    // variável que controla a direção do movimento, inicialmente definida para subir
    private bool movingUp = true;

    // referência ao componente RectTransform do objeto
    private RectTransform rectTransform;

    void Start()
    {
        // obtém a referência ao componente RectTransform anexado ao GameObject
        rectTransform = GetComponent<RectTransform>();

        // inicia a corrotina que controla o movimento do objeto
        StartCoroutine(MoveTrash());
    }

    IEnumerator MoveTrash()
    {
        // laço infinito para continuar o movimento do objeto indefinidamente
        while (true)
        {
            if (movingUp)
            {
                // enquanto a posição y do objeto for menor que maxY, ele continua subindo
                while (rectTransform.anchoredPosition.y < maxY)
                {
                    // atualiza a posição y do objeto incrementando conforme a velocidade definida
                    rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
                    yield return null;
                }

                // após alcançar maxY, espera pelo tempo definido em waitTime
                yield return new WaitForSeconds(waitTime);

                // muda a direção do movimento para descer
                movingUp = false;
            }
            else
            {
                // enquanto a posição y do objeto for maior que minY, ele continua descendo
                while (rectTransform.anchoredPosition.y > minY)
                {
                    // atualiza a posição y do objeto decrementando conforme a velocidade definida
                    rectTransform.anchoredPosition += Vector2.down * speed * Time.deltaTime;
                    yield return null;
                }

                // após alcançar minY, espera pelo tempo definido em waitTime
                yield return new WaitForSeconds(waitTime);

                // muda a direção do movimento para subir
                movingUp = true;
            }
        }
    }
}
