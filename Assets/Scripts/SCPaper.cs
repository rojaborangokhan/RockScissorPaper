using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPaper : MonoBehaviour
{
    public List<GameObject> boardCube;
    public static SCPaper instance;
    public GameObject _paperTouchCube;
    private void Awake()
    {
        instance = this;
        boardCube = new List<GameObject>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            boardCube.Add(other.gameObject);
            _paperTouchCube = other.gameObject;
            for (int i = 0; i < boardCube.Count; i++)
            {
                boardCube[i].GetComponent<Tile>().isTherePaper = true;
            }
            
        }
    }
}
