using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    // método para carregar uma nova cena
    public void Play(string cena)
    {
        // carrega a cena com o nome passado como parâmetro
        SceneManager.LoadScene(cena);
    }

    // método para sair do aplicativo
    public void Quit()
    {
        // fecha o aplicativo
        Application.Quit();
    }
}
