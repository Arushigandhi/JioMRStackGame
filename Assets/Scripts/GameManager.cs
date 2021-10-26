using System;
using System.Collections;
using System.Collections.Generic;
using JMRSDK.InputModule;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour,ISelectClickHandler { 
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;
    // public Text phaseDisplayText;
    private Touch theTouch;
    // private float timeTouchEnded;
    // private float displayTime = .5f;
    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }
    public void OnSelectClicked(SelectClickEventData eventData) {
     
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            currentSpawner.SpawnCube();
            //FindObjectOfType<CubeSpawner>().SpawnCube();
            OnCubeSpawned();


    }


    public static event Action OnCubeSpawned = delegate {};
    // Update is called once per frame
 
    private void Start()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }

}



