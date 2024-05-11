using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
        // Carrega a cena especificada pelo nome fornecido
    public void Play(string cena)
    {
        SceneManager.LoadScene(cena);
    }
        // Fecha o aplicativo
    public void Quit(){
        Application.Quit();
    }
}
