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
                var spawnedTile = Instantiate(_cube, new Vector3(j,0, i), Quaternion.identity);
                spawnedTile.name = $"Tile {j} {i}";
                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                Debug.Log(isOffset);
                _tile.InIt(isOffset);
            }
        }
    }
    
    
}
