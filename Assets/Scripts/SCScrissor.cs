using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCScrissor : MonoBehaviour
{
    public List<GameObject> boardCube;
    public GameObject _scissorTouchCube;
    public static SCScrissor instance;
    private void Awake()
    {
        instance = this;
        boardCube = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            StartCoroutine(LateBeginning());
            boardCube.Add(other.gameObject);
            _scissorTouchCube = other.gameObject;
            //Debug.Log(_scissorTouchCube);
            for (int i = 0; i < boardCube.Count; i++)
            {
                boardCube[i].GetComponent<Tile>().isThereScissor = true;
            }
            
        }
    }

    private IEnumerator LateBeginning()
    {
        yield return new WaitForSecondsRealtime(5f*Time.deltaTime);
    }

}
