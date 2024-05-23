using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicleInstructions : MonoBehaviour
{
    // referência ao GameObject filho que será ativado/desativado
    public GameObject childObject;

    // método chamado no início do jogo
    private void Start()
    {
        // verifica se o childObject não é nulo
        if (childObject != null)
        {
            // desativa o childObject no início do jogo
            childObject.SetActive(false);
        }
    }

    // método chamado quando outro collider entra no trigger deste GameObject
    private void OnTriggerEnter(Collider other)
    {
        // verifica se o childObject não é nulo
        if (childObject != null)
        {
            // ativa o childObject quando um collider entra no trigger
            childObject.SetActive(true);
        }
    }

    // método chamado quando outro collider sai do trigger deste GameObject
    private void OnTriggerExit(Collider other)
    {
        // verifica se o childObject não é nulo
        if (childObject != null)
        {
            // desativa o childObject quando um collider sai do trigger
            childObject.SetActive(false);
        }
    }
}
