using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using scripts from: https://www.youtube.com/watch?v=POq1i8FyRyQ
public class Timer : MonoBehaviour
{
    float dayTime;
    [SerializeField] TextMeshProUGUI timerText;
    void Update()
    {
        dayTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(dayTime / 60);
        int seconds = Mathf.FloorToInt(dayTime % 60);
        timerText.text= string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
