using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRock : MonoBehaviour
{

    public GameObject _rockTouchCube;
    public static SCRock instance;
    private void Awake()
    {
        instance = this;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _rockTouchCube = other.gameObject;
            _rockTouchCube.GetComponent<Tile>().isThereRock = true;
            _rockTouchCube.GetComponent<Tile>()._touchAnything = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _rockTouchCube.GetComponent<Tile>().isThereRock = false;
            _rockTouchCube.GetComponent<Tile>()._touchAnything = false;
        }
    }

}
