using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStateManager : MonoBehaviour
{
    #region
    public  GameObject main_menu = null;
    #endregion
    #region
    public GameObject leader_board = null;
    #endregion
    #region pauseMenu
    public   GameObject pause_menu = null;
    #endregion
    #region
    public GameObject game_over = null;
    private bool is_game_over = false;
    #endregion
    public static GameStateManager Instance { get; private set; }
    private void Awake()
    {
        /***
         * Enforce Singleton Pattern described above. 
         ***/
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(this);
           
        }
        else
        {
            Destroy(this);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        //Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        //string sceneName = currentScene.name;


    }

    #region MainMenuFucntionsCalls
    public void StartGameButtonOnPress()
    {
        LoadScene("Game");
        
    }
    public void LeaderBoardButtonOnPress()
    {
        ChangeGameState(2);
        leader_board.GetComponent<LeaderBoarder>().DisplayLeaderBoard();
    }
    public void QuitButtonOnPress()
    {
        GameData.Instance.SaveAllRankPlayersData();
        Application.Quit();
    }
#endregion

    #region LearderBoardFuctionsCall
    public  void MainMenuButtonOnPress()
    {
        ChangeGameState(1);
    }
    #endregion

    #region LoadSceneFunction
    private void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    #endregion

    #region PauseMenuFuctionsCalls
    public void Pause()
    {
      

        if (pause_menu != null && !is_game_over )
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print("Esc key was pressed");
                if (Time.timeScale == 1)
                {
                
                    ChangeGameState(3);
                    Time.timeScale = 0;
                    print("pause");
                }
            }
        }
       
    }
    public void ResumeButtonOnPress()
    {
      

        if (pause_menu != null)
        {
                print("Esc key was pressed");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    print("unpause");
                    ChangeGameState(4);
                }
         
        }
     
    }

    public void QuitGameOnPress()
    {
        Time.timeScale = 1;// unpause
        LoadScene("MainMenu");
    }
    #endregion;

    #region GameOverFunction
    public void ReturnButtionIsPressed() 
    {
        LoadScene("MainMenu");
    }
    #endregion

    


    public void ChangeGameState(int newState)
    {
        switch (newState)
        {
            case GameStates.MainMenuState:
                main_menu.SetActive(true);
                leader_board.SetActive(false);
                SoundManager.Instance.PlayMusic();
                break;
            case GameStates.LeaderBoardState:
                main_menu.SetActive(false);
                leader_board.SetActive(true);
                SoundManager.Instance.StopMusic();
                break;
            case GameStates.PauseState:
                pause_menu.SetActive(true);
                SoundManager.Instance.PlayMusic();
                break;
            case GameStates.GamemodeState:
                is_game_over = false;
                pause_menu.SetActive(false);
                game_over.SetActive(false);
                SoundManager.Instance.StopMusic();
                break;
            case GameStates.GameOverState:
                is_game_over = true;
                pause_menu.SetActive(false);
                game_over.SetActive(true);
                break;
               
        }
    }

    static public class GameStates
    {
        public const int MainMenuState = 1;

        public const int LeaderBoardState = 2;

        public const int PauseState = 3;

        public const int GamemodeState = 4;

        public const int GameOverState = 5;

    }
}
