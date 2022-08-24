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
                    // Taşa dokunursa buraya girecek
                    if (hit.transform.gameObject.tag == "Rock" && _clickNumber == 1)
                    {
                        // hangi objenin seçildiğine göre komşuları göstermeye yarıyor.
                        ClickSpecifications(false,true,false, true);
                        // clicked objeyi taş-kağıt-makas üçlüsünden hangisine tıkladıysak ona eşitliyoruz.
                        _clickedObject = hit.transform.gameObject;
                        // taşın dokunduğu objeyi click cube adında bir objeye eşitliyoruz.
                        GameObject _clickCube= SCRock.instance._rockTouchCube;
                        int listCountRock = _clickCube.GetComponent<Tile>().neighObjects.Count;
                        if (_clickCube.GetComponent<Tile>().isThereRock)
                        {
                            for (int i = 0; i < listCountRock; i++)
                            {
                                GameObject _neighOfClickCube = _clickCube.GetComponent<Tile>().neighObjects[i];
                                // tıklanan objenin komşularında kağıt ya da makas var mı ve taş objesinin dokunabileceği bir obje olup olmadığını kontrol ediyor
                                if (!_neighOfClickCube.GetComponent<Tile>().isTherePaper 
                                    && !_neighOfClickCube.GetComponent<Tile>().isThereScissor 
                                    && _neighOfClickCube.GetComponent<Tile>()._canRockTouch)
                                {
                                    // nearCubes listemize gidebileceğimiz komşuları ekliyoruz
                                    nearCubes.Add(_neighOfClickCube);
                                    
                                }
                                if ((i == 1 || i == 2) && (_neighOfClickCube.GetComponent<Tile>().isTherePaper || _neighOfClickCube.GetComponent<Tile>().isThereScissor)&& _neighOfClickCube.name != "0 0" && _neighOfClickCube.name != "1 0" && _neighOfClickCube.name != "2 0")
                                {
                                    nearCubes.Add(_neighOfClickCube.GetComponent<Tile>().neighObjects[i]);
                                }
                                // EĞER NEİGHOFCLICKCUBE'UN İLK 2 ELEMENTİ ISTHEREPAPER YA DA ISTHEREROCK ILE DOLUYSA 3'LU ZIPLAMA YAPABİLİRLER.
                                
                                // else if (_neighOfClickCube.GetComponent<Tile>().isTherePaper)
                                // {
                                //     nearCubes.Add(_neighOfClickCube.GetComponent<Tile>().neighObjects[0]);
                                // }
                                // else if (_neighOfClickCube.GetComponent<Tile>().isThereScissor)
                                // {
                                //     nearCubes.Add(_neighOfClickCube.GetComponent<Tile>().neighObjects[0]);
                                // }
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
                        GameObject _clickCube = SCPaper.instance._paperTouchCube;
                        int listCountPaper = _clickCube.GetComponent<Tile>().neighObjects.Count;
                        if (_clickCube.GetComponent<Tile>().isTherePaper)
                        {
                            for (int i = 0; i < listCountPaper; i++)
                            {
                                GameObject _neighOfClickCube = _clickCube.GetComponent<Tile>().neighObjects[i];
                                if (!_neighOfClickCube.GetComponent<Tile>().isThereRock &&
                                    !_neighOfClickCube.GetComponent<Tile>().isThereScissor &&
                                    _neighOfClickCube.GetComponent<Tile>()._canPaperTouch)
                                {
                                    nearCubes.Add(_neighOfClickCube);
                                }
                                if ((i == 1 || i == 2) && (_neighOfClickCube.GetComponent<Tile>().isThereRock || _neighOfClickCube.GetComponent<Tile>().isThereScissor) && _neighOfClickCube.name != "0 0" && _neighOfClickCube.name != "1 0" && _neighOfClickCube.name != "2 0")
                                {
                                    nearCubes.Add(_neighOfClickCube.GetComponent<Tile>().neighObjects[i]);
                                }
                            }
                            for (int i = 0; i < nearCubes.Count; i++)
                            {
                                if (nearCubes[i].GetComponent<Tile>().isTherePaper && nearCubes[i].GetComponent<Tile>().isThereScissor)
                                {
                                    nearCubes.Remove(nearCubes[i]);
                                }
                                nearCubes[i].GetComponent<Renderer>().material = _clickedMaterial;
                            }
                        }
                    }
                    
                    // eğer makası hareket ettireceksek buraya girer
                    else if (hit.transform.gameObject.tag == "Scissor"  && _clickNumber == 1)
                    {
                        ClickSpecifications(false,false,true, true);
                        
                        ReturnAllCubesToOriginalMaterials();
                        _clickedObject = hit.transform.gameObject;
                        GameObject _clickCube = SCScrissor.instance._scissorTouchCube;
                        int listCountScissor = _clickCube.GetComponent<Tile>().neighObjects.Count;
                        if (_clickCube.GetComponent<Tile>().isThereScissor)
                        {
                            for (int i = 0; i < listCountScissor; i++)
                            {
                                GameObject _neighOfClickCube = _clickCube.GetComponent<Tile>().neighObjects[i];
                                if (!_neighOfClickCube.GetComponent<Tile>().isTherePaper 
                                    && !_neighOfClickCube.GetComponent<Tile>().isThereRock
                                    && _neighOfClickCube.GetComponent<Tile>()._canScissorTouch)
                                {
                                    nearCubes.Add(_neighOfClickCube);
                                }

                               
                                if ((i == 1 || i == 2) && (_neighOfClickCube.GetComponent<Tile>().isTherePaper || _neighOfClickCube.GetComponent<Tile>().isThereRock) && _neighOfClickCube.name != "0 0" && _neighOfClickCube.name != "1 0" && _neighOfClickCube.name != "2 0")
                                { 
                                    nearCubes.Add(_neighOfClickCube.GetComponent<Tile>().neighObjects[i]);
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
                        Debug.Log(firstLine[j].GetComponent<Tile>().neighObjects[i].name);
                        firstLine[j].GetComponent<Tile>().neighObjects.Remove(firstLine[j].GetComponent<Tile>().neighObjects[i]);
                    }
                }
            
                // for (int i = 0; i < firstLine[j].GetComponent<Tile>().neighObjects.Count; i++)
                // {
                //     if (firstLine[j].GetComponent<Tile>().neighObjects[i] == null)
                //     {
                //         Debug.Log("oldu");
                //         firstLine[j].GetComponent<Tile>().neighObjects.Remove(firstLine[j].GetComponent<Tile>().neighObjects[i]);
                //     }
                // }
                // ilk sırada olan her kübün içindeki scripte olan komşulara ulaşıp son indextekini kaldırıyoruz çünkü son indexte yer alan objeler 
                // yok olacak olan sıradaki küpleri içeriyor
                            
                //firstLine[j].GetComponent<Tile>().neighObjects.RemoveAt(firstLine[j].GetComponent<Tile>().neighObjects.Count-1);
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