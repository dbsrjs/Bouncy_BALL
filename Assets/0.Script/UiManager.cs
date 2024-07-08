using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public int totalItemCount;  //현재 갖고 있는 루비

    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject player;

    public Text stageCountText; //스테이지 Text
    [SerializeField] private Text timerText;    //타이머 Text

    [Header("Panel")]
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NoRePlayPanel;
    [SerializeField] private GameObject SettingPanel;

    [Header("UI")]
    [SerializeField] private Slider soundSlider;

    private AudioSource soundManager;

    private bool gameStart = false;//게임을 시작헀냐?

    private void Awake()
    {
        instance = this;

        soundManager = player.GetComponent<AudioSource>();
        stageCountText.text = $"0 / {totalItemCount}";
    }

    private void Start()
    {
        soundManager.volume = PlayerPrefs.GetFloat("Volume");
        soundSlider.value = soundManager.volume;
        PlayerPrefs.DeleteAll(); 
    }

    private void Update()
    {
        timerText.text = Timer.instance.timer.ToString("F3");   //Timer 증가(소수점 셋째 자리까지 표시)

        if (Input.GetKeyDown(KeyCode.Escape))//ESC키를 눌렀을 때
            OpenSetting();
    }

    /// <summary>
    /// 루비를 먹었을 때 루비 개수 변경
    /// </summary>
    /// <param name="count"></param>
    public void GetItem(int count)
    {
        stageCountText.text = $"{count.ToString()} / {totalItemCount}";
    }

    /// <summary>
    /// 게임 끝날을 때
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// 재시작 버튼
    /// </summary>
    public void RePlayYes() 
    {
        SceneManager.LoadScene("Game_0");
        Time.timeScale = 1;
    }

    /// <summary>
    /// 재시작 안 하는 버튼
    /// </summary>
    public void RePlayNo() 
    {
        NoRePlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    /// <summary>
    /// 게임 종료 버튼
    /// </summary>
    public void ExitGameYes()
    {
        Debug.Log("ExitGame");
        //UnityEditor.EditorApplication.isPlaying = false;    //빌드 전
        Application.Quit(); //빌드
    }

    /// <summary>
    /// 게임 종료 안 하는 버튼
    /// </summary>
    public void ExitGameNo()    
    {
        NoRePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// 게임 시작 버튼
    /// </summary>
    public void GameStart()
    {
        Time.timeScale = 1;
        StartButton.SetActive(false);
        gameStart = true;
    }

    /// <summary>
    /// 설정 창 열기
    /// </summary>
    public void OpenSetting()
    {
        if (!gameStart && GameManager.instance.stage == 0)  //게임을 시작하지 않았고, 1스테이지가 아닐 때
        {
            // 삼항 연산자를 사용하여 상태에 따라 SetActive 호출
            SettingPanel.SetActive(SettingPanel.activeSelf ? false : true);
        }

        else
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                SettingPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                SettingPanel?.SetActive(false);
            }
        }
    }

    public void Sound()
    {
        soundManager.volume = soundSlider.value;

        Debug.Log(soundManager.volume);
        PlayerPrefs.SetFloat("Volume", soundManager.volume);
    }
}