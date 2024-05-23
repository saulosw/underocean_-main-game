using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    // referência ao componente TextMeshProUGUI para exibir o texto do diálogo
    public TextMeshProUGUI dialogueText;
    // array de strings contendo as linhas de diálogo
    public string[] dialogueLines;
    // array de clipes de áudio correspondentes às linhas de diálogo
    public AudioClip[] dialogueAudioClips;
    // atraso entre a exibição de cada letra no texto
    public float letterDelay = 0.05f;
    // referência ao componente AudioSource para tocar os clipes de áudio
    public AudioSource audioSource;

    // índice da linha de diálogo atual
    private int currentLineIndex = 0;
    // indica se uma linha de diálogo está sendo exibida
    private bool isDisplaying = false;
    // referência à coroutine que exibe a linha de diálogo
    private Coroutine displayCoroutine;

    // método chamado no início do jogo
    void Start()
    {
        // se houver linhas de diálogo, inicia a exibição da primeira linha
        if (dialogueLines.Length > 0)
        {
            displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[currentLineIndex]));
        }
    }

    // método chamado a cada frame
    void Update()
    {
        // verifica se a tecla espaço foi pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // se uma linha de diálogo está sendo exibida
            if (isDisplaying)
            {
                // interrompe a exibição atual e exibe a linha completa
                StopCoroutine(displayCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                isDisplaying = false;
            }
            else
            {
                // avança para a próxima linha de diálogo, se houver
                currentLineIndex++;
                if (currentLineIndex < dialogueLines.Length)
                {
                    displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[currentLineIndex]));
                }
            }
        }
    }

    // coroutine para exibir uma linha de diálogo letra por letra
    IEnumerator DisplayLine(string line)
    {
        isDisplaying = true; // marca que uma linha de diálogo está sendo exibida
        dialogueText.text = ""; // limpa o texto do diálogo

        // se o componente AudioSource e o clipe de áudio correspondente existirem
        if (audioSource != null && dialogueAudioClips.Length > currentLineIndex)
        {
            audioSource.clip = dialogueAudioClips[currentLineIndex]; // define o clipe de áudio
            audioSource.Play(); // toca o clipe de áudio
        }

        // adiciona cada letra da linha de diálogo ao texto, com um atraso entre cada letra
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        isDisplaying = false; // marca que a exibição da linha de diálogo terminou
    }
}
