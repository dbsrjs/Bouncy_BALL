using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTime : MonoBehaviour
{
    private Text timeText;
    private float time;


    private void Awake()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = $"Time : {time.ToString("F2")}";
    }
}