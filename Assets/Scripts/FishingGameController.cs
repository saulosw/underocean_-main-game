using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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

    public float moveSpeedBar = 50f;
    public float moveSpeed = 50f;
    public float moveDistance = 100f;
    public float moveInterval = 2f;

    public float moveIntervalVerdadeiro = 0;

    public Material waterMaterial;
    public Color startColor = Color.blue;
    public Color endColor = Color.cyan;
    public int maxPoints = 100;

    private bool isCatching = false;

    public AudioSource audioSource;
    public AudioClip catchSound;

    void Start()
    {
        ToggleFishingGame(false);
        StartCoroutine(MoveImageRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleFishingGame(!isCatching);
        }

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
                currentCatchTime += Time.deltaTime;
                if (currentCatchTime >= moveInterval)
                {
                    Debug.Log("Fish Caught!");
                    AddPoint();
                    currentCatchTime = 0f;
                }
            }
            else
            {
                currentCatchTime = 0f;
            }
        }
    }

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
        PlayCatchSound();

        if (points >= 30)
        {
            LoadNextScene();
        }
    }

    void PlayCatchSound()
    {
        if (audioSource != null && catchSound != null)
        {
            audioSource.PlayOneShot(catchSound);
        }
    }

    void UpdateWaterColor()
    {
        float t = Mathf.Clamp01((float)points / maxPoints);
        Color currentColor = Color.Lerp(startColor, endColor, t);
        waterMaterial.SetColor("_BaseColor", currentColor);
    }

    void MoveBarUp()
    {
        fishingBar.anchoredPosition += Vector2.up * moveSpeedBar * Time.deltaTime;
    }

    void MoveBarDown()
    {
        fishingBar.anchoredPosition -= Vector2.up * moveSpeedBar * Time.deltaTime;
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

    void LoadNextScene()
    {
        SceneManager.LoadScene("Win");
    }
}