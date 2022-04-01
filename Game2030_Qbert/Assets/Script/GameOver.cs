using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("GamerOver UI")]
    #region Game over UI
    [SerializeField] private Text text_title = null;
    [SerializeField] private Text text_status = null;
    [SerializeField] private Button return_button = null;
    [SerializeField] private Button add_score_button = null;
    #endregion
    [Header("AddHighScore UI")]
    #region AddHighScore UI
    [SerializeField] private InputField name_inputField;
    [SerializeField] private Button add_button = null;
    #endregion
    [Header("Display LeaderBoard")]
    #region AddHighScore UI
    [SerializeField] private Text [] text_leader_board = null;
    [SerializeField] private Button return_button_2 = null;
    public List<RankPlayerData> list_players_data;
    #endregion

    #region AddHighScore UI
    [SerializeField] private string name = null;
    public string player_name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
        [SerializeField] private int score = 0;
    public int player_score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }
  
    #endregion

    public static GameOver Instance { get; private set; }
    private void Awake()
    {
        /***
         * Enforce Singleton Pattern described above. 
         ***/
        if (Instance == null)
        {
            Instance = this;
          

        }
        else
        {
            Destroy(this);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        list_players_data = new List<RankPlayerData>();

        if (add_score_button != null && add_button) 
        {
            add_score_button.GetComponent<Button>().onClick.AddListener(AddScoreIsPressed);
            add_button.GetComponent<Button>().onClick.AddListener(EnterIsPressed);
        }

        add_button.gameObject.SetActive(false);
        name_inputField.gameObject.SetActive(false);
        return_button_2.gameObject.SetActive(false);
        foreach (Text text in text_leader_board) 
        {
            text.gameObject.SetActive(false);
        }


        if (return_button != null && return_button_2 != null) 
        {
            return_button_2.GetComponent<Button>().onClick.AddListener(ReturnIsPressed);
            return_button.GetComponent<Button>().onClick.AddListener(ReturnIsPressed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverInitial(bool is_has_clear, bool is_has_highscore)
    {
        if (is_has_clear && is_has_highscore)
        {
            if (text_status != null && return_button != null && add_score_button != null)
            {
                text_status.text = "You won";
                text_status.gameObject.SetActive(true);
                return_button.gameObject.SetActive(true);
                add_score_button.gameObject.SetActive(true);
            }
        }
        else if (!is_has_clear && !is_has_highscore)
        {
            if (text_status != null && return_button != null && add_score_button != null)
            {
                text_status.text = "You lost";
                text_status.gameObject.SetActive(true);
                return_button.gameObject.SetActive(true);
                add_score_button.gameObject.SetActive(false);
            }
        }
        else if (!is_has_clear && is_has_highscore)
        {
            if (text_status != null && return_button != null && add_score_button != null)
            {
                text_status.text = "You lost";
                text_status.gameObject.SetActive(true);
                return_button.gameObject.SetActive(false);
                add_score_button.gameObject.SetActive(true);
            }
        }
        else if (is_has_clear && !is_has_highscore)
        {
            if (text_status != null && return_button != null && add_score_button != null)
            {
                text_status.text = "You won";
                text_status.gameObject.SetActive(true);
                return_button.gameObject.SetActive(true);
                add_score_button.gameObject.SetActive(false);
            }
        }

    }



    public void AddScoreIsPressed()
    {
        if (text_title != null) 
        {
            text_title.text = "leader"; 
        }
        add_score_button.gameObject.SetActive(false);
        if (name_inputField != null && add_button  != null)
        {
            name_inputField.gameObject.SetActive(true);
            add_button.gameObject.SetActive(true);
        }

        text_status.gameObject.SetActive(false);
        return_button.gameObject.SetActive(false);
        add_score_button.gameObject.SetActive(false);
        
    }


    public void EnterIsPressed()
    {
        if ( name_inputField != null && name_inputField.text != "")
        {
            if (text_title != null)
            {
                text_title.text = "leader";
            }
            add_score_button.gameObject.SetActive(false);
            if (name_inputField != null && add_button)
            {
                name_inputField.gameObject.SetActive(false);
                add_button.gameObject.SetActive(false);
            }

            text_status.gameObject.SetActive(false);
            return_button.gameObject.SetActive(false);
            add_score_button.gameObject.SetActive(false);

            name = name_inputField.text.ToString();


            if (name != null && score != 0)
            {
                GameData.Instance.AddPlayerDataToRankPlayersData(new RankPlayerData(name, score));
            }

            GameData.Instance.CopyList(list_players_data);

            for (int i = 0; i < list_players_data.Count; i++)
            {
             
                    text_leader_board[i].text = "<color=cyan>" + (1 + i).ToString() + ")" + list_players_data[i].get_player_name_data().ToString() + "</color>" + " " + "<color=yellow>" + list_players_data[i].get_player_score_data().ToString() + "</color>";
                    text_leader_board[i].gameObject.SetActive(true);
                
            }
            if (return_button_2 != null) 
            {
                return_button_2.gameObject.SetActive(true);
            }
        }
       

    }


   private void ReturnIsPressed() 
    {
        GameStateManager.Instance.ReturnButtionIsPressed();
    }
}
