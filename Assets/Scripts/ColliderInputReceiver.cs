using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    private bool _willDestroy = true;
    [SerializeField] private Button _resBut;
    private bool _canGoBack = true;
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

    // ReSharper disable Unity.PerformanceAnalysis
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
                    if (_canGoBack)
                    {
                        for (int j = 0; j < BoardCreation.instance._destroyNearObjects.Count; j++)
                        {
                            BoardCreation.instance._destroyNearObjects[j].GetComponent<Tile>().neighObjects.RemoveAt(BoardCreation.instance._destroyNearObjects[j].GetComponent<Tile>().neighObjects.Count-1);
                        }

                        _canGoBack = false;
                    }
                    _canGoBack = false;
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
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isTherePaper 
                                    && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereScissor 
                                    && _clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>()._canRockTouch)
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
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereRock &&
                                    !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereScissor &&
                                    _clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>()._canPaperTouch)
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
                                if (!_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isTherePaper 
                                    && !_clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>().isThereRock
                                    && _clickBoard.GetComponent<Tile>().neighObjects[i].GetComponent<Tile>()._canScissorTouch)
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
                        if (_resBut !=null)
                        {
                            Destroy(_resBut.gameObject);
                        }
                        
                        for (int i = 0; i < nearCubes.Count; i++)
                        {
                            if (hit.transform.gameObject == nearCubes[i])
                            {
                                Vector3 _targetPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                                //_clickedObject.transform.position = Vector3.MoveTowards(_clickedObject.transform.position,_targetPos,4f);
                                _clickedObject.transform.position = Vector3.Lerp(_clickedObject.transform.position,
                                    _targetPos, 500f*Time.deltaTime);
                            }
                        }
                        allCubes = FindObjectsOfType(typeof(Tile));
                        foreach( var a in allCubes) {             
                            a.GetComponent<Tile>().Sample();      
                        }
                        _clickNumber--;
                    }

                    if (_willDestroy)
                    {
                        DestroyItself(); 
                    }
                    
                } 
            }
        }
    }
    
    
    public void DestroyItself()
    {
        if (BoardCreation.instance.destroyedCubes != null)
        {
            int destroyNum = 0;
            for (int i = 0; i < BoardCreation.instance.destroyedCubes.Count; i++)
            {
                
                if (!BoardCreation.instance.destroyedCubes[i].GetComponent<Tile>()._touchAnything)
                {
                    destroyNum++;
                }

                if (destroyNum == 3)
                {
                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                    {
                        Destroy(BoardCreation.instance.destroyedCubes[j]);
                    }
                    _willDestroy = false;
                    
                }
                
            }
            
        }

        

        

    }
}
