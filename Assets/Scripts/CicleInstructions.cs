using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicleInstructions : MonoBehaviour
{
    public GameObject childObject;

    private void Start()
    {
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (childObject != null)
        {
            childObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }
}