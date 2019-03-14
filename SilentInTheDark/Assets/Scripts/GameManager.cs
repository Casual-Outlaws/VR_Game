﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    InMenu,
    InTutorial,
    InPlaying,
    LostGame,
    WonGame,
};

public class GameManager : MonoBehaviour
{
    public GameState gameState { get; set; }

    public static GameManager Instance = null;

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else if( Instance != this )
        {
            UnityEngine.Object.Destroy( gameObject );
        }

        DontDestroyOnLoad( gameObject );
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.InPlaying;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
