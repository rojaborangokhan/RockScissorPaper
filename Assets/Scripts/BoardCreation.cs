using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Scriptable object/Board/Layout")]
public class BoardCreation : MonoBehaviour//ScriptableObject
{
    [SerializeField] private int _boardWidth, _boardHeight;
    [SerializeField] private GameObject _cube;
    [SerializeField] private Tile _tile;
    private GameObject spawnedTile;
    [SerializeField] private GameObject _rock;
    [SerializeField] private GameObject _paper;
    [SerializeField] private GameObject _scissor;
    private void Awake()
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
                if ((i ==0 && j <= boardWidth-1) || (j<=boardWidth-1 && i==boardHeight-1))
                {
                    Debug.Log(spawnedTile.name);
                    // ilk sıraya buradan ulaşıyoruz. Bütün taşlar ilk sıradan çıktıktan sonra bu sıra kullanılamayacak.
                    
                }
                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                _tile.InIt(isOffset);
                
            }
        }
        
    }
    
    
}
