using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class BoardCreation : MonoBehaviour
{
    public static BoardCreation instance;
    [SerializeField] private int _boardWidth, _boardHeight;
    [SerializeField] private GameObject _cube;
    private GameObject spawnedTile;
    [SerializeField] private GameObject _rock;
    [SerializeField] private GameObject _paper;
    [SerializeField] private GameObject _scissor;
    private ColliderInputReceiver _colliderInputReceiver;
    [SerializeField] private GameObject _destroyRock, _destroyScissor, _destroyPaper, instantiateRock, _instantiatePaper, _instantiateScissor;
    private bool _isRockLocated = false;
    private bool _isPaperLocated = false;
    private bool _isScisLocated = false;
    public GameObject _rockClicked, _paperClicked, _scissorClicked;
    private Object[] allCubes;
    public List<GameObject> forbiddenCubes, destroyedCubes, _destroyNearObjects;


    private void Awake()
    {
        instance = this;
        List<GameObject> forbiddenCubes = new List<GameObject>();
        List<GameObject> destroyedCubes = new List<GameObject>();
        List<GameObject> _destroyNearObjects = new List<GameObject>();
        GenerateGrid(_boardWidth, _boardHeight);
    }
    
    void Update()
    {
        if (!_isRockLocated)
        {
            SpawningRock();
        }
        else if (!_isScisLocated)
        {
            SpawningScissor();
        }
        else if (!_isPaperLocated)
        {
            SpawningPaper();
        }

        if (_isRockLocated && _isPaperLocated && _isScisLocated) 
        {
            ColliderInputReceiver.instance._objectSpawned = true;
            
        }
        
        
    }

    private void SpawningScissor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform.tag == "Cube") && (hit.transform.name == "00" || hit.transform.name == "10" || hit.transform.name == "20")&& !hit.transform.GetComponent<Tile>().isThereRock)
                {
                    Vector3 _scissorPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                    _instantiateScissor =  Instantiate(_scissor, _scissorPos, Quaternion.identity);
                    _scissorClicked = hit.transform.gameObject;
                    _destroyScissor.SetActive(false);
                    _isScisLocated = true;
                }
            }
        }
    }

    private void SpawningPaper()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform.tag == "Cube") && (hit.transform.name == "00" || hit.transform.name == "10" || hit.transform.name == "20") && !hit.transform.GetComponent<Tile>().isThereScissor && !hit.transform.GetComponent<Tile>().isThereRock)
                {
                    Vector3 _paperPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                    _instantiatePaper = Instantiate(_paper, _paperPos, Quaternion.identity);
                    _paperClicked = hit.transform.gameObject;
                    _destroyPaper.SetActive(false);
                    _isPaperLocated = true;
                }

            }
        }
    }
    
    private void SpawningRock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform.tag == "Cube" )&& (hit.transform.name == "00" || hit.transform.name == "10" || hit.transform.name == "20"))
                {
                    Vector3 _rockPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                    instantiateRock = Instantiate(_rock,_rockPos, Quaternion.identity);
                    _rockClicked = hit.transform.gameObject;
                    _destroyRock.SetActive(false);
                    _isRockLocated = true;
                }
            }
        }
    }

    private void GenerateGrid(int boardWidth, int boardHeight)
    {
       
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                spawnedTile = Instantiate(_cube, new Vector3(j,0, i), Quaternion.identity);
                // spawnedTile oluşturup adlandırmasını yapıyoruz
                spawnedTile.name = $"{j}{i}";
                // küplerin rengini atıyoruz
                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                Tile.instance.InIt(isOffset);
                if (j == 1)
                {
                    spawnedTile.GetComponent<Tile>().isItInMiddle = true;
                }

                if (j == 0)
                {
                    spawnedTile.GetComponent<Tile>().isItInLeft = true;
                }

                if (j == 2)
                {
                    spawnedTile.GetComponent<Tile>().isItInRight = true;
                }
                // ilk sırayı listeye ekliyoruz
                if ((i == 0) && (j==1 || j == 0 || j ==2) )
                {
                    destroyedCubes.Add(spawnedTile);
                }
                if ((i == 1) && (j==1 || j == 0 || j ==2) )
                {
                    _destroyNearObjects.Add(spawnedTile);
                }
                if ((j == 0 && i == 1) || (j == 2 && i == 1) || (j == 1 && i == 2))
                {
                    forbiddenCubes.Add(spawnedTile);
                }
            }
        }
        Tile.instance.DefineForbiddenCubes();
    }
    
    public void ResetButton()
    {
        _isPaperLocated = false;
        _isRockLocated = false;
        _isScisLocated = false;
        _destroyRock.SetActive(true);
        _destroyPaper.SetActive(true);
        _destroyScissor.SetActive(true);
        
        if (instantiateRock != null)
        {
            Destroy(instantiateRock);
        }

        if (_instantiatePaper != null)
        {
            Destroy(_instantiatePaper);
        }

        if (_instantiateScissor != null)
        {
            Destroy(_instantiateScissor);
        }
        allCubes = FindObjectsOfType(typeof(Tile));
        foreach( var a in allCubes) {
            if (a != null)
            {
                a.GetComponent<Tile>().isTherePaper = false;
                a.GetComponent<Tile>().isThereRock = false;
                a.GetComponent<Tile>().isThereScissor = false;
            }
        } 
    }
    
    
}
