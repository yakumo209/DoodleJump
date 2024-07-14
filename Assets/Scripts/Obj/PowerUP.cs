using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    public int itemType;
    public SpriteRenderer sp;
    public Sprite[] sprites;
    public GameObject[] used;
    public float[] powerTime;
    private GameObject player;
    private bool fly = false;

    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        sp.enabled = true;
        int.TryParse(gameObject.name, out itemType);
        sp.sprite = sprites[itemType];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            sp.enabled = false;  
            player.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
            used[itemType].SetActive(true);
            fly = true;
            StartCoroutine(StopFly());
        }
    }

    IEnumerator StopFly()
    {
        yield return new WaitForSeconds(powerTime[itemType]);
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<BoxCollider2D>().enabled = true;
        used[itemType].SetActive(false);
        fly = false;
        GameManager.Instance.AddInActiveObjectToPool(gameObject,ObjectType.Item);
    }

    private void Update()
    {
        if (fly)
        {
            player.transform.Translate(new Vector2(0,12*Time.deltaTime));
        }
        else if (GameManager.Instance.floor.transform.position.y>transform.position.y+1)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject,ObjectType.Item);
        }
    }
}
