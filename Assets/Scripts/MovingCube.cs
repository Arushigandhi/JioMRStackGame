﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public partial class MovingCube : MonoBehaviour
{
    [SerializeField]
    public AudioSource PerfectStackResponse, ImperfectStackResponse;
    public static MovingCube CurrentCube {get; private set;}
    bool toggleChangePerfect = true, toggleChangeImperfect = true;
    public static MovingCube LastCube {get; private set;}
    public MoveDirection MoveDirection { get; set; }
    [SerializeField]
    public float moveSpeed = 1f;
    public float speedIncrease = 1f;

    public bool XZ;
    private void OnEnable()
    {
        if(LastCube==null)
            LastCube=GameObject.Find("Start").GetComponent<MovingCube>();
        CurrentCube = this;    
        GetComponent<Renderer>().material.color = GetRandomColor();
        transform.localScale = new Vector3(LastCube.transform.localScale.x,transform.localScale.y,LastCube.transform.localScale.z);
    }
     private Color GetRandomColor()
     {
         return new Color(UnityEngine.Random.Range(0,1f),UnityEngine.Random.Range(0,1f),UnityEngine.Random.Range(0,1f));
     }
      public void SplitCubeOnX(float hangover, float direction)
     {
        //Getting AudioSource

        XZ = true;
            float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.x - newXSize;
            float newXPosition = LastCube.transform.position.x + (hangover/2);
            transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newXPosition,transform.position.y, transform.position.z);
            float cubeEdge = transform.position.x + (newXSize/2f * direction);
            float fallingBlockXPosition = cubeEdge + (fallingBlockSize/2f * direction);
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale=  Vector3.one * 0.1f;
            sphere.transform.position= new Vector3(transform.position.x,transform.position.y,cubeEdge);


        SpawnDropCube(fallingBlockXPosition,fallingBlockSize);


    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        XZ = false;
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;
        float newZPosition = LastCube.transform.position.z + (hangover/2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);
        float cubeEdge = transform.position.z + (newZSize/2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize/2f * direction);
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale=  Vector3.one * 0.1f;
        sphere.transform.position= new Vector3(transform.position.x,transform.position.y,cubeEdge);
        

        SpawnDropCube(fallingBlockZPosition,fallingBlockSize);
        //print(fallingBlockSize);


    }
    internal void Stop()
    {
        moveSpeed=0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        if (Mathf.Abs(hangover) >= max){
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(2);
        }
        float direction = hangover>0 ? 1f : -1f;
        if (MoveDirection == MoveDirection.Z)
        {
            SplitCubeOnZ(hangover, direction);
        }
        else
        {
            SplitCubeOnX(hangover, direction);
        }

        LastCube = this;


        toggleChangePerfect = true;
        toggleChangeImperfect = true;
    }

    public bool Audio(float hangover)
    {
        float fallingBlockSize,factor;
        //PerfectStackResponse = GetComponent<AudioSource>();
        //ImperfectStackResponse = GetComponent<AudioSource>();
        if (XZ)
        {
            float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
            fallingBlockSize = transform.localScale.x - newXSize;
            factor = fallingBlockSize/newXSize;
            //print(hangover);
        }
        else
        {
            float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
            fallingBlockSize = transform.localScale.z - newZSize;
            factor = fallingBlockSize/newZSize;
            //print(hangover);
        }

        if (fallingBlockSize<=5f )
        {
            return (true);
        }
        else
        {
            return (false);
        }
        
        
    }

    public float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
            return transform.position.x - LastCube.transform.position.x;

    }


    private void SpawnDropCube(float fallingBlockZPosition,float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z );
            cube.transform.position = new Vector3(fallingBlockZPosition,transform.position.y, transform.position.z);

        }

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject,1f);
    }
    // private void SpawnDropXCube(float fallingBlockXPosition,float fallingBlockSize)
    // {
    //     var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //     cube.transform.localScale= new Vector3(fallingBlockSize, transform.localScale.y,transform.localScale.z);
    //     cube.transform.position= new Vector3(fallingBlockXPosition, transform.position.y,transform.position.z);
    //     cube.AddComponent<Rigidbody>();
    //     cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
    //     Destroy(cube.gameObject,1f);
    // }

    void Update()
    {
        if (MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.right * Time.deltaTime * moveSpeed;

    }
}