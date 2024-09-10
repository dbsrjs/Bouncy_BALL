using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rd;
    AudioSource audio;

    public float jumpPower = 30;
    bool isJump;
    public int itemCount;

    public static PlayerBall instance;

    int sceneIndex; //현재 씬 인덱스

    private void Awake()
    {
        instance = this;
        isJump = false;
        rd = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isJump == false && UiManager.instance.gameStart && Time.timeScale == 1)//점프
        {
            isJump = true;
            rd.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

        if (Input.GetKeyDown("r"))   //재시작
        {
            SceneManager.LoadScene(sceneIndex);
        }

        if (Input.GetKeyDown(KeyCode.F1))   //다음 씬으로 이동
        {
            UiManager.instance.GameStart();

            if(sceneIndex == 3)
                UiManager.instance.GameOver(); 

            if(sceneIndex < 3)
            SceneManager.LoadScene(sceneIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.F2) && sceneIndex > 0)   //전 씬으로 이동
        {
            SceneManager.LoadScene(sceneIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))//ESC키를 눌렀을 때
        {
            if(UiManager.instance.shortcutKeyPanel.activeSelf)
                UiManager.instance.OpenShortcutKey();
            else
                UiManager.instance.OpenSetting();
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");           //가로
        float v = Input.GetAxisRaw("Vertical");             //세로

        rd.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            UiManager.instance.GetItem(itemCount);
        }

        else if (other.tag == "Point")
        {
            if (itemCount == UiManager.instance.totalItemCount)
            {
                if (sceneIndex == 3) //마지막(3)단계까지 클리어
                    UiManager.instance.GameOver();

                else
                    SceneManager.LoadScene(sceneIndex + 1);   //다음 단계로 넘어감
            }

            else
                SceneManager.LoadScene(sceneIndex); //현재 단계 재시작
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Wall")
            SceneManager.LoadScene(sceneIndex);
    }
}