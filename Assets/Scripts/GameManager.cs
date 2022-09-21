using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerOneTurn = true;
    public bool playerTwoTurn = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void TurnBetweenPlayers()
    {
        playerOneTurn = !playerOneTurn;
        playerTwoTurn = !playerTwoTurn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
