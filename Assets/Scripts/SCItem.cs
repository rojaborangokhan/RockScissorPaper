using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCItem : MonoBehaviour
{

    private List<GameObject> boardCube;
    public static SCItem instance;

    private void Awake()
    {
        instance = this;
        boardCube = new List<GameObject>();
    }

    public void MoveToPos(Transform transform, Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            boardCube.Add(other.gameObject);
            for (int i = 0; i < boardCube.Count; i++)
            {
                boardCube[i].GetComponent<Tile>().isThereObject = true;
            }
            
        }
    }
}
