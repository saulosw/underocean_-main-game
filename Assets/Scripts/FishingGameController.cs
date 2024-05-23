using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FishingGameController : MonoBehaviour
{
    // variáveis de controle de pontos e tempo de captura atual
    int points = 0; // pontuação inicial do jogador
    float currentCatchTime = 0f; // tempo atual de captura do peixe

    // referências aos elementos da interface do usuário e do jogo
    public RectTransform fishingBar; // barra de pesca que o jogador controla
    public RectTransform fishArea; // área onde os peixes aparecem
    public GameObject[] elementsToToggle; // elementos da interface que podem ser ativados/desativados
    public GameObject lixo; // objeto específico que não deve ser desativado
    public TextMeshProUGUI pointsText; // texto que exibe a pontuação do jogador
    public Image movingImage; // imagem que se move no jogo

    // variáveis de velocidade e intervalo de movimento
    public float moveSpeedBar = 50f; // velocidade de movimento da barra de pesca
    public float moveSpeed = 50f; // velocidade de movimento da imagem
    public float moveDistance = 100f; // distância de movimento da imagem
    public float moveInterval = 2f; // intervalo de tempo para capturar um peixe

    // variável adicional para intervalos de movimento
    public float moveIntervalVerdadeiro = 0; // intervalo de tempo real para o movimento da imagem

    // variáveis para a cor da água e o controle de pontos máximos
    public Material waterMaterial; // material da água para mudança de cor
    public Color startColor = Color.blue; // cor inicial da água
    public Color endColor = Color.cyan; // cor final da água
    public int maxPoints = 100; // pontos máximos para a cor final da água

    // variável de estado de captura
    private bool isCatching = false; // indica se o jogador está capturando um peixe

    // variáveis de áudio
    public AudioSource audioSource; // fonte de áudio para tocar sons
    public AudioClip catchSound; // som a ser tocado quando um peixe é capturado

    void Start()
    {
        // desativa o jogo de pesca no início
        ToggleFishingGame(false);
        // inicia a rotina de movimento da imagem
        StartCoroutine(MoveImageRoutine());
    }

    void Update()
    {
        // alterna o estado do jogo de pesca quando a tecla E é pressionada
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleFishingGame(!isCatching);
        }

        // se o jogo de pesca está ativo
        if (isCatching)
        {
            // move a barra de pesca para cima quando a tecla K é pressionada
            if (Input.GetKey(KeyCode.K))
            {
                MoveBarUp();
            }
            // move a barra de pesca para baixo quando a tecla L é pressionada
            else if (Input.GetKey(KeyCode.L))
            {
                MoveBarDown();
            }

            // verifica se a barra de pesca está dentro da área do peixe
            if (RectTransformUtility.RectangleContainsScreenPoint(fishArea, fishingBar.position))
            {
                // incrementa o tempo de captura
                currentCatchTime += Time.deltaTime;
                // se o tempo de captura for suficiente, adiciona um ponto
                if (currentCatchTime >= moveInterval)
                {
                    Debug.Log("Fish Caught!");
                    AddPoint();
                    currentCatchTime = 0f;
                }
            }
            else
            {
                // reseta o tempo de captura se a barra sair da área do peixe
                currentCatchTime = 0f;
            }
        }
    }

    // ativa ou desativa o mini-jogo de pesca
    public void ToggleFishingGame(bool activate)
    {
        isCatching = activate;

        // ativa ou desativa elementos específicos do jogo de pesca
        foreach (GameObject element in elementsToToggle)
        {
            // verifica se o elemento não é o objeto 'lixo' antes de alterar seu estado
            if (element != lixo)
            {
                element.SetActive(activate);
            }
        }

        // reseta o tempo de captura se o mini-jogo for desativado
        if (!activate)
        {
            currentCatchTime = 0f;
        }
    }

    // adiciona um ponto ao jogador
    void AddPoint()
    {
        points++; // incrementa a pontuação
        pointsText.text = points.ToString(); // atualiza o texto da pontuação
        UpdateWaterColor(); // atualiza a cor da água
        PlayCatchSound(); // toca o som de captura

        // se a pontuação atingir ou exceder 25, carrega a próxima cena
        if (points >= 25)
        {
            LoadNextScene();
        }
    }

    // toca o som de captura de peixe
    void PlayCatchSound()
    {
        if (audioSource != null && catchSound != null)
        {
            audioSource.PlayOneShot(catchSound); // toca o som uma vez
        }
    }

    // atualiza a cor da água com base na pontuação
    void UpdateWaterColor()
    {
        // calcula a interpolação da cor com base na pontuação atual
        float t = Mathf.Clamp01((float)points / maxPoints);
        Color currentColor = Color.Lerp(startColor, endColor, t);
        waterMaterial.SetColor("_BaseColor", currentColor); // aplica a cor interpolada ao material da água
    }

    // move a barra de pesca para cima
    void MoveBarUp()
    {
        fishingBar.anchoredPosition += Vector2.up * moveSpeedBar * Time.deltaTime;
    }

    // move a barra de pesca para baixo
    void MoveBarDown()
    {
        fishingBar.anchoredPosition -= Vector2.up * moveSpeedBar * Time.deltaTime;
    }

    // rotina para mover a imagem associada para cima e para baixo
    IEnumerator MoveImageRoutine()
    {
        while (true)
        {
            // move a imagem para cima até alcançar a distância especificada
            while (movingImage.rectTransform.anchoredPosition.y < moveDistance)
            {
                movingImage.rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;
                yield return null; // espera até o próximo frame
            }

            // move a imagem para baixo até alcançar a distância especificada
            while (movingImage.rectTransform.anchoredPosition.y > -moveDistance)
            {
                movingImage.rectTransform.anchoredPosition -= Vector2.up * moveSpeed * Time.deltaTime;
                yield return null; // espera até o próximo frame
            }

            // espera pelo intervalo especificado antes de repetir o movimento
            yield return new WaitForSeconds(moveIntervalVerdadeiro);
        }
    }

    // carrega a próxima cena chamada "Win"
    void LoadNextScene()
    {
        SceneManager.LoadScene("Win");
    }
}
