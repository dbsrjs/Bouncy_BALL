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

    public void GameOver()  //게임 끝날을 때
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void RePlayYes() //재시작 버튼
    {
        SceneManager.LoadScene("Game_0");
        Time.timeScale = 1;
    }

    public void RePlayNo()  //재시작 안 하는 버튼
    {
        NoRePlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void ExitGameYes()//게임 종료 버튼
    {
        UnityEditor.EditorApplication.isPlaying = false;    //빌드 전
        //Application.Quit(); //빌드
    }

    public void ExitGameNo()    //게임 종료 안 하는 버튼
    {
        NoRePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void GameStart() //게임 시작 버튼
    {
        Time.timeScale = 1;
        StartButton.SetActive(false);
    }
}
