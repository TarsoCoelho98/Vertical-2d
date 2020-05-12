using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer Auto;
    
    [SerializeField]
    public string CurrentColor = string.Empty;
    
    
    // Start is called before the first frame update
    void Start()
    {        
        Auto = GetComponent<SpriteRenderer>();
        CurrentColor = SpawnManager.Instance.InitializeElement(Auto);
        
        InitializeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Manager.Instance.GameBounds(this.gameObject);
    }

    public void InitializeEnemy()
    {
        int number = SpawnManager.Instance.EnemyNumber;
        string currentName = this.gameObject.name;

        currentName = currentName.Replace("(Clone)", string.Concat("_", number.ToString()));
        currentName = string.Concat(CurrentColor, currentName);

        this.gameObject.name = currentName;
        Auto.enabled = true;
    }  
}
