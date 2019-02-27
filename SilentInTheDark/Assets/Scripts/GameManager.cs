using System.Collections;
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

    private GameManager()
    {

    }
    public static GameManager Instance { get; } = new GameManager();

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
