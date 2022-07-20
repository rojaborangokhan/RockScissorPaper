using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial, _offsetMaterial;

    public bool isThereObject = false;
    private List<GameObject> nearCube;
    public static Tile instance;


    private void Awake()
    {
        instance = this;
        nearCube = new List<GameObject>();
    }

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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Cube"))
        {
            nearCube.Add(other.gameObject);
            
            if (isThereObject)
            {
                for (int i = 0; i < nearCube.Count; i++)
                {
                    Debug.Log(nearCube[i]);
                }
            }
        }
    }
    
    
    
    
}
