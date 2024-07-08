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
    [SerializeField] private GameObject player;

    public Text stageCountText; //�������� Text
    [SerializeField] private Text timerText;    //Ÿ�̸� Text

    [Header("Panel")]
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NoRePlayPanel;
    [SerializeField] private GameObject SettingPanel;

    [Header("UI")]
    [SerializeField] private Slider soundSlider;

    private AudioSource soundManager;

    private bool gameStart = false;//������ ��������?

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
        timerText.text = Timer.instance.timer.ToString("F3");   //Timer ����(�Ҽ��� ��° �ڸ����� ǥ��)

        if (Input.GetKeyDown(KeyCode.Escape))//ESCŰ�� ������ ��
            OpenSetting();
    }

    /// <summary>
    /// ��� �Ծ��� �� ��� ���� ����
    /// </summary>
    /// <param name="count"></param>
    public void GetItem(int count)
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

    /// <summary>
    /// ����� �� �ϴ� ��ư
    /// </summary>
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
        Debug.Log("ExitGame");
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
        gameStart = true;
    }

    /// <summary>
    /// ���� â ����
    /// </summary>
    public void OpenSetting()
    {
        if (!gameStart && GameManager.instance.stage == 0)  //������ �������� �ʾҰ�, 1���������� �ƴ� ��
        {
            // ���� �����ڸ� ����Ͽ� ���¿� ���� SetActive ȣ��
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