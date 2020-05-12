using System.Collections;
// using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Quadrants Section 

    public Vector2 FirstQuadrant = new Vector2(6, 3);
    public Vector2 SecondQuadrant = new Vector2(-6, 3);
    public Vector2 ThirdQuadrant = new Vector2(-6, -3);
    public Vector2 FourthQuadrant = new Vector2(6, -3);

    // Constant Section

    public static string Red = "Red";
    public static string Green = "Green";
    public static string Blue = "Blue";
    public static string Yellow = "Yellow";

    // Manager Section 

    public static Manager Instance = null;

    [SerializeField]
    public float Timer;
    public bool GameOver = false;

    // Spawm Area Force 

    AreaEffector2D SpawnAreaForce;

    #region Game Bounds

    private float xPositiveLimit = 9.5f;
    private float xNegativeLimit = -9.5f;
    private float yPositiveLimit = 4.5f;
    private float yNegativeLimit = -4.5f;


    // Spawn Points

    public GameObject SpawnPoint;
    List<Vector2> SpawnPointsList = new List<Vector2>();

    #endregion



    private void Start()
    {
        StartSpawnPointList(SpawnPointsList);
    }

    private void Awake()
    {
        InstanceVerify();
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Time.time;
    }


    #region Increase Dificulty

    #endregion

    public void InstanceVerify()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void IncreaseSpawnPoint()
    {
        var point = SpawnPointsList.FirstOrDefault();

        GameObject spawnPoint = Instantiate(SpawnPoint, point, Quaternion.identity) as GameObject;        
        SpawnPointsList.Remove(point);
    }

    public void StartSpawnPointList(List<Vector2> list)
    {
        list.Add(FirstQuadrant);
        list.Add(SecondQuadrant);
        list.Add(ThirdQuadrant);
        list.Add(FourthQuadrant);
    }

    public void GameBounds(GameObject element)
    {
        if (element.transform.position.x > xPositiveLimit)
            element.transform.position = new Vector2(xNegativeLimit, element.transform.position.y);

        if (element.transform.position.x < xNegativeLimit)
            element.transform.position = new Vector2(xPositiveLimit, element.transform.position.y);

        if (element.transform.position.y < yNegativeLimit)
            element.transform.position = new Vector2(element.transform.position.x, yPositiveLimit);

        if (element.transform.position.y > yPositiveLimit)
            element.transform.position = new Vector2(element.transform.position.x, yNegativeLimit);
    }


}
