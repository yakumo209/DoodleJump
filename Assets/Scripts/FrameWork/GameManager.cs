using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{

    public Queue<GameObject> tilePool = new Queue<GameObject>();
    public Queue<GameObject> coinPool = new Queue<GameObject>();
    public Queue<GameObject> bulletPool = new Queue<GameObject>();
    public Queue<GameObject> enemyPool = new Queue<GameObject>();
    public Queue<GameObject> itemPool = new Queue<GameObject>();
    private int initialSize = 60;
    private float totalSum;
    private float currentY = -4.3f;
    public GameObject tilePrefab;
    public GameObject coinPrefab;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public GameObject itemPrefab;
    public GameSetting advance;
    public GameObject floor;
    public GameState gameState = GameState.Paused;
    public Transform parent;
    private int coin;
    private int score;
    public Transform bulletPos;

    private float lastTime;
    public int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GetAllWeight();
        GenerateTilePool();
        GenerateCoinPool();
        GenerateBulletPool();
        GenerateEnemyPool();
        GenerateItemPool();
        //generate tile
        for (int i = 0; i < initialSize; i++)
        {
             GenerateTile();
             GenerateItem();
             GenerateEnemy();
             GenerateCoin();
        }
    }

    

    private void GenerateTile()
    {
        GameObject go = GetInactiveObject(ObjectType.Tile);
        float rand = Random.Range(0, totalSum);
        int randNum = SetTileByRandomNumber(rand);
        float left = Camera.main.ViewportToWorldPoint(new Vector2(0,0)).x;
        float right = Camera.main.ViewportToWorldPoint(new Vector2(1,0)).x;
        Vector2 pos=new Vector2(Random.Range(left+1f,right-1f),currentY);
        
        switch (randNum)
        {
            case 0:
                go.transform.position = pos;
                currentY += Random.Range(advance.normalTile.minHeight, advance.normalTile.maxHeight);
                go.name = "0";
                go.SetActive(true);
                break;
            case 1: 
                go.transform.position = pos;
                currentY += Random.Range(advance.brokenTile.minHeight, advance.brokenTile.maxHeight);
                go.name = "1";
                go.SetActive(true);
                break;
            case 2:
                go.transform.position = pos;
                currentY += Random.Range(advance.oneTimeOnly.minHeight, advance.oneTimeOnly.maxHeight);
                go.name = "2";
                go.SetActive(true);
                break;
            case 3:
                go.transform.position = pos;
                currentY += Random.Range(advance.springTile.minHeight, advance.springTile.maxHeight);
                go.name = "3";
                go.SetActive(true);
                break;
            case 4:
                go.transform.position = pos;
                currentY += Random.Range(advance.movingHorizontally.minHeight, advance.movingHorizontally.maxHeight);
                go.name = "4";
                go.SetActive(true);
                break;
            case 5:
                go.transform.position = pos;
                currentY += Random.Range(advance.movingVertiacally.minHeight, advance.movingVertiacally.maxHeight);
                go.name = "5";
                go.SetActive(true);
                break;
            default:
                break;
        }
    }

    void GenerateItem()
    {
        
        
        float rand = Random.Range(0f, 1f);
        if (rand<advance.itemProbability)
        {
            GameObject randGo = null;
            while (true)
            {
                randGo = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;
                if (randGo.GetComponent<Tile>().tileType<4&& randGo.transform.position.y>5f)
                {
                    break;
                }
            }
            GameObject go = GetInactiveObject(ObjectType.Item);
            go.name = Random.Range(0, 2).ToString();
            go.SetActive(true);
            go.transform.position = randGo.transform.position+new Vector3(0,.5f,0);
        }
    }

    void GenerateEnemy()
    {
        float rand = Random.Range(0f, 1f);
        if (rand<advance.enemyProbability)
        {
            
            float left = Camera.main.ViewportToWorldPoint(new Vector2(0,0)).x;
            float right = Camera.main.ViewportToWorldPoint(new Vector2(1,0)).x;
            Vector2 pos=new Vector2(Random.Range(left+1f,right-1f),currentY);
            GameObject go = GetInactiveObject(ObjectType.Enemy);
            go.name = Random.Range(0, 3).ToString();
            go.SetActive(true);
            go.transform.position = pos;
        }
    }


    void GenerateCoin()
    {
        float rand = Random.Range(0f, 1f);
        if (rand<advance.coinProbability)
        {
            
            float left = Camera.main.ViewportToWorldPoint(new Vector2(0,0)).x;
            float right = Camera.main.ViewportToWorldPoint(new Vector2(1,0)).x;
            Vector2 pos=new Vector2(Random.Range(left+1f,right-1f),currentY);
            GameObject go = GetInactiveObject(ObjectType.Coin);
            go.SetActive(true);
            go.transform.position = pos;
        }
    }

    void IncreaseDifficulty(float time)
    {
        if (Time.time-lastTime>time)
        {
            lastTime = Time.time;
            advance.enemyProbability += 0.01f;
            print("Increased");
        }
    }
    
    int SetTileByRandomNumber(float num)
    {
        if (num<=advance.normalTile.weight)
        {
            return 0;
        }
        else if (num<=advance.normalTile.weight+advance.brokenTile.weight)
        {
            return 1;
        }
        else if (num<=advance.normalTile.weight+advance.brokenTile.weight+advance.oneTimeOnly.weight)
        {
            return 2;
        }
        else if (num<=advance.normalTile.weight+advance.brokenTile.weight+advance.oneTimeOnly.weight+advance.springTile.weight)
        {
            return 3;
        }
        else if (num<=advance.normalTile.weight+advance.brokenTile.weight+advance.oneTimeOnly.weight+advance.springTile.weight
                 +advance.movingHorizontally.weight)
        {
            return 4;
        }
        else if (num<=advance.normalTile.weight+advance.brokenTile.weight+advance.oneTimeOnly.weight+advance.springTile.weight
                 +advance.movingHorizontally.weight+advance.movingVertiacally.weight)
        {
            return 5;
        }
        else
        {
            return -1;
        }
    }
    
    private void GenerateItemPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go1 = Instantiate(itemPrefab, parent);
            go1.SetActive(false);
            go1.name = i.ToString();
            itemPool.Enqueue(go1);
        }
    }
    private void GenerateCoinPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go1 = Instantiate(coinPrefab, parent);
            go1.SetActive(false);
            go1.name = i.ToString();
            coinPool.Enqueue(go1);
        }
    }
    private void GenerateEnemyPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go1 = Instantiate(enemyPrefab, parent);
            go1.SetActive(false);
            go1.name = i.ToString();
            enemyPool.Enqueue(go1);
        }
    }
    private void GenerateBulletPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go1 = Instantiate(bulletPrefab, parent);
            go1.SetActive(false);
            go1.name = i.ToString();
            bulletPool.Enqueue(go1);
        }
    }
    private void GenerateTilePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go1 = Instantiate(tilePrefab, transform);
            go1.SetActive(false);
            go1.name = i.ToString();
            tilePool.Enqueue(go1);
        }
    }
    public GameObject GetInactiveObject(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Tile:
                return tilePool.Dequeue();
                break;
            case ObjectType.Item:
                return itemPool.Dequeue();
                break;
            case ObjectType.Coin:
                return coinPool.Dequeue();
                break;
            case ObjectType.Enemy:
                return enemyPool.Dequeue();
                break;
            case ObjectType.Bullet:
                GameObject go = bulletPool.Dequeue();
                go.SetActive(true);

                go.transform.position = bulletPos.position;
                return go;
                break;
            default:
                return null;
        }
    }

    void GetAllWeight()
    {
        float sum = 0;
        sum += advance.normalTile.weight;
        sum += advance.brokenTile.weight;
        sum += advance.oneTimeOnly.weight;
        sum += advance.springTile.weight;
        sum += advance.movingVertiacally.weight;
        sum += advance.movingHorizontally.weight;
        totalSum = sum;
    }

    public void AddInActiveObjectToPool(GameObject go,ObjectType type)
    {
        go.SetActive(false);
        switch (type)
        {
            case ObjectType.Tile:
                tilePool.Enqueue(go);
                CreateTile();
                break;
            case ObjectType.Item:
                itemPool.Enqueue(go);
                break;
            case ObjectType.Coin:
                coinPool.Enqueue(go);
                break;
            case ObjectType.Enemy:
                enemyPool.Enqueue(go);
                break;
            case ObjectType.Bullet:
                bulletPool.Enqueue(go);
                break;
            default:
                break;
        }
    }

    void CreateTile()
    {
        if (gameState!=GameState.Gameover)
        {
            GenerateTile();
            GenerateItem();
            GenerateEnemy();
            GenerateCoin();
            IncreaseDifficulty(5);
            score += 5;
            //添加道具
            //难度曲线
            //分数
        }
    }

    public void GameOver()
    {
        if (gameState != GameState.Gameover)
        {
            gameState = GameState.Gameover;
            //save ui...
        }
    }
}

public enum ObjectType
{
    Tile,
    Item,
    Coin,
    Enemy,
    Bullet,
}
public enum GameState
{
    Paused,
    Running,
    Gameover,
}