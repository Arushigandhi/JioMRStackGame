using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    AudioSource PerfectStackResponse, ImperfectStackResponse;
    [SerializeField]
    MovingCube caller;
    [SerializeField]
    MovingCube movingcube;

    private int score;
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.OnCubeSpawned += GameManager_OnCubeSpawned;
        
    }
    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubeSpawned;
    }
    private void GameManager_OnCubeSpawned()
    {
        score++;
        text.text = "Score: " + score;
        //To increase speed as score increases
        if (score % 5 == 0)
        {
            movingcube.moveSpeed += movingcube.speedIncrease;
           
        }

        //PerfectStackResponse.Play(0);
        float hangover = caller.GetHangover();
        if (caller.Audio(hangover) == true)
        {
            PerfectStackResponse.Play();
        }
        else
        {
            ImperfectStackResponse.Play();
        }




    }

}
