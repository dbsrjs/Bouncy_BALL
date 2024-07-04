using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public int totalItemCount;  //���� ���� �ִ� ���

    [SerializeField] private GameObject StartButton;

    public Text stageCountText; //�������� Text
    [SerializeField] private Text timerText;    //Ÿ�̸� Text
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NoRePlayPanel;

    private void Awake()
    {
        instance = this;
        stageCountText.text = $"0 / {totalItemCount}";
    }

    private void Update()
    {
        timerText.text = Timer.main.timer.ToString("F3");   //Timer ����(�Ҽ��� ��° �ڸ����� ǥ��)
    }

    public void GetItem(int count)//��� �Ծ��� �� ��� ���� ����
    {
        stageCountText.text = $"{count.ToString()} / {totalItemCount}";
    }

    /// <summary>
    /// ���� ������ ��
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// ����� ��ư
    /// </summary>
    public void RePlayYes() 
    {
        SceneManager.LoadScene("Game_0");
        Time.timeScale = 1;
    }

    //����� �� �ϴ� ��ư
    public void RePlayNo() 
    {
        NoRePlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    /// <summary>
    /// ���� ���� ��ư
    /// </summary>
    public void ExitGameYes()
    {
        //UnityEditor.EditorApplication.isPlaying = false;    //���� ��
        Application.Quit(); //����
    }

    /// <summary>
    /// ���� ���� �� �ϴ� ��ư
    /// </summary>
    public void ExitGameNo()    
    {
        NoRePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// ���� ���� ��ư
    /// </summary>
    public void GameStart()
    {
        Time.timeScale = 1;
        StartButton.SetActive(false);
    }
}
