using UnityEngine;
using UnityEngine.UI;

public class FishingBarController : MonoBehaviour
{
    public float moveSpeed = 100f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float input = Input.GetAxis("Vertical");
        rectTransform.anchoredPosition += new Vector2(0, input * moveSpeed * Time.deltaTime);
        
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            Mathf.Clamp(rectTransform.anchoredPosition.y, -150, 140)
        );
    }
}