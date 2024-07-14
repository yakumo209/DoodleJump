using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private float distance;
    private Vector3 startPos;
    private int direction;


    public float[] Speed;
    public float[] Distance;
    public Sprite[] Sprites;
    public int enemyType;
    private SpriteRenderer sp;

    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        sp = GetComponent<SpriteRenderer>();
        int.TryParse(gameObject.name,out enemyType);
        sp.sprite = Sprites[enemyType];
        speed = Speed[enemyType];
        distance = Distance[enemyType];
        startPos = transform.position;
    }
    private void Update()
    {
        if (direction==0)
        {
            transform.Translate(Time.deltaTime*-speed*Vector2.right);
            if ((startPos.x- transform.position.x)>distance)
            {
                direction = 1;
                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,
                    transform.localScale.z);
            }
        }
        else
        {
            transform.Translate(Time.deltaTime*speed*Vector2.right);
            if (transform.position.x>startPos.x)
            {
                direction = 0;
                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,
                    transform.localScale.z);
            }
        }
        if (GameManager.Instance.floor.transform.position.y>=transform.position.y+1)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject,ObjectType.Enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            player.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 3;
            player.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Instance.GameOver();
        }

        if (other.CompareTag("Bullet")&&other.GetComponent<Rigidbody2D>().velocity.y>0)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject,ObjectType.Enemy);
            GameManager.Instance.AddInActiveObjectToPool(other.gameObject,ObjectType.Bullet);
            GameManager.Instance.Score+=10;
        }
    }
}
