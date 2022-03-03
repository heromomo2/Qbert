using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject quit_game_button = null;
    public GameObject resume_button = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "LeaderBoardButton")
                quit_game_button = go;
            else if (go.name == "StartGameButton")
                resume_button = go;
           
        }

        quit_game_button.GetComponent<Button>().onClick.AddListener(QuitGameButtonOnPress);
        resume_button.GetComponent<Button>().onClick.AddListener(ResumeButtonOnPress);
        

        if (GameStateManager.Instance.pause_menu == null)
        {
            GameStateManager.Instance.pause_menu = this.gameObject;
        }

        GameStateManager.Instance.ChangeGameState(4);
    }
    void QuitGameButtonOnPress()
    {
        GameStateManager.Instance.QuitGameOnPress();
    }
    void ResumeButtonOnPress()
    {
        GameStateManager.Instance.ResumeButtonOnPress();
    }
}
