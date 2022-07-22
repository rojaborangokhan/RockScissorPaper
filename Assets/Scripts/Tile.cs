using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    public bool isThereRock = false;
    public bool isTherePaper = false;
    public bool isThereScissor = false;
    public List<GameObject> nearCubePaper;
    public List<GameObject> nearCubeScissor;
    public List<GameObject> nearCubeRock;
    public static Tile instance;
    public Material[] firstMaterials;
    public Material[] changingMaterials;
    public MeshRenderer mrender;
    private Material[] _currentMaterial;
    

    private void Awake()
    {
        instance = this;
        mrender = this.gameObject.transform.GetComponent<MeshRenderer>();
        firstMaterials = mrender.materials;
        nearCubePaper = new List<GameObject>();
        nearCubeRock = new List<GameObject>();
        nearCubeScissor = new List<GameObject>();
    }

    public void Sample()
    {
        mrender.materials = _currentMaterial;
    }
    public void InIt(bool isOffset)
    {
        if (isOffset)
        {
            var x = firstMaterials;
            x[0] = changingMaterials[0];
            mrender.materials = x;
            _currentMaterial = mrender.materials;
        }else
        {
            var x = firstMaterials;
            x[0] = changingMaterials[1];
            mrender.materials = x;
            _currentMaterial = mrender.materials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MakeNearCubes(other));

    }

    public IEnumerator MakeNearCubes(Collider other)
    {
        yield return new WaitForSecondsRealtime(20f * Time.deltaTime);
        
        if (other.CompareTag("Cube") && isTherePaper && !other.gameObject.GetComponent<Tile>().isThereRock && !other.gameObject.GetComponent<Tile>().isThereScissor)
        {
            nearCubePaper.Add(other.gameObject);
        }
        else if (other.CompareTag("Cube") && isThereRock && !other.gameObject.GetComponent<Tile>().isTherePaper && !other.gameObject.GetComponent<Tile>().isThereScissor)
        {
            nearCubeRock.Add(other.gameObject);
            
        }
        else if (other.CompareTag("Cube") && isThereScissor && !other.gameObject.GetComponent<Tile>().isThereRock && !other.gameObject.GetComponent<Tile>().isTherePaper)
        {
            nearCubeScissor.Add(other.gameObject);
        }
    }
    
    
    
    
}
