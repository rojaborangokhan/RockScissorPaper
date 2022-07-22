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
    public bool isOffset;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        
        GenerateGrid(_boardWidth, _boardHeight);
        
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
                    
                    Vector3 pos = new Vector3(j,
                        1f,
                        i);
                    Instantiate(_rock,pos,quaternion.identity);
                }
                if ((i ==0 && j <= boardWidth-1) || (j<=boardWidth-1 && i==boardHeight-1))
                {
                    // ilk sıraya buradan ulaşıyoruz. Bütün taşlar ilk sıradan çıktıktan sonra bu sıra kullanılamayacak.
                    
                    if (i == 0 && j == 1)
                    {
                        Vector3 pos = new Vector3(j,
                            1.5f,
                            i);
                        Instantiate(_paper,pos,quaternion.identity);
                    }
                    if (i == 0 && j == 2)
                    {
                        Vector3 pos = new Vector3(j,
                            1.5f,
                            i);
                        Instantiate(_scissor,pos,quaternion.identity);
                    }

                }
                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                Tile.instance.InIt(isOffset);
            }
            
        }

    }

    
    
    
}
