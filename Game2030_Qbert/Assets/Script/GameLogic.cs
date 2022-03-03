using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update

 
    void Start()
    {
        int random_number = Random.Range(0, 600);
        RankPlayerData test_rank_player = new RankPlayerData("Momo", random_number);

        if (GameData.Instance.IsYourScoreRankWorthy((test_rank_player.get_player_score_data())))
        {
            GameData.Instance.AddPlayerDataToRankPlayersData(test_rank_player);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
