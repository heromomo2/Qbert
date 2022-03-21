using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] List <Platform> list_platforms;

    void Start()
    {
        int random_number = Random.Range(0, 600);
        RankPlayerData test_rank_player = new RankPlayerData("Momo", random_number);

        if (GameData.Instance.IsYourScoreRankWorthy((test_rank_player.get_player_score_data())))
        {
            GameData.Instance.AddPlayerDataToRankPlayersData(test_rank_player);
        }

        if (player != null)
        {
            player.GetComponent<PlayerController>().On_qbert_event += QbertEventListener;
        }
        else 
        {
            GameObject temp_player = temp_player = GameObject.FindGameObjectWithTag("Player");
            if (temp_player != null)
            {
                player = temp_player;
                player.GetComponent<PlayerController>().On_qbert_event += QbertEventListener;
            }
            else 
            {
                Debug.LogWarning("Couldn't find Player Game object in Gamelogic");
            }
        }

        GameObject[] game_Objects = GameObject.FindGameObjectsWithTag("Platform");

        list_platforms = new List <Platform>();

        if (game_Objects.Length > 0) 
        {

            if (list_platforms.Count == 0)
            {
               // int i = 0;
                foreach (GameObject go in game_Objects)
                {
                    list_platforms.Add ( go.GetComponent<Platform>());
                   // i++;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
        if (player != null)
        {
            player.GetComponent<PlayerController>().On_qbert_event -= QbertEventListener;
        }

    }

    private void QbertEventListener(Qbert_Event_states qbert_event)
    {
        switch (qbert_event)
        {
            case Qbert_Event_states.Krevive_player_pyramid:
                PlacePlayerOnRightplatform();
                break;
        }

    }

    void PlacePlayerOnRightplatform() 
    {
        if (player!= null && list_platforms.Count != 0) 
        {
            foreach(Platform platform in list_platforms)
            {
                if (platform.get_is_player_current_this_platform) 
                {
                    GameObject child = platform.gameObject.transform.GetChild(0).gameObject;
                   
                    
                    
                    player.transform.position = child.transform.position;
                   
                    break;
                }
            }
        }
    }
}
