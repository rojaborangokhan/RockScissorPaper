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
    public static Tile instance;
    public Material[] firstMaterials;
    public Material[] changingMaterials;
    public MeshRenderer mrender;
    private Material[] _currentMaterial;
    [SerializeField] private GameObject _currentObject;
    private GameObject _currentGameObject;
    private Collider[] colliderList;
    public List<GameObject> neighObjects;


    private void Awake()
    {
        instance = this;
        mrender = this.gameObject.transform.GetComponent<MeshRenderer>();
        firstMaterials = mrender.materials;
        colliderList = new Collider[] { };
    }

    private void Start()
    {
        colliderList = Physics.OverlapSphere(transform.position, 0.6f);
        for (int i = 0; i < colliderList.Length; i++)
        {
            GameObject hitCollider = colliderList[i].gameObject;
            
            if (hitCollider.CompareTag("Cube") && !(hitCollider == this.gameObject))
            {
                neighObjects.Add(hitCollider);
            }
        }
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
        }
        else
        {
            var x = firstMaterials;
            x[0] = changingMaterials[1];
            mrender.materials = x;
            _currentMaterial = mrender.materials;
        }
    }
    
}
