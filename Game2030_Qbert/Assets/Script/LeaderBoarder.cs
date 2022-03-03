using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoarder : MonoBehaviour
{
   
    public GameObject main_menu_button = null;

    public List<Text> Texts = new List<Text>();

    public List<RankPlayerData> list_players_data;


    // Start is called before the first frame update
    void Start()
    {
        list_players_data = new List<RankPlayerData>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "MainMenuButton")
                main_menu_button = go;
        }

        main_menu_button.GetComponent<Button>().onClick.AddListener(MainMenubuttonButtonOnPress);

        if (GameStateManager.Instance.leader_board == null)
        {
            GameStateManager.Instance.leader_board = this.gameObject;
        }

      
    }




    void MainMenubuttonButtonOnPress()
    {
        GameStateManager.Instance.MainMenuButtonOnPress();
    }

    public void DisplayLeaderBoard()
    {
        if (GameData.Instance.RankPlayersDataCount() > 0) 
        {
            GameData.Instance.CopyList(list_players_data);

            for (int i = 0; i < list_players_data.Count; i++)
            {
                if (i == 0)
                {
                    Texts[i].text =  "<color=cyan>" + list_players_data[i].get_player_name_data().ToString() + "</color>" + " " + "<color=yellow>" + list_players_data[i].get_player_score_data().ToString() + "</color>" ;
                    Texts[i].fontSize = 21;
                }
                else
                {
                    Texts[i].text = "<color=cyan>" + (1 + i).ToString() + ")" + list_players_data[i].get_player_name_data().ToString() + "</color>" + " " + "<color=yellow>" + list_players_data[i].get_player_score_data().ToString() + "</color>";
                }
            }
            Debug.Log("list_players_data.Count " + list_players_data.Count.ToString());
        } 

    }
}
