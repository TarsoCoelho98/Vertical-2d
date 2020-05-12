using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Physics 

    private Rigidbody2D Rigid;

    // Constant Sections 

    //[SerializeField]
    private SpriteRenderer Auto;

    [SerializeField]
    private int Points = 0;

    // Color Section 

    [SerializeField]
    private string CurrentColor;

    // UI 

    public UI Interface;

    // Spawn Point Section

    public int[] TargetPoints = new int[] { 10, 20, 30, 40};

    #region Move

    [SerializeField]
    private float speed = 10;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Auto = GetComponent<SpriteRenderer>();
        Rigid = GetComponent<Rigidbody2D>();
        CurrentColor = SpawnManager.Instance.InitializeElement(Auto);

        Interface = GameObject.Find("UIManager").GetComponent<UI>();
        Interface.UpdateScore(Points);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        Move();
        Manager.Instance.GameBounds(this.gameObject);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime));
    }

    private void ChangeColor()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Auto.color = Color.red;
            CurrentColor = Manager.Red;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Auto.color = Color.blue;
            CurrentColor = Manager.Blue;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Auto.color = Color.green;
            CurrentColor = Manager.Green;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Auto.color = Color.yellow;
            CurrentColor = Manager.Yellow;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionComponent = collision.gameObject.GetComponent<Enemy>();
        var collisionColor = collisionComponent.CurrentColor;

        Destroy(collision.gameObject);

        if (CurrentColor == collisionColor)
        {
            Points++;
            Interface.UpdateScore(Points);

            if (TargetPoints.Contains(Points))
                Manager.Instance.IncreaseSpawnPoint();
        }
        else
        {
            Manager.Instance.GameOver = true;
            Destroy(this.gameObject);
            Interface.Exit();
        }
    }
}
