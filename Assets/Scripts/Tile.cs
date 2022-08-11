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
    [SerializeField] private Material[] forbiddenMaterials;
    public bool _canRockTouch = true;
    public bool _canPaperTouch = true;
    public bool _canScissorTouch = true;
    public bool _touchAnything = false;

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

    public void DefineForbiddenCubes()
    {
        int randomNum = UnityEngine.Random.Range(0,6);
        switch (randomNum)
        {
            case 0:
                
                for (int i = 0; i < forbiddenMaterials.Length; i++)
                {
                    if (i == 0)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[i];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._canRockTouch = false;
                    }

                    else if (i == 1)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[i];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[1].GetComponent<Tile>()._canPaperTouch = false;
                    }

                    else if (i == 2)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[i];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[2].GetComponent<Tile>()._canScissorTouch = false;
                    }
                    
                }
                break;
            
            case 1:
                
                for (int i = 0; i < forbiddenMaterials.Length; i++)
                {
                    if (i == 0)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[0];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._canRockTouch = false;
                    }

                    else if (i == 1)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[2];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[1].GetComponent<Tile>()._canScissorTouch = false;
                    }

                    else if (i == 2)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[1];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[2].GetComponent<Tile>()._canPaperTouch = false;
                    }
                }
                break;
            case 2:
                
                for (int i = 0; i < forbiddenMaterials.Length; i++)
                {
                    if (i == 0)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[1];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._canPaperTouch = false;
                    }

                    else if (i == 1)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[0];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[1].GetComponent<Tile>()._canRockTouch = false;
                    }

                    else if (i == 2)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[2];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[2].GetComponent<Tile>()._canScissorTouch = false;
                    }
                }
                break;
            
            case 3:
                
                for (int i = 0; i < forbiddenMaterials.Length; i++)
                {
                    if (i == 0)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[1];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._canPaperTouch = false;
                    }

                    else if (i == 1)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[2];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[1].GetComponent<Tile>()._canScissorTouch = false;
                    }

                    else if (i == 2)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[0];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[2].GetComponent<Tile>()._canRockTouch = false;
                    }
                }

                break;
            
            case 4:
                
                for (int i = 0; i < forbiddenMaterials.Length; i++)
                {
                    if (i == 0)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[2];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._canScissorTouch = false;
                    }

                    else if (i == 1)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[1];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[1].GetComponent<Tile>()._canPaperTouch = false;
                    }

                    else if (i == 2)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[0];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[2].GetComponent<Tile>()._canRockTouch = false;
                    }
                    
                }
                break;
            
            case 5:
                
                for (int i = 0; i < forbiddenMaterials.Length; i++)
                {
                    if (i == 0)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[2];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._canScissorTouch = false;
                    }

                    else if (i == 1)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;

                        x[0] = forbiddenMaterials[0];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[1].GetComponent<Tile>()._canRockTouch = false;
                    }

                    else if (i == 2)
                    {
                        
                        var x =  BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().firstMaterials;
                        x[0] = forbiddenMaterials[1];
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials = x;
                        BoardCreation.instance.forbiddenCubes[i].GetComponent<Tile>()._currentMaterial = BoardCreation
                            .instance.forbiddenCubes[i].GetComponent<Tile>().mrender.materials;
                        BoardCreation.instance.forbiddenCubes[2].GetComponent<Tile>()._canPaperTouch = false;
                    }
                    
                }
                break;
        }
    }

    
}