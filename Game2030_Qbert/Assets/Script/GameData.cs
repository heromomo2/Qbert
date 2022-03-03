using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
   public List<RankPlayerData> rank_players_data;

    public int RankPlayersDataCount()
    {
        return rank_players_data.Count;
    }

    bool has_save = false;
    public static GameData Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

        }
        else
        {
            Destroy(this);
        }


        rank_players_data = new List<RankPlayerData>();
        rank_players_data.Capacity = 10;


        LoadAllRankPlayersData();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveAllRankPlayersData()
    {
        bool has_elements = rank_players_data.Any();

        if (has_elements && !has_save)
        {
            
            StreamWriter sw = new StreamWriter(Application.dataPath + Path.DirectorySeparatorChar + "LeaderBoardData.txt");

            foreach (RankPlayerData rank_Player_data in rank_players_data)
            {


                sw.WriteLine(PlayerDataSavingSignifiers.PlayerDataIdSignifier + "," + rank_Player_data.get_player_name_data() + "," + rank_Player_data.get_player_score_data());

            }
            sw.Close();
            has_save = true;
        }
    }

    public void LoadAllRankPlayersData()
    {
        if (File.Exists(Application.dataPath + Path.DirectorySeparatorChar + "LeaderBoardData.txt"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + Path.DirectorySeparatorChar + "LeaderBoardData.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] csv = line.Split(',');

                int signifier = int.Parse(csv[0]);
                if (signifier == PlayerDataSavingSignifiers.PlayerDataIdSignifier)
                {

                    rank_players_data.Add(new RankPlayerData (csv[1], int.Parse(csv[2]) ));
                }
            }
        }
    }

    public bool IsYourScoreRankWorthy(int score)
    {
        if (rank_players_data.Count != 0)
        {
            foreach (RankPlayerData seach_rank_player_data in rank_players_data)
            {
                if (score > seach_rank_player_data.get_player_score_data())
                {
                    return true;
                }
               else if (rank_players_data.Count <= 9) 
                {
                    return true;
                }
            }
            return false;
        }
        else 
        {
            return true;
        }
    }


    public void CopyList(List<RankPlayerData> leader_rank_player_data)
    {
        
        if (leader_rank_player_data.Count > 0)
        {
            leader_rank_player_data.Clear();
        }

        foreach (RankPlayerData rank_player_data in rank_players_data)
        {
            leader_rank_player_data.Add(rank_player_data);
        }
    }


    public void AddPlayerDataToRankPlayersData(RankPlayerData your_player_data)
    {
        if (rank_players_data.Count > 0)
        {
            // add score
            // sort by score

            if (rank_players_data.Count <= 9)
            {
                rank_players_data.Add(your_player_data);
            }
            else 
            {
                rank_players_data[9] = your_player_data;
            }
           
            rank_players_data.Sort(sortByScore);

            // remove anything beyond 10 element
           
        }
        else
        {
            rank_players_data.Add(your_player_data);
        }
    }


    private int sortByScore(RankPlayerData a, RankPlayerData b)
    {
        if (a.get_player_score_data() < b.get_player_score_data())
        {
            return 1;
        }
        else if (a.get_player_score_data() > b.get_player_score_data())
        {
            return -1;
        }
        return 0;
    }


}

public class RankPlayerData
{
    private string player_name_ = "playerName";
    private int player_score_ = 100;
    public RankPlayerData()
    {

    }
    public RankPlayerData(string name, int score)
    {
        player_name_ = name;
        player_score_ = score;
    }

    public int get_player_score_data()
    {
        return player_score_;
    }
    public void set_player_score_data(int change_score)
    {
        player_score_ = change_score;
    }


    public string get_player_name_data()
    {
        return player_name_;
    }

    public void set_player_name_data(string change_name)
    {
        player_name_ = change_name;
    }

}

public class PlayerDataSavingSignifiers
{
    public const int PlayerDataIdSignifier = 1;
};
