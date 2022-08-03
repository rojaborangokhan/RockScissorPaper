using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCScrissor : MonoBehaviour
{

    public GameObject _scissorTouchCube;
    public static SCScrissor instance;
    private void Awake()
    {
        instance = this;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _scissorTouchCube = other.gameObject;
            _scissorTouchCube.GetComponent<Tile>().isThereScissor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _scissorTouchCube.GetComponent<Tile>().isThereScissor = false;
        }
    }
    

}
