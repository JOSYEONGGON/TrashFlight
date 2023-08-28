using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    } 
        
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime; 

        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
           Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0f)
            {
                if (gameObject.tag == "boss")
                {
                    GameManager.Instance.SetGameOver();
                }

                Destroy (gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }
    }

}
