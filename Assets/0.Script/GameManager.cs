using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int stage = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.F1))   //다음 씬으로 이동
        {
            SceneManager.LoadScene("Game_" + (stage + 1).ToString());
        }

        if (Input.GetKeyDown(KeyCode.F2))   //전 씬으로 이동
        {
            SceneManager.LoadScene("Game_" + (stage - 1).ToString());
        }
    }

    private void OnTriggerEnter(Collider other) //떨어졌을 때 재시작
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stage);
        }
    }
}
