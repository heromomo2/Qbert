using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    
    public GameObject start_game_button = null;
    public GameObject leader_board_button = null;
    public GameObject Quit_button = null;


    // Start is called before the first frame update
    void Start()
    {
        
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "LeaderBoardButton")
                leader_board_button = go;
            else if (go.name == "StartGameButton")
                start_game_button = go;
            else if (go.name == "QuitButton")
                Quit_button = go;
        }

        leader_board_button.GetComponent<Button>().onClick.AddListener(LeaderBoardButtonOnPress);
        start_game_button.GetComponent<Button>().onClick.AddListener(StartGameButtonOnPress);
        Quit_button.GetComponent<Button>().onClick.AddListener(QuitButtionOnPress);
        
       if( GameStateManager.Instance.main_menu == null)
       {
            GameStateManager.Instance.main_menu = this.gameObject;
       }
    }

    


    void LeaderBoardButtonOnPress()
    {
        GameStateManager.Instance.LeaderBoardButtonOnPress();
    }
    void StartGameButtonOnPress()
    {
        GameStateManager.Instance.StartGameButtonOnPress();
    }
    void QuitButtionOnPress()
    {
        GameStateManager.Instance.QuitButtonOnPress();
    }

}
