using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial, _offsetMaterial;
    
    public void InIt(bool isOffset)
    {
        
        if (isOffset)
        {
            this.GetComponent<Renderer>().material = _baseMaterial;
        }

        else
        {
            this.GetComponent<Renderer>().material = _offsetMaterial;
        }
    }
}
