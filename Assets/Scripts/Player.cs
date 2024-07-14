using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rightBorder;
    public float leftBorder;
    public float moveSpeed = 5f;

    private void Start()
    {
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            Jump(1);
        }

        if (other.CompareTag("Floor"))
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Jump(float x)
    {
        GetComponent<Rigidbody2D>().velocity=Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,12*x),ForceMode2D.Impulse);
    }

    private Vector3 move;
    private void Update()
    {
        if (GameManager.Instance.gameState==GameState.Running)
        {
            move.x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            if (move.x>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (move.x<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            transform.Translate(move,Space.Self);
            if (transform.position.x<leftBorder)
            {
                transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
            }
            if (transform.position.x>rightBorder)
            {
                transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
            }
        }
    }
}
