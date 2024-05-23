using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCS : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // verifica se o objeto que colidiu tem a tag "Water"
        if (other.CompareTag("Water"))
        {
            // carrega a cena chamada "Game"
            SceneManager.LoadScene("Game");
        }
    }
}
