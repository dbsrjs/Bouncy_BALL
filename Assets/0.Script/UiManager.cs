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

    public Text stageCountText; //스테이지 Text
    [SerializeField] private Text timerText;    //타이머 Text
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NoRePlayPanel;

    private void Awake()
    {
        instance = this;
        stageCountText.text = $"0 / {totalItemCount}";
    }

    private void Update()
    {
        timerText.text = Timer.main.timer.ToString("F3");   //Timer 증가(소수점 셋째 자리까지 표시)
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

    //재시작 안 하는 버튼
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
}
