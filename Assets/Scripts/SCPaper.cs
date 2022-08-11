using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPaper : MonoBehaviour
{

    public static SCPaper instance;
    public GameObject _paperTouchCube;
    private void Awake()
    {
        instance = this;

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cube"))
        {

            _paperTouchCube = other.gameObject;
            _paperTouchCube.GetComponent<Tile>().isTherePaper = true;
            _paperTouchCube.GetComponent<Tile>()._touchAnything = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _paperTouchCube.GetComponent<Tile>().isTherePaper = false;
            _paperTouchCube.GetComponent<Tile>()._touchAnything = false;
        }
    }

}
