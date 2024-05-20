using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    public AudioClip[] dialogueAudioClips;
    public float letterDelay = 0.05f;
    public AudioSource audioSource;

    private int currentLineIndex = 0;
    private bool isDisplaying = false;
    private Coroutine displayCoroutine;

    void Start()
    {
        if (dialogueLines.Length > 0)
        {
            displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[currentLineIndex]));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDisplaying)
            {
                StopCoroutine(displayCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                isDisplaying = false;
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < dialogueLines.Length)
                {
                    displayCoroutine = StartCoroutine(DisplayLine(dialogueLines[currentLineIndex]));
                }
            }
        }
    }

    IEnumerator DisplayLine(string line)
    {
        isDisplaying = true;
        dialogueText.text = "";

        if (audioSource != null && dialogueAudioClips.Length > currentLineIndex)
        {
            audioSource.clip = dialogueAudioClips[currentLineIndex];
            audioSource.Play();
        }

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        isDisplaying = false;
    }
}
