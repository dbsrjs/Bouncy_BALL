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

    private void Awake()
    {
        instance = this;

        soundManager = player.GetComponent<AudioSource>();
        stageCountText.text = $"0 / {totalItemCount}";
    }

    private void Start()
    {
        soundSlider.value = soundManager.volume;
    }

    private void Update()
    {
        timerText.text = Timer.instance.timer.ToString("F3");   //Timer 증가(소수점 셋째 자리까지 표시)

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC");
            if (SettingPanel.activeSelf == true)//켜져 있다면
                ExitSetting();
            else
                OpenSetting();
        }
    }

    public void GetItem(int count)//루비를 먹었을 때 루비 개수 변경
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
    }

    /// <summary>
    /// 설정 창 열기
    /// </summary>
    public void OpenSetting()
    {
        Time.timeScale = 0;
        SettingPanel.SetActive(true);
    }

    /// <summary>
    /// 설창 창 닫기
    /// </summary>
    public void ExitSetting()
    {
        Time.timeScale = 1;
        SettingPanel?.SetActive(false);
    }

    public void Sound()
    {
        soundManager.volume = soundSlider.value;

        Debug.Log(soundManager.volume);
    }
}