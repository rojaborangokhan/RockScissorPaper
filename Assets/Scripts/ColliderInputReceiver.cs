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
    public int _clickNumber = 1;
    public List<GameObject> nearCubes;
    private bool _isRockChosen, _isPaperChosen, _isScissorChosen;

    private void Awake()
    {
        instance = this;
        nearCubes = new List<GameObject>();
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
                    
                    if (hit.transform.gameObject.tag == "Rock" && _clickNumber == 1)
                    {
                        nearCubes.Clear();
                        _isRockChosen = true;
                        _isPaperChosen = false;
                        _isScissorChosen = false;
                        _clickNumber++;
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        } 
                        _clickedObject = hit.transform.gameObject;
                        GameObject _clickBoard = SCRock.instance._rockTouchCube;
                        int listCountRock = _clickBoard.GetComponent<Tile>().neighObjects.Count;
                        if (_clickBoard.GetComponent<Tile>().isThereRock)
                        {
                            for (int i = 0; i < listCountRock; i++)
                            {
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isTherePaper && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereScissor)
                                {
                                    nearCubes.Add(_clickBoard.GetComponent<Tile>().neighObjects[i]);
                                }
                            }
                            for (int i = 0; i < nearCubes.Count; i++)
                            {
                                nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }

                        }
                    }
                    else if (hit.transform.gameObject.tag == "Paper" && _clickNumber == 1)
                    {
                        nearCubes.Clear();
                        _isPaperChosen = true;
                        _isRockChosen = false;
                        _isScissorChosen = false;
                        _clickNumber++;
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        }
                        _clickedObject = hit.transform.gameObject;
                        GameObject _clickBoard = SCPaper.instance._paperTouchCube;
                        int listCountPaper = _clickBoard.GetComponent<Tile>().neighObjects.Count;
                        if (_clickBoard.GetComponent<Tile>().isTherePaper)
                        {
                            for (int i = 0; i < listCountPaper; i++)
                            {
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereRock && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereScissor)
                                {
                                    nearCubes.Add(_clickBoard.GetComponent<Tile>().neighObjects[i]);
                                }
                            }
                            for (int i = 0; i < nearCubes.Count; i++)
                            {
                                nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }
                        }
                    }
                    else if (hit.transform.gameObject.tag == "Scissor"  && _clickNumber == 1)
                    {
                        nearCubes.Clear();
                        _isScissorChosen = true;
                        _isPaperChosen = false;
                        _isRockChosen = false;
                        _clickNumber++;
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        }
                        _clickedObject = hit.transform.gameObject;
                        GameObject _clickBoard = SCScrissor.instance._scissorTouchCube;
                        int listCountScissor = _clickBoard.GetComponent<Tile>().neighObjects.Count;
                        if (_clickBoard.GetComponent<Tile>().isThereScissor)
                        {
                            for (int i = 0; i < listCountScissor; i++)
                            {
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isTherePaper && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereRock)
                                {
                                    nearCubes.Add(_clickBoard.GetComponent<Tile>().neighObjects[i]);
                                }
                            }
                            for (int i = 0; i < nearCubes.Count; i++)
                            {
                                nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }
                        }
                    }
                    else if ((hit.transform.gameObject.tag != "Scissor" || hit.transform.gameObject.tag != "Paper" || hit.transform.gameObject.tag != "Rock") && _clickNumber == 1)
                    {
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) { 
                            a.GetComponent<Tile>().Sample();
                        }

                        _isPaperChosen = false;
                        _isRockChosen = false;
                        _isScissorChosen = false;
                    }
                    else if (_isPaperChosen || _isRockChosen || _isScissorChosen)
                    {
                        for (int i = 0; i < nearCubes.Count; i++)
                        {
                            if (hit.transform.gameObject == nearCubes[i])
                            {
                                Vector3 _targetPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                                _clickedObject.transform.position = Vector3.MoveTowards(_clickedObject.transform.position,
                                    _targetPos, 500f);
                            }
                        }
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        }
                        _clickNumber--;
                    }
                } 
            }
        }
    }
    
}
