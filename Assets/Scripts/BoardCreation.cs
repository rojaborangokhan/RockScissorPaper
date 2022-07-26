using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BoardCreation : MonoBehaviour
{
    public static BoardCreation instance;
    [SerializeField] private int _boardWidth, _boardHeight;
    [SerializeField] private GameObject _cube;
    [SerializeField] private Tile _tile;
    private GameObject spawnedTile;
    [SerializeField] private GameObject _rock;
    [SerializeField] private GameObject _paper;
    [SerializeField] private GameObject _scissor;
    private ColliderInputReceiver _colliderInputReceiver;
    private int a, b;
    [SerializeField] private GameObject _destroyRock, _destroyScissor, _destroyPaper, instantiateRock, _instantiatePaper, _instantiateScissor;
    private bool _isRockLocated = false;
    private bool _isPaperLocated = false;
    private bool _isScisLocated = false;
    private Vector3 _destroyRockPos, _destroyPaperPos, _destroyScissorPos;

    private void Awake()
    {
        instance = this;
        _destroyRockPos = _destroyRock.transform.position;
        _destroyPaperPos = _destroyPaper.transform.position;
        _destroyScissorPos = _destroyScissor.transform.position;
    }

    private void OnEnable()
    {
        GenerateGrid(_boardWidth, _boardHeight);
    }

    void Update()
    {
        if (!_isRockLocated)
        {
            SpawningRock();
        }
        else if (!_isPaperLocated)
        {
            SpawningPaper();
        }
        else if (!_isScisLocated)
        {
            SpawningScissor();
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
                if ((hit.transform.tag == "Cube") && (hit.transform.name == "0 0" || hit.transform.name == "1 0" || hit.transform.name == "2 0"))
                {
                    Vector3 _scissorPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                    _instantiateScissor =  Instantiate(_scissor, _scissorPos, Quaternion.identity);
                    hit.transform.GetComponent<Tile>().isThereScissor = true;
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
                if ((hit.transform.tag == "Cube") && (hit.transform.name == "0 0" || hit.transform.name == "1 0" || hit.transform.name == "2 0"))
                {
                    Vector3 _paperPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                    _instantiatePaper = Instantiate(_paper, _paperPos, Quaternion.identity);
                    hit.transform.GetComponent<Tile>().isTherePaper = true;
                    _destroyPaper.SetActive(false);
                    _isPaperLocated = true;
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
                spawnedTile.name = $"{j} {i}";
                
                if (i == 7 && j == 1)
                {
                    
                }
                if ((i ==0 && j <= boardWidth-1) || (j<=boardWidth-1 && i==boardHeight-1))
                {
                    // ilk sıraya buradan ulaşıyoruz. Bütün taşlar ilk sıradan çıktıktan sonra bu sıra kullanılamayacak.
                    
                }
                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                Tile.instance.InIt(isOffset);
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
                if ((hit.transform.tag == "Cube" )&& (hit.transform.name == "0 0" || hit.transform.name == "1 0" || hit.transform.name == "2 0"))
                {
                    Vector3 _rockPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                    instantiateRock = Instantiate(_rock,_rockPos, Quaternion.identity);
                    hit.transform.GetComponent<Tile>().isThereRock = true;
                    _destroyRock.SetActive(false);
                    _isRockLocated = true;
                }
            }
        }
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
    }



    
    
    
}
