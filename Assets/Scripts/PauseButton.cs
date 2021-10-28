using System;
using UnityEngine;
using JMRSDK.InputModule;

public class PauseButton : MonoBehaviour, IFn1Handler
{
    //bool paused = false;

    //public void OnSelectClicked(SelectClickEventData eventData)
    //{
        //Input.GetButtonDown("PauseButton");
            //paused = togglePause();
    //}

    //void OnGUI()
    //{
        //if (paused)
        //{
            //GUILayout.Label("Game is paused!");
            //if (GUILayout.Button("Click me to unpause"))
                //paused = togglePause();
        //}
    //}

    public void togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            //return (false);
        }
        else
        {
            Time.timeScale = 0f;
            //return (true);
        }
    }
    public void Start()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }

    public void OnFn1Action()
    {
        Debug.Log("OnFn1Action");
        togglePause();
    }
}