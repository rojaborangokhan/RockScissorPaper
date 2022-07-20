using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//[CreateAssetMenu(menuName = "Scriptable object/Board/Layout")]
public class BoardCreation : MonoBehaviour//ScriptableObject
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
    private Vector3 newVector;
    private void Awake()
    {
        instance = this;
        GenerateGrid(_boardWidth, _boardHeight);
        newVector = new Vector3(3, 5, 5);
    }

    private void GenerateGrid(int boardWidth, int boardHeight)
    {
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                spawnedTile = Instantiate(_cube, new Vector3(j,0, i), Quaternion.identity);
                spawnedTile.name = $"{j} {i}";
                if ((i ==0 && j <= boardWidth-1) || (j<=boardWidth-1 && i==boardHeight-1))
                {
                    // ilk sıraya buradan ulaşıyoruz. Bütün taşlar ilk sıradan çıktıktan sonra bu sıra kullanılamayacak.
                    if (i == 0 && j == 0)
                    {
                        Vector3 pos = new Vector3(spawnedTile.transform.position.x,
                            1f,
                            spawnedTile.transform.position.z);
                        Instantiate(_rock,pos,quaternion.identity);
                    }
                    if (i == 0 && j == 1)
                    {
                        Vector3 pos = new Vector3(spawnedTile.transform.position.x,
                            1.5f,
                            spawnedTile.transform.position.z);
                        Instantiate(_paper,pos,quaternion.identity);
                    }
                    if (i == 0 && j == 2)
                    {
                        Vector3 pos = new Vector3(spawnedTile.transform.position.x,
                            1.5f,
                            spawnedTile.transform.position.z);
                        Instantiate(_scissor,pos,quaternion.identity);
                    }

                }
                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                _tile.InIt(isOffset);
                
            }
        }
        
    }

    private void SelectObject()
    {
        
    }

    public void PossibleMoves()
    {
        ColliderInputReceiver.instance._clickedObject.transform.position = new Vector3(3, 2, 5);
    }
    
    
}
