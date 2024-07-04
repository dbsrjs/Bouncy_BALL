using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{public static Timer instance;
    [HideInInspector] public float timer;

    private void Awake()
    {
        Time.timeScale = 0;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)//������ �۵� ���� ���
        {
            timer += Time.deltaTime;
        }
    }
}