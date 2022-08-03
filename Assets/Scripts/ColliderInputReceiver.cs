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
    public bool _objectSpawned = false;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (_objectSpawned)
        {
            ClickToObject();
        }
        
    }

    private void ClickToObject()
    {
        if (_objectSpawned)
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
                        GameObject _clickBoard = SCRock.instance._rockTouchCube;
                        int listCountRock = _clickBoard.GetComponent<Tile>().neighObjects.Count;
                        if (_clickBoard.GetComponent<Tile>().isThereRock)
                        {
                            List<GameObject> _selamlar = new List<GameObject>();
                            for (int i = 0; i < listCountRock; i++)
                            {
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isTherePaper && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereScissor)
                                {
                                    _selamlar.Add(_clickBoard.GetComponent<Tile>().neighObjects[i]);
                                }
                            }

                            for (int i = 0; i < _selamlar.Count; i++)
                            {
                                _selamlar[i].GetComponent<Renderer>().material = _clickedMaterial;
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
                        GameObject _clickBoard = SCPaper.instance._paperTouchCube;
                        int listCountPaper = _clickBoard.GetComponent<Tile>().neighObjects.Count;
                        if (_clickBoard.GetComponent<Tile>().isTherePaper)
                        {
                            List<GameObject> _selamlar = new List<GameObject>();
                            for (int i = 0; i < listCountPaper; i++)
                            {
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereRock && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereScissor)
                                {
                                    _selamlar.Add(_clickBoard.GetComponent<Tile>().neighObjects[i]);
                                }
                            }

                            for (int i = 0; i < _selamlar.Count; i++)
                            {
                                _selamlar[i].GetComponent<Renderer>().material = _clickedMaterial;
                                RaycastHit newHit = default;
                                Ray raynew = Camera.main.ScreenPointToRay(Input.mousePosition);
                                if ( _selamlar[i] = newHit.transform.gameObject)
                                {
                                    
                                }
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
                        GameObject _clickBoard = SCScrissor.instance._scissorTouchCube;
                        int listCountScissor = _clickBoard.GetComponent<Tile>().neighObjects.Count;
                        if (_clickBoard.GetComponent<Tile>().isThereScissor)
                        {
                            List<GameObject> _selamlar = new List<GameObject>();
                            for (int i = 0; i < listCountScissor; i++)
                            {
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isTherePaper && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereRock)
                                {
                                    _selamlar.Add(_clickBoard.GetComponent<Tile>().neighObjects[i]);
                                }
                            }

                            for (int i = 0; i < _selamlar.Count; i++)
                            {
                                _selamlar[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }
                            
                            
                        }
                    }
                    else if (hit.transform.gameObject.tag != "Scissor" || hit.transform.gameObject.tag != "Paper" || hit.transform.gameObject.tag != "Rock" || hit.transform == null)
                    {
                        
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        }     
                        
                    } 
                } 
            }
        }
    }

    public void MoveObjects()
    {
        
    }
    
    
    
    
}
