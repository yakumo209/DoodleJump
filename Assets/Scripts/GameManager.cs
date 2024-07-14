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
    public GameSetting advance;
    public GameObject floor;
    public GameState gameState = GameState.Paused;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GetAllWeight();
        GenerateTilePool();
        print(totalSum);
        //generate tile
        for (int i = 0; i < initialSize; i++)
        {
             GenerateTile();
        }
    }

    

    private void GenerateTile()
    {
        GameObject go = GetInactiveObject(ObjectType.Tile);
        float rand = Random.Range(0, totalSum);
        int randNum = SetTileByRandomNumber(rand);

        Vector2 pos=new Vector2(Random.Range(-4.5f,4.5f),currentY);
        
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
                return bulletPool.Dequeue();
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