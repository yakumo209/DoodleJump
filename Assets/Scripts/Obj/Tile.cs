using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public int tileType;

    public Sprite[] sprites;
    private SpriteRenderer sp;

    private float speed;
    private float distance;
    private Vector3 startPos;
    private int direction;
    private void OnEnable()
    {
        Init();
    }
    

    void Init()
    {
        sp = GetComponent<SpriteRenderer>();
        int.TryParse(gameObject.name,out tileType);
        switch (tileType)
        {
            case 0:
                sp.sprite = sprites[0];
                break;
            case 1:
                sp.sprite = sprites[1];
                break;
            case 2:
                sp.sprite = sprites[2];
                break;
            case 3:
                sp.sprite = sprites[3];
                break;
            case 4:
                sp.sprite = sprites[4];
                speed = GameManager.Instance.advance.movingHorizontally.speed;
                speed *= Random.Range(0.9f, 1.1f);
                distance = GameManager.Instance.advance.movingHorizontally.distance;

                startPos = transform.position;
                break;
            case 5:
                sp.sprite = sprites[5];
                speed = GameManager.Instance.advance.movingVertiacally.speed;
                speed *= Random.Range(0.9f, 1.1f);
                distance = GameManager.Instance.advance.movingVertiacally.distance;

                startPos = transform.position;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& other.GetComponent<Rigidbody2D>().velocity.y<0)
        {
            switch (tileType)
            {
                case 0:
                    other.GetComponent<Player>().Jump(1);
                    break;
                case 1:
                    GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                    break;
                case 2:
                    other.GetComponent<Player>().Jump(1);
                    GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                    break;
                case 3:
                    other.GetComponent<Player>().Jump(1.5f);
                    break;
                case 4:
                    other.GetComponent<Player>().Jump(1);
                    break;
                case 5:
                    other.GetComponent<Player>().Jump(1);
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        switch (tileType)
        {
            case 4:
                if (direction==0)
                {
                    transform.Translate(new Vector3(-speed*Time.deltaTime,0));
                    if ((startPos.x-transform.position.x)>distance)
                    {
                        direction = 1;
                    }
                }
                else
                {
                    transform.Translate(new Vector3(speed*Time.deltaTime,0));
                    if ((startPos.x-transform.position.x)<-distance)
                    {
                        direction = 0;
                    }
                }
                break;
            case 5:
                if (direction==0)
                {
                    transform.Translate(new Vector2(0,Time.deltaTime*speed));
                    if ((transform.position.y-startPos.y)>distance)
                    {
                        direction = 1;
                    }
                }
                else
                {
                    transform.Translate(new Vector2(0,Time.deltaTime*-speed));
                    if ((transform.position.y-startPos.y)<-distance)
                    {
                        direction = 0;
                    }
                }
                break;
        }
        if (GameManager.Instance.floor.transform.position.y>=transform.position.y+1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GameManager.Instance.AddInActiveObjectToPool(gameObject,ObjectType.Tile);
        }
        
    }
}
