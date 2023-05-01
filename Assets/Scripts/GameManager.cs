using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState {
    Menu,
    Options,
    InGame
};

public enum RoundState {
    WinRound,
    LoseRound,
    BossRound,
    InRound
};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RoundState CurrentRoundState;
    public GameState CurrentGameState;

    public static event Action<GameState> OnGameStateChanged;

    void Awake() {
        Instance = this;    
    }

    void Start() {
        NewGameState(GameState.Menu);
    }

    public void NewGameState(GameState NewState)
    {
        this.CurrentGameState = NewState;

        switch (NewState)
        {
            case GameState.InGame:
                break;
            case GameState.Menu:
                break;
            case GameState.Options:
                break;
            default:
                this.CurrentGameState = GameState.Menu;
                break;
        }

        OnGameStateChanged?.Invoke(NewState);
    }
}
