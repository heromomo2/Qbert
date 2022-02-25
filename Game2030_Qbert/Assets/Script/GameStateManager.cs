using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ChangeGameState(int newState)
    {
        switch (newState)
        {
            case GameStates.MainMenu:
              
                break;
            case GameStates.LeaderBoard:
                
                break;
            case GameStates.Pause:
               
                break;
            case GameStates.Quit:
              
                break;
            case GameStates.Gamemode:

                break;
        }
    }

    static public class GameStates
    {
        public const int MainMenu = 1;

        public const int LeaderBoard = 2;

        public const int Pause = 3;

        public const int Quit = 4;

        public const int Gamemode = 5;

    }
}
