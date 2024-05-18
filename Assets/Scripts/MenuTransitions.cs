using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    public void Play(string cena)
    {
        SceneManager.LoadScene(cena);
    }
    public void Quit(){
        Application.Quit();
    }
}
