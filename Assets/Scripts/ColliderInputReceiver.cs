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
    private bool _canGoBack = false;
    private int click;
    private bool isRockMid = false;
    private bool isPaperMid = false;
    private bool isScissorMid = false;
    private void Awake()
    {
        instance = this;
        nearCubes = new List<GameObject>();
    }

    void Update()
    {
        if (_objectSpawned)
        {
            // herhangi bir objeye dokunulduğunda burası tetikleniyor. Oyunun ana noktası burasıdır.
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
                    

                    click = 0;
                    
                    Tile _clickCubeRock = SCRock.instance._rockTouchCube.GetComponent<Tile>();
                    Tile _clickCubePaper = SCPaper.instance._paperTouchCube.GetComponent<Tile>();
                    Tile _clickCubeScissor = SCScrissor.instance._scissorTouchCube.GetComponent<Tile>();
                    int rockNeigh = _clickCubeRock.neighObjects.Count;
                    int paperNeigh = _clickCubePaper.neighObjects.Count;
                    int scissorNeigh = _clickCubeScissor.neighObjects.Count;
                    int midObjRock = 0;
                    int midObjPaper = 0;
                    int midObjScissor = 0;
                    
                    
                    if (rockNeigh>=3 && (_clickCubeRock.isItInMiddle || _clickCubeRock.isItInRight) && (_clickCubeRock.neighObjects[1].GetComponent<Tile>().isTherePaper || _clickCubeRock.neighObjects[1].GetComponent<Tile>().isThereScissor))
                    {
                        midObjRock++;
                    }
                    else if (rockNeigh>=3 && _clickCubeRock.isItInLeft && (_clickCubeRock.neighObjects[0].GetComponent<Tile>().isTherePaper || _clickCubeRock.neighObjects[0].GetComponent<Tile>().isThereScissor))
                    {
                        
                    }
                    if ( rockNeigh>=3 && (_clickCubeRock.isItInMiddle || _clickCubeRock.isItInRight) && (_clickCubeRock.neighObjects[2].GetComponent<Tile>().isTherePaper || _clickCubeRock.neighObjects[2].GetComponent<Tile>().isThereScissor))
                    {
                        midObjRock++;
                    }
                    else if ( rockNeigh>=3 && _clickCubeRock.isItInLeft && (_clickCubeRock.neighObjects[1].GetComponent<Tile>().isTherePaper || _clickCubeRock.neighObjects[1].GetComponent<Tile>().isThereScissor))
                    {
                        
                    }

                    if (midObjRock >= 2)
                    {
                        isRockMid = true;
                    }


                    if (paperNeigh>=3 && (_clickCubePaper.isItInMiddle || _clickCubePaper.isItInRight) &&( _clickCubePaper.neighObjects[1].GetComponent<Tile>().isThereRock || _clickCubePaper.neighObjects[1].GetComponent<Tile>().isThereScissor))
                    {
                        midObjPaper++;
                    }
                                
                    if (paperNeigh>=3 && (_clickCubePaper.isItInMiddle || _clickCubePaper.isItInRight) && (_clickCubePaper.neighObjects[2].GetComponent<Tile>().isThereRock || _clickCubePaper.neighObjects[2].GetComponent<Tile>().isThereScissor))
                    {
                        midObjPaper++;
                    }
                                
                    if (midObjPaper>=2)
                    {
                        isPaperMid = true;
                    }
                    
                    
                   
                    if (scissorNeigh>=3 && (_clickCubeScissor.isItInMiddle || _clickCubeScissor.isItInRight)  &&( _clickCubeScissor.neighObjects[1].GetComponent<Tile>().isTherePaper || _clickCubeScissor.neighObjects[1].GetComponent<Tile>().isThereRock))
                    {
                        midObjScissor++;
                    }
                                
                    if (scissorNeigh>=3 && (_clickCubeScissor.isItInMiddle || _clickCubeScissor.isItInRight) && (_clickCubeScissor.neighObjects[2].GetComponent<Tile>().isTherePaper || _clickCubeScissor.neighObjects[2].GetComponent<Tile>().isThereRock))
                    {
                        midObjScissor++;
                    }
                                
                    if (midObjScissor>=2)
                    {
                        isScissorMid = true;
                    }
                    
                    
                    // Taşa dokunursa buraya girecek
                    if (hit.transform.gameObject.tag == "Rock" && _clickNumber == 1)
                    {
                        
                        // hangi objenin seçildiğine göre komşuları göstermeye yarıyor.
                        ClickSpecifications(false,true,false, true);
                        // clicked objeyi taş-kağıt-makas üçlüsünden hangisine tıkladıysak ona eşitliyoruz.
                        _clickedObject = hit.transform.gameObject;
                        // taşın dokunduğu objeyi click cube adında bir objeye eşitliyoruz.
                        GameObject _denemeCube = SCRock.instance._rockTouchCube;
                        Tile _clickCube= SCRock.instance._rockTouchCube.GetComponent<Tile>();
                        int listCountRock = _clickCube.neighObjects.Count;
                        if (_clickCube.isThereRock)
                        {
                            
                            for (int i = 0; i < listCountRock; i++)
                            {
                                
                                GameObject _neighOfClickCube = _clickCube.neighObjects[i];
                                Tile _neighOfClickCubeTileScript = _clickCube.neighObjects[i].GetComponent<Tile>();
                                // tıklanan objenin komşularında kağıt ya da makas var mı ve taş objesinin dokunabileceği bir obje olup olmadığını kontrol ediyor
                                if (!_neighOfClickCubeTileScript.isTherePaper 
                                    && !_neighOfClickCubeTileScript.isThereScissor 
                                    && _neighOfClickCubeTileScript._canRockTouch)
                                {
                                    // nearCubes listemize gidebileceğimiz komşuları ekliyoruz
                                    nearCubes.Add(_neighOfClickCube);
                                    
                                }

                                if (listCountRock >3 && _clickCube.isItInMiddle && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor) && !isRockMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && (!_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock) && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canRockTouch )
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && (!_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock) && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                
                                if (listCountRock <=3 && _clickCube.isItInMiddle && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor) && !isRockMid)
                                {
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _denemeCube == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canRockTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                        
                                        if (_denemeCube == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canRockTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                        }
                                    }
                                }
                                
                                if (listCountRock>2 && _clickCube.isItInRight && _neighOfClickCubeTileScript.isItInRight && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                if (listCountRock>2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInLeft && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                }
                                
                                if (listCountRock<=2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInLeft && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _denemeCube == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canRockTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                        }
                                        
                                        if (_denemeCube == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canRockTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                    }
                                }
                                
                                
                                
                                if (listCountRock<=2 &&_clickCube.isItInRight && _neighOfClickCubeTileScript.isItInRight && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _denemeCube == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canRockTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                        
                                        if (_denemeCube == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canRockTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                        }
                                    }
                                }

                                if (listCountRock>2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[3]);
                                    }
                                }
                                if (listCountRock<=2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                
                                if (_clickCube.isItInRight && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isTherePaper || _neighOfClickCubeTileScript.isThereScissor)&& !isRockMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canRockTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                    }
                                }

                                if (isPaperMid && (_clickCube.isItInMiddle || _clickCube.isItInRight))
                                {
                                    if (_denemeCube == _clickCubePaper.GetComponent<Tile>().neighObjects[2])
                                    {
                                        nearCubes.Add(_clickCubeScissor.GetComponent<Tile>().neighObjects[1]);
                                    }

                                    else if (_denemeCube == _clickCubePaper.GetComponent<Tile>().neighObjects[1])
                                    {
                                        nearCubes.Add(_clickCubeScissor.GetComponent<Tile>().neighObjects[2]);
                                    }
                                }

                                if (isScissorMid && (_clickCube.isItInMiddle || _clickCube.isItInRight))
                                {
                                    if (_denemeCube == _clickCubeScissor.GetComponent<Tile>().neighObjects[2])
                                    {
                                        nearCubes.Add(_clickCubePaper.GetComponent<Tile>().neighObjects[1]);
                                    }

                                    else if (_denemeCube == _clickCubeScissor.GetComponent<Tile>().neighObjects[1])
                                    {
                                        nearCubes.Add(_clickCubePaper.GetComponent<Tile>().neighObjects[2]);
                                    }
                                }



                            }
                            for (int i = 0; i < nearCubes.Count; i++)
                            {
                                // gidebileceğimiz komşuların rengini istediğimiz materyalde değiştiriyoruz
                                nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }

                        }
                    }
                    
                    // kağıda dokunursak buraya girecek
                    else if (hit.transform.gameObject.tag == "Paper" && _clickNumber == 1)
                    {
                        ClickSpecifications(true,false,false, true);
                        
                        _clickedObject = hit.transform.gameObject;
                        Tile _clickCube = SCPaper.instance._paperTouchCube.GetComponent<Tile>();
                        GameObject _clickCubeObject = SCPaper.instance._paperTouchCube;
                        int listCountPaper = _clickCube.neighObjects.Count;
                        if (_clickCube.isTherePaper)
                        {
                            for (int i = 0; i < listCountPaper; i++)
                            {
                                GameObject _neighOfClickCube = _clickCube.GetComponent<Tile>().neighObjects[i];
                                Tile _neighOfClickCubeTileScript = _clickCube.GetComponent<Tile>().neighObjects[i]
                                    .GetComponent<Tile>();
                                if (!_neighOfClickCubeTileScript.isThereRock &&
                                    !_neighOfClickCubeTileScript.isThereScissor &&
                                    _neighOfClickCubeTileScript._canPaperTouch)
                                {
                                    nearCubes.Add(_neighOfClickCube);
                                }
                                if (listCountPaper>3 && _clickCube.isItInMiddle && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor) && !isPaperMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch )
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                if (listCountPaper<=3 && _clickCube.isItInMiddle && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor) && !isPaperMid)
                                {
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _clickCubeObject == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                        
                                        if (_clickCubeObject == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canPaperTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                        }
                                    }
                                }

                                if (listCountPaper>2 && _clickCube.isItInRight && _neighOfClickCubeTileScript.isItInRight && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor  && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                if (listCountPaper >2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInLeft && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                }
                                
                                if (listCountPaper <=2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInLeft && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _clickCubeObject == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canPaperTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                        }
                                        
                                        if (_clickCubeObject == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                    }
                                }
                                
                                if (listCountPaper<=2 &&_clickCube.isItInRight && _neighOfClickCubeTileScript.isItInRight && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _clickCubeObject == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                        
                                        if (_clickCubeObject == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canPaperTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                        }
                                    }
                                }
                                
                                if (listCountPaper>2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[3]);
                                    }
                                }
                                
                                if (listCountPaper<=2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                
                                if (_clickCube.isItInRight && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isThereScissor)&& !isPaperMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canPaperTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                    }
                                }
                                
                                if (isRockMid && (_clickCube.isItInMiddle || _clickCube.isItInRight))
                                {
                                    if (_clickCubeObject == _clickCubeRock.GetComponent<Tile>().neighObjects[2])
                                    {
                                        nearCubes.Add(_clickCubeScissor.GetComponent<Tile>().neighObjects[1]);
                                    }

                                    if (_clickCubeObject == _clickCubeRock.GetComponent<Tile>().neighObjects[1])
                                    {
                                        nearCubes.Add(_clickCubeScissor.GetComponent<Tile>().neighObjects[2]);
                                    }
                                }

                                if (isScissorMid && (_clickCube.isItInMiddle || _clickCube.isItInRight))
                                {
                                    if (_clickCubeObject == _clickCubeScissor.GetComponent<Tile>().neighObjects[2])
                                    {
                                        nearCubes.Add(_clickCubeRock.GetComponent<Tile>().neighObjects[1]);
                                    }

                                    if (_clickCubeObject == _clickCubeScissor.GetComponent<Tile>().neighObjects[1])
                                    {
                                        nearCubes.Add(_clickCubeRock.GetComponent<Tile>().neighObjects[2]);
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < nearCubes.Count; i++)
                        {
                            nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                        }
                    }
                    
                    // eğer makası hareket ettireceksek buraya girer
                    else if (hit.transform.gameObject.tag == "Scissor"  && _clickNumber == 1)
                    {
                        ClickSpecifications(false,false,true, true);
                        
                        ReturnAllCubesToOriginalMaterials();
                        _clickedObject = hit.transform.gameObject;
                        Tile _clickCube = SCScrissor.instance._scissorTouchCube.GetComponent<Tile>();
                        GameObject _clickCubeObject = SCScrissor.instance._scissorTouchCube;
                        int listCountScissor = _clickCube.neighObjects.Count;
                        if (_clickCube.isThereScissor)
                        {
                            for (int i = 0; i < listCountScissor; i++)
                            {
                                GameObject _neighOfClickCube = _clickCube.GetComponent<Tile>().neighObjects[i];
                                Tile _neighOfClickCubeTileScript = _clickCube.GetComponent<Tile>().neighObjects[i]
                                    .GetComponent<Tile>();
                                if (!_neighOfClickCubeTileScript.isTherePaper 
                                    && !_neighOfClickCubeTileScript.isThereRock
                                    && _neighOfClickCubeTileScript._canScissorTouch)
                                {
                                    nearCubes.Add(_neighOfClickCube);
                                }
                                if (listCountScissor >3 && _clickCube.isItInMiddle && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper) && !isScissorMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                
                                if (listCountScissor<=3 && _clickCube.isItInMiddle && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _clickCubeObject == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                        
                                        if (_clickCubeObject == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canScissorTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                        }
                                    }
                                }
                                if (listCountScissor>2 &&_clickCube.isItInRight && _neighOfClickCubeTileScript.isItInRight && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                
                                if (listCountScissor<=2 &&_clickCube.isItInRight && _neighOfClickCubeTileScript.isItInRight && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _clickCubeObject == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock &&  !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                        
                                        if (_clickCubeObject == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                        }
                                    }
                                }
                                if (listCountScissor>2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInLeft && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    int clickCubeInt = 0;
                                    int neighCubeInt = 0;
                                    int.TryParse(_clickCube.name, out clickCubeInt);
                                    int.TryParse(_neighOfClickCube.name, out neighCubeInt);
                                    if (clickCubeInt-neighCubeInt >0 && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                    }
                                    else if (clickCubeInt-neighCubeInt <0 && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                    }
                                }
                                
                                if (listCountScissor<=2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInLeft && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    for (int j = 0; j < BoardCreation.instance.destroyedCubes.Count; j++)
                                    {
                                        
                                        if ( _clickCubeObject == BoardCreation.instance.destroyedCubes[j] &&  !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canScissorTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                        }
                                        
                                        if (_clickCubeObject == BoardCreation.instance._destroyNearObjects[j] && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereScissor && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>().isThereRock && _neighOfClickCubeTileScript.neighObjects[1].GetComponent<Tile>()._canScissorTouch)
                                        {
                                            nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[1]);
                                        }
                                    }
                                }
                                
                                
                                if ( listCountScissor>2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[3].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[3]);
                                    }
                                }
                                
                                if ( listCountScissor<=2 && _clickCube.isItInLeft && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    if (!_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[2].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[2]);
                                    }
                                }
                                
                                if (  _clickCube.isItInRight && _neighOfClickCubeTileScript.isItInMiddle && (_neighOfClickCubeTileScript.isThereRock || _neighOfClickCubeTileScript.isTherePaper)&& !isScissorMid)
                                {
                                    
                                    if (!_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereRock && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isTherePaper && !_neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>().isThereScissor && _neighOfClickCubeTileScript.neighObjects[0].GetComponent<Tile>()._canScissorTouch)
                                    {
                                        nearCubes.Add(_neighOfClickCubeTileScript.neighObjects[0]);
                                    }
                                }
                                if (isRockMid && (_clickCube.isItInMiddle || _clickCube.isItInRight))
                                {
                                    if (_clickCubeObject == _clickCubeRock.GetComponent<Tile>().neighObjects[2])
                                    {
                                        nearCubes.Add(_clickCubePaper.GetComponent<Tile>().neighObjects[1]);
                                    }

                                    if (_clickCubeObject == _clickCubeRock.GetComponent<Tile>().neighObjects[1])
                                    {
                                        nearCubes.Add(_clickCubePaper.GetComponent<Tile>().neighObjects[2]);
                                    }
                                }

                                if (isPaperMid && (_clickCube.isItInMiddle || _clickCube.isItInRight))
                                {
                                    if (_clickCubeObject == _clickCubePaper.GetComponent<Tile>().neighObjects[2])
                                    {
                                        nearCubes.Add(_clickCubeRock.GetComponent<Tile>().neighObjects[1]);
                                    }

                                    if (_clickCubeObject == _clickCubePaper.GetComponent<Tile>().neighObjects[1])
                                    {
                                        nearCubes.Add(_clickCubeRock.GetComponent<Tile>().neighObjects[2]);
                                    }
                                }
                            }
                            
                            for (int i = 0; i < nearCubes.Count; i++)
                            {
                                nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }
                        }
                    }
                    // eğer bizim objelerimizden birine dokunmadıysa her şeyi normal haline getirecek.
                    else if ((hit.transform.gameObject.tag != "Scissor" || hit.transform.gameObject.tag != "Paper" || hit.transform.gameObject.tag != "Rock") && _clickNumber == 1)
                    {
                        ClickSpecifications(false,false,false, false);
                    }
                    // eğer herhangi bir obje seçildiyse yani eğer taş-kağıt-makastan birini hareket ettireceksek buraya girecek.
                    else if (_isPaperChosen || _isRockChosen || _isScissorChosen)
                    {
                        // reset butonunun yok edilişi
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
                                isPaperMid = false;
                                isRockMid = false;
                                isScissorMid = false;
                            }
                        }
                        ReturnAllCubesToOriginalMaterials();
                        _clickNumber--;
                    }

                    if (_willDestroy)
                    {
                        
                        DestroyFirstLine();
                        _canGoBack = true;
                    }
                } 
            }
        }
    }
    
    
    public void DestroyFirstLine()
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
        
        // İlk sıra yok olacak ve ilk sıradan çıktığında geri dönmemek için yazıldı burası.
        if (_canGoBack)
        {
            
            List<GameObject> firstLine = BoardCreation.instance._destroyNearObjects;
            for (int j = 0; j < firstLine.Count; j++)
            {
                for (int i = 0; i <firstLine[j].GetComponent<Tile>().neighObjects.Count; i++)
                {
                    if (firstLine[j].GetComponent<Tile>().neighObjects[i] == BoardCreation.instance.destroyedCubes[j])
                    {
                        firstLine[j].GetComponent<Tile>().neighObjects.Remove(firstLine[j].GetComponent<Tile>().neighObjects[i]);
                    }
                }
            }
            _canGoBack = false;
        }

    }

    private void ReturnAllCubesToOriginalMaterials()
    {
        allCubes = FindObjectsOfType(typeof(Tile));
        foreach( var a in allCubes) {             
            a.GetComponent<Tile>().Sample();      
        } 
    }

    private void ClickSpecifications(bool isItPaper, bool isItRock, bool isItScissor, bool _clickNumberIncrease)
    {
        // nearCubes taş-kağıt-makas arasından hangisine dokunduysak onun altındaki kübün komşularını alıyor. Her seferinde temizlemezsek
        // sürekli üstüne ekleniyor ve bug oluşuyor.
        nearCubes.Clear();
        // allCubes tile scriptine sahip tüm objelere ulaşır ve onun içinde bütün küpleri eski materyallerine dönmesini sağlayan fonksiyonu çalıştırır.
        ReturnAllCubesToOriginalMaterials();
        _isPaperChosen = isItPaper;
        _isRockChosen = isItRock;
        _isScissorChosen = isItScissor;
        if (_clickNumberIncrease)
        {
            _clickNumber++;
        }
        
    }
}