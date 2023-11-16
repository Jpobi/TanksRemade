using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using System;
using UnityEditor.Experimental.GraphView;
using TMPro;

[System.Serializable]
public class ItemData
{
    public float cooldown = 2f;
    public bool isPlaced = true;
    public GameObject prefab;
}

public class LevelManager : MonoBehaviour
{
    #region Sigleton

    private static LevelManager _instance;
    private static LevelManager Instance
    {
        get { return _instance ??= FindObjectOfType<LevelManager>(); }
    }
    #endregion

    public GameObject aStar;

    public Transform lastCheckpoint; 
    public GameObject player;
    public string nextScene;
    List<GraphNode> nodes = new List<GraphNode>();

    public static int playerLives=2;

    public List<ItemData> itemsList = new List<ItemData>();
    public int nivel;
    public int totEnemies;
    public TextMeshProUGUI enemyCounter;
    public TextMeshProUGUI levelCounter;
    

    public void enemyDeath()
    {
        totEnemies=totEnemies-1;
        enemyCounter.text = "Enemigos: " + totEnemies;
        if(totEnemies == 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    void Start()
    {
        aStar.GetComponent<AstarPath>().graphs[0].GetNodes(node=> { if (node.Walkable) nodes.Add(node); });
        enemyCounter.text = "Enemigos: " + totEnemies;
        levelCounter.text = "Nivel: " + nivel;
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemUpgrade (DMG)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemUpgrade (Shield)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemUpgrade (SPEED)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemWeapon (Sniper)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemWeapon (Shotgun)") });
    }

    void Update()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            float currentItem = itemsList[i].cooldown;
            if (currentItem > 0f)
            {
                itemsList[i].cooldown -=Time.deltaTime;
            }
            else
            {
                if (!itemsList[i].isPlaced)
                {
                    var position = (Vector3)nodes[UnityEngine.Random.Range(0, nodes.Count)].position;
                    Instantiate(itemsList[i].prefab, position, Quaternion.identity);
                    itemsList[i].cooldown = 2f;
                    itemsList[i].isPlaced = true;
                }
            }
        }


        if (Input.GetButtonDown("Debug Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}