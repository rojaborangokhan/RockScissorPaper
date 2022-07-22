using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class ColliderInputReceiver : MonoBehaviour
{
    private Vector3 clickPosition;
    public GameObject _clickedObject;
    public static ColliderInputReceiver instance;
    [SerializeField] private Material _clickedMaterial;
    [SerializeField] private GameObject _cube, _rock, _paper, _scissor;
    private GameObject _chosenObject;
    private Material _prevMat;
    private Object[] allCubes;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        ClickToObject();
    }

    private void ClickToObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                
                    if (hit.transform.gameObject.tag == "Rock")
                    {
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        } 
                        _chosenObject = _rock;
                        clickPosition = hit.point;
                        _clickedObject = hit.transform.gameObject;
                        int listCountRock = SCRock.instance._rockTouchCube.GetComponent<Tile>().nearCubeRock.Count;
                        
                        if (SCRock.instance._rockTouchCube.GetComponent<Tile>().isThereRock)
                        {
                            for (int i = 0; i < listCountRock; i++)
                            {
                                
                                SCRock.instance._rockTouchCube.GetComponent<Tile>().nearCubeRock[i].GetComponent<MeshRenderer>().material = _clickedMaterial;
                            }
                        }
                    }
                    else if (hit.transform.gameObject.tag == "Paper")
                    {
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        } 
                        _chosenObject = _paper;
                        clickPosition = hit.point;
                        _clickedObject = hit.transform.gameObject;
                        int listCountPaper = SCPaper.instance._paperTouchCube.GetComponent<Tile>().nearCubePaper.Count;
                        if (SCPaper.instance._paperTouchCube.GetComponent<Tile>().isTherePaper)
                        {
                            
                            for (int i = 0; i < listCountPaper; i++)
                            {
                                
                                SCPaper.instance._paperTouchCube.GetComponent<Tile>().nearCubePaper[i].GetComponent<Renderer>().material = _clickedMaterial;
                                
                            }
                        }
                    }
                    else if (hit.transform.gameObject.tag == "Scissor")
                    {
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        } 
                        _chosenObject = _scissor;
                        clickPosition = hit.point;
                        _clickedObject = hit.transform.gameObject;
                        int listCountScissor = SCScrissor.instance._scissorTouchCube.GetComponent<Tile>().nearCubeScissor.Count;
                        
                        if (SCScrissor.instance._scissorTouchCube.GetComponent<Tile>().isThereScissor)
                        {
                            for (int i = 0; i < listCountScissor; i++)
                            {
                                
                                SCScrissor.instance._scissorTouchCube.GetComponent<Tile>().nearCubeScissor[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }
                        }
                    }
                    else if (hit.transform.gameObject.tag != "Scissor" || hit.transform.gameObject.tag != "Paper" || hit.transform.gameObject.tag != "Rock")
                    {
                        Debug.Log("BASKAYER");
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        }     
                        
                    }
            }
        }
    }
    
    
    
    
}
