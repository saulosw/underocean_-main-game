using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FishingGameController : MonoBehaviour
{
    int points = 0;
    float currentCatchTime = 0f;
    public RectTransform fishingBar;
    public RectTransform fishArea;
    public GameObject[] elementsToToggle;
    public GameObject lixo;
    public TextMeshProUGUI pointsText;
    public Image movingImage;
    public float moveSpeed = 50f;
    public float moveDistance = 100f;
    public float moveInterval = 2f;

    public float moveIntervalVerdadeiro = 0;

    public Material waterMaterial;
    public Color startColor = Color.blue;
    public Color endColor = Color.cyan;
    public int maxPoints = 100;

    private bool isCatching = false;

    void Start()
    {
        ToggleFishingGame(false); // Inicia o jogo de pesca desativado
        StartCoroutine(MoveImageRoutine());
    }

    void Update()
    {
        // Verifica se o jogador pressionou a tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleFishingGame(!isCatching); // Alterna o estado do jogo de pesca
        }

        // Verifica se o jogador está movendo a barra
        if (isCatching)
        {
            if (Input.GetKey(KeyCode.K))
            {
                MoveBarUp();
            }
            else if (Input.GetKey(KeyCode.L))
            {
                MoveBarDown();
            }

            if (RectTransformUtility.RectangleContainsScreenPoint(fishArea, fishingBar.position))
            {
                currentCatchTime += Time.deltaTime; // Incrementa o tempo de captura
                if (currentCatchTime >= moveInterval)
                {
                    // Lógica de captura de peixe
                    Debug.Log("Fish Caught!");
                    AddPoint();
                    currentCatchTime = 0f; // Reseta o tempo de captura
                }
            }
            else
            {
                // Resetar o tempo de captura se a barra não estiver cobrindo a área do peixe
                currentCatchTime = 0f;
            }
        }
    }

    // Método para ativar ou desativar o jogo de pesca
    public void ToggleFishingGame(bool activate)
    {
        isCatching = activate;

        foreach (GameObject element in elementsToToggle)
        {
            if (element != lixo)
            {
                element.SetActive(activate);
            }
        }

        if (!activate)
        {
            currentCatchTime = 0f;
        }
    }

    void AddPoint()
    {
        points++;
        pointsText.text = points.ToString();
        UpdateWaterColor();
    }

    void UpdateWaterColor()
    {
        float t = Mathf.Clamp01((float)points / maxPoints);
        Color currentColor = Color.Lerp(startColor, endColor, t);
        waterMaterial.SetColor("_BaseColor", currentColor);
    }

    void MoveBarUp()
    {
        // Mover a barra para cima
        fishingBar.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;
    }

    void MoveBarDown()
    {
        // Mover a barra para baixo
        fishingBar.anchoredPosition -= Vector2.up * moveSpeed * Time.deltaTime;
    }

    IEnumerator MoveImageRoutine()
    {
        while (true)
        {
            while (movingImage.rectTransform.anchoredPosition.y < moveDistance)
            {
                movingImage.rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;
                yield return null;
            }

            while (movingImage.rectTransform.anchoredPosition.y > -moveDistance)
            {
                movingImage.rectTransform.anchoredPosition -= Vector2.up * moveSpeed * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(moveIntervalVerdadeiro);
        }
    }
}
