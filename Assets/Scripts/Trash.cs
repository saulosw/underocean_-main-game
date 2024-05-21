using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxY = 200.0f;
    public float minY = 0.0f;
    public float waitTime = 2.0f;

    private bool movingUp = true;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(MoveTrash());
    }

    IEnumerator MoveTrash()
    {
        while (true)
        {
            if (movingUp)
            {
                while (rectTransform.anchoredPosition.y < maxY)
                {
                    rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
                movingUp = false;
            }
            else
            {
                while (rectTransform.anchoredPosition.y > minY)
                {
                    rectTransform.anchoredPosition += Vector2.down * speed * Time.deltaTime;
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
                movingUp = true;
            }
        }
    }
}