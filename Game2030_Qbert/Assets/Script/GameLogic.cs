using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] GameObject coily;
    [SerializeField] List<Platform> list_platforms;
    [SerializeField] private bool is_win_cin = false;
    [SerializeField] private int counter_step_on_platform = 0;
    [Header("UI")]
    #region UI for the game
    [SerializeField] Text round_text_ui = null;
    [SerializeField] Text level_text_ui = null;
    [SerializeField] Text score_text_ui = null;
    [SerializeField] Text score_bouus_text_ui = null;
    [SerializeField] Image[] lives_display = null;
    [SerializeField] private int number_of_lives = 3;
    [SerializeField] private int number_of_arounds = 0;
    [SerializeField] private int player_score = 0;
    #endregion
    [Header("Delay for qbert")]
    #region Delay on player
    [SerializeField] bool is_qbert_dead_from_foe = false;
    [SerializeField] private float delay_time_qbert_death_by_foe = 4f;
    [SerializeField] private float delay_timer_qbert_death_by_foe = 0;

    [SerializeField] bool is_qbert_dead_off_pyramid = false;
    [SerializeField] private float delay_time_qbert_death_by_jump = 4f;
    [SerializeField] private float delay_timer_qbert_death_by_jump = 0;
    #endregion

    [Header("Delay for score count")]
    #region Delay for counting points
    [SerializeField] bool is_there_elevater_to_count = false;
    [SerializeField] private float delay_time_each_elevator = 2f;
    [SerializeField] private float delay_timer_each_elevator = 0;
    [SerializeField] bool is_level_clear = false;
    [SerializeField] private float delay_time_bewteen_win_score_count = 4f;
    [SerializeField] private float delay_timer_bewteen_win_score_count = 0;
    #endregion

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

        list_platforms = new List<Platform>();

        if (game_Objects.Length > 0)
        {

            if (list_platforms.Count == 0)
            {

                foreach (GameObject go in game_Objects)
                {
                    list_platforms.Add(go.GetComponent<Platform>());
                    go.GetComponent<Platform>().On_platform_event += PlatformEventListener;
                }
            }
        }
        score_bouus_text_ui.enabled = false;
        delay_timer_each_elevator = delay_time_each_elevator;
        delay_timer_qbert_death_by_foe = delay_time_qbert_death_by_foe;
        delay_timer_qbert_death_by_jump = delay_time_qbert_death_by_jump;
        delay_timer_bewteen_win_score_count = delay_time_bewteen_win_score_count;
        StartCoroutine(WaitAndDisplaylives(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        /// - when you dead on platform
        if (is_qbert_dead_from_foe)
        {
            delay_timer_qbert_death_by_foe -= Time.deltaTime;
            if (delay_timer_qbert_death_by_foe < 0)
            {
                if (number_of_lives <= 0)
                {
                    player.GetComponent<PlayerController>().Qbertlost();
                    is_qbert_dead_from_foe = false;
                    delay_timer_qbert_death_by_foe = delay_time_qbert_death_by_foe;
                }
                else
                {
                    is_qbert_dead_from_foe = false;
                    delay_timer_qbert_death_by_foe = delay_time_qbert_death_by_foe;
                    player.GetComponent<PlayerController>().QbertReviceOnPyramid();
                }
            }

        }

        /// - when you dead off platform
        if (is_qbert_dead_off_pyramid)
        {
            delay_timer_qbert_death_by_jump -= Time.deltaTime;
            if (delay_timer_qbert_death_by_jump < 0)
            {
                if (number_of_lives <= 0)
                {
                    player.GetComponent<PlayerController>().Qbertlost();
                    is_qbert_dead_off_pyramid = false;
                    delay_timer_qbert_death_by_jump = delay_time_qbert_death_by_jump;
                }
                else
                {
                    is_qbert_dead_off_pyramid = false;
                    delay_timer_qbert_death_by_jump = delay_time_qbert_death_by_jump;
                    ResetThePlatfromsofLastPlayerLocation();
                    player.GetComponent<PlayerController>().QbertReviceOffPyramid();
                }


            }

        }

        if (coily == null)
        {
            GameObject temp_snake;
            temp_snake = GameObject.FindGameObjectWithTag("Snake");
            if (temp_snake != null)
            {
                coily = temp_snake;
                coily.GetComponent<snake>().On_coily_event += CoilyEventListener;
            }

        }

        if (is_level_clear)
        {
            delay_timer_bewteen_win_score_count -= Time.deltaTime;

            if (delay_timer_bewteen_win_score_count < 0)
            {
                if (player != null)
                {
                    player.GetComponent<PlayerController>().QbertCountScore();
                    foreach (Platform platform in list_platforms)
                    {
                        platform.GetComponent<Platform>().GetPlatformStopPlayingWinAnimation();
                    }
                    is_level_clear = false;
                }
            }

        }

        /// add score if there is elevator
        if (is_there_elevater_to_count)
        {
            GameObject temp_elevator;
            temp_elevator = GameObject.FindGameObjectWithTag("Elevator");

            if (temp_elevator != null)
            {

                delay_timer_each_elevator -= Time.deltaTime;

                if (delay_timer_each_elevator < 0)
                {
                    SoundManager.Instance.PlaySoundEffect("AddElevatorToScore");
                    player_score += 100;
                    DisplayScore();
                    Destroy(temp_elevator);
                    delay_timer_each_elevator = delay_time_each_elevator;
                }

            }
            else
            {
                is_there_elevater_to_count = false;
            }
        }


    }



    private void OnDestroy()
    {

        if (player != null)
        {
            player.GetComponent<PlayerController>().On_qbert_event -= QbertEventListener;
        }

        if (list_platforms != null && list_platforms.Count != 0)
        {
            foreach (Platform platform in list_platforms)
            {
                platform.On_platform_event -= PlatformEventListener;
            }
        }
        if (coily != null)
        {
            coily.GetComponent<snake>().On_coily_event -= CoilyEventListener;
        }
    }

    private void QbertEventListener(Qbert_Event_states qbert_event)
    {
        switch (qbert_event)
        {
            case Qbert_Event_states.Kdeath_player_reach_bottom:
                number_of_lives -= 1;
                is_qbert_dead_off_pyramid = true;
                break;
            case Qbert_Event_states.Krevive_player_off_pyramid:
                ChangeLivesDisplayed();
                break;
            case Qbert_Event_states.Kdeath_on_pyramid:
                number_of_lives -= 1;
                is_qbert_dead_from_foe = true;
                break;
            case Qbert_Event_states.Krevive_player_pyramid:
                ChangeLivesDisplayed();
                PlacePlayerOnRightplatform();
                break;
            case Qbert_Event_states.Ktouch_greenball:
                player_score += 100;
                DisplayScore();
                break;
            case Qbert_Event_states.kcount_score:    
                StartCoroutine(WaitAndclearBonusScore( 2f));
                break;
        }

    }

    private void PlatformEventListener(bool is_step)
    {
        if (is_step)
        {

            player_score += 25;
            DisplayScore();
            counter_step_on_platform++;
            if (counter_step_on_platform == list_platforms.Count)
            {
                CheckIfAllPlatformBeenStepOn();
            }
        }
    }


    private void CoilyEventListener(bool does_coily_exist)
    {
        if (!does_coily_exist)
        {
            player_score += 500;
            DisplayScore();
        }

    }


    void PlacePlayerOnRightplatform()
    {
        if (player != null && list_platforms.Count != 0)
        {
            foreach (Platform platform in list_platforms)
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



    void ResetThePlatfromsofLastPlayerLocation()
    {
        if (list_platforms.Count != 0)
        {
            foreach (Platform platform in list_platforms)
            {
                platform.set_is_player_current_this_platform = false;

            }
        }
    }





    void ChangeLivesDisplayed()
    {
        if (lives_display.Length > 0 && lives_display != null)
        {
            switch (number_of_lives)
            {
                case 3:
                    for (int i = 0; i < lives_display.Length; i++)
                    {
                        if (i < 2)
                        {
                            lives_display[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            lives_display[i].gameObject.SetActive(false);
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < lives_display.Length; i++)
                    {
                        if (i < 1)
                        {
                            lives_display[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            lives_display[i].gameObject.SetActive(false);
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < lives_display.Length; i++)
                    {

                        lives_display[i].gameObject.SetActive(false);
                    }
                    break;
            }
        }

    }

    /// <summary>
    /// win cond
    /// </summary>
    void CheckIfAllPlatformBeenStepOn()
    {
        foreach (Platform platform in list_platforms)
        {
            if (!platform.get_has_been_step_on)
            {
                is_win_cin = false;
                break;
            }
            else
            {
                is_win_cin = true;
            }
        }

        if (is_win_cin)
        {
            foreach (Platform platform in list_platforms)
            {
                platform.GetPlatformToPlayWinAnimation();
            }
            player.GetComponent<PlayerController>().QbertWin();
            is_level_clear = true;
        }
    }


    IEnumerator WaitAndDisplaylives(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeLivesDisplayed();
    }

    IEnumerator WaitAndclearBonusScore(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        score_bouus_text_ui.enabled = true;
        score_bouus_text_ui.text = "<color=#ff00ffff>" + "Bonus: " + "</color>" + "<color=orange>" + "1000" + "</color>";
        yield return new WaitForSeconds(waitTime);
        score_bouus_text_ui.enabled = false;
        player_score += 1000;
        DisplayScore();
        yield return new WaitForSeconds(waitTime);
        is_there_elevater_to_count = true;
    }

    void DisplayScore()
    {
        if (score_text_ui != null)
        {
            score_text_ui.text = player_score.ToString();
        }
    }
}
