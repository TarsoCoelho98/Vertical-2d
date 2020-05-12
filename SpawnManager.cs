using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance = null;

    // Player Section 

    [SerializeField]
    private GameObject Player;

    // Spawn Enemy Section 

    private SpriteRenderer EnemySprite;
    public int EnemyNumber = 0;

    [SerializeField]
    private GameObject Enemy;

    // Difficulty Section 

    [SerializeField]
    private float SpawnEnemyTime;
    private const float FixedSpawnEnemyTime = 3;
    private float SpawnTimeMultiplier = 0.005f;
    
    // Spawm Area Force 

    AreaEffector2D SpawnAreaForce;

    // UI Manager 

    public UI UIManager;

    // Start is called before the first frame update
    void Start()
    {
        InstanceVerify();
        UpdateSpawnEnemyTime();
        SpawnAreaForce = GetComponent<AreaEffector2D>();
        UIManager = GameObject.Find("UIManager").GetComponent<UI>();

        PlayerSpawn();
        StartCoroutine("EnemySpawnRoutine");
    }

    private void Awake()
    {
        PlayerSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnEnemyTime();
    }

    private void EnemySpawn()
    {
        SpawnAreaForceAngleVariation();
        GameObject instance = Instantiate(Enemy, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        EnemyNumber++;
    }

    public void InstanceVerify()
    {
        if (this.gameObject.name.Contains("Point"))
            return;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Manager.Instance.GameOver = true;
            UIManager.Exit();
        }
    }

    private void UpdateSpawnEnemyTime()
    {
        if (SpawnEnemyTime <= 1.5)
            return;

        SpawnEnemyTime = FixedSpawnEnemyTime - (SpawnTimeMultiplier * Time.time);
    }

    private void SpawnAreaForceAngleVariation()
    {
        float angle = Random.Range(0, 359);
        SpawnAreaForce.forceAngle = angle;
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (!Manager.Instance.GameOver)
        {
            yield return new WaitForSeconds(SpawnEnemyTime);
            EnemySpawn();
        }
    }

    private void PlayerSpawn()
    {
        if (this.gameObject.name.Contains("Point"))
            return;

        int[] xQuadrantValues = new int[] { 6, -6};
        int[] yQuadrantValues = new int[] { 3, -3 };

        int xValue = xQuadrantValues[Random.Range(0, xQuadrantValues.Count())];
        int yValue = yQuadrantValues[Random.Range(0, yQuadrantValues.Count())];
       
        Player.transform.position = new Vector3(xValue, yValue);
        Player.gameObject.SetActive(true);
    }

    public string InitializeElement(SpriteRenderer auto)
    {
        int color = Random.Range(1, 5);
        string colorName = string.Empty;


        switch (color)
        {
            case 1:
                auto.color = Color.red;
                colorName = Manager.Red;
                break;

            case 2:
                auto.color = Color.green;
                colorName = Manager.Green;
                break;

            case 3:
                auto.color = Color.blue;
                colorName = Manager.Blue;
                break;

            case 4:
                auto.color = Color.yellow;
                colorName = Manager.Yellow;
                break;
        }

        auto.enabled = true;
        return colorName;
    }    
}
