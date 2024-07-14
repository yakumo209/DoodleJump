using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileType;

    public Sprite[] sprites;
    private SpriteRenderer sp;

    private void OnEnable()
    {
        Init();
    }
    

    void Init()
    {
        sp = GetComponent<SpriteRenderer>();
        tileType = int.Parse(gameObject.name);
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
                break;
            case 5:
                sp.sprite = sprites[5];
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
}
