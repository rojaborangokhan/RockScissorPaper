using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRock : MonoBehaviour
{

    public GameObject _rockTouchCube;
    public static SCRock instance;
    private List<GameObject> _cubesNears;
    private void Awake()
    {
        instance = this;
        _cubesNears = new List<GameObject>();
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

    void KnowNeighbor()
    {
        _cubesNears = _rockTouchCube.GetComponent<Tile>().neighObjects;
    }

}
