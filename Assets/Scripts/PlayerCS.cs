using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCS : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}

