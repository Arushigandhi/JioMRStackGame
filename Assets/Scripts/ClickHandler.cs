using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class ClickHandler : MonoBehaviour,ISelectClickHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnSelectClicked(SelectClickEventData eventData)
    {
        print("Clicked!");
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }


}
