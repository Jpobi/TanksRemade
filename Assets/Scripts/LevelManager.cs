using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using System;
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
    public static LevelManager Instance
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
    public GameObject heartsCounter;
    public GameObject heartPrefab;
    public float offset = 1.5f;
    

    public void enemyDeath()
    {
        totEnemies=totEnemies-1;
        enemyCounter.text = "Enemigos: " + totEnemies;
        if(totEnemies == 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void updateLives(int lives)
    {
        playerLives=lives;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        int heartsDifference = heartsCounter.transform.childCount - (playerLives+1);

        for (int i = 0; i < heartsDifference; i++)
        {
            Destroy(heartsCounter.transform.GetChild(heartsCounter.transform.childCount-1-i).gameObject);
        }

        for (int i = heartsCounter.transform.childCount; i < playerLives+1; i++)
        {
            float xPosition =-1.8f + i * offset;
            var newHeart=Instantiate(heartPrefab, heartsCounter.transform);
            newHeart.transform.SetLocalPositionAndRotation(new Vector3(xPosition, 0, 0),Quaternion.identity);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (LevelManager.playerLives <2)
            {
                LevelManager.playerLives = 2;
            }
        }
        aStar.GetComponent<AstarPath>().graphs[0].GetNodes(node=> { if (node.Walkable) nodes.Add(node); });
        enemyCounter.text = "Enemigos: " + totEnemies;
        levelCounter.text = "Nivel: " + nivel;
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemUpgrade (DMG)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemUpgrade (Shield)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemUpgrade (SPEED)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemWeapon (Sniper)") });
        itemsList.Add(new ItemData { cooldown = 2f, isPlaced = true, prefab = Resources.Load<GameObject>("ItemWeapon (Shotgun)") });
        UpdateHearts();
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