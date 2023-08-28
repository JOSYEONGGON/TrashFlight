using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // 유니티 Inspector에서  값을 넣을 수 있도록 해준다.
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;

    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //키보드
        // 방법 1
        //float horizontalInput = Input.GetAxisRaw("Horizontal"); // 키보드 좌,우
        ////float verticalInput = Input.GetAxisRaw("Vertical");  // 키보드 위,아래
        //Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        //transform.position += moveTo * (moveSpeed * Time.deltaTime);

        // 방법 2
        //Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position -= moveTo;
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position += moveTo;   
        //}

        //마우스
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.Instance.isGameOver == false)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Coin" || collision.gameObject.tag == "boss")
        {
            GameManager.Instance.IncreaseCoin();
            Destroy(collision.gameObject);
        }
    }

    public void UpgradeWeapon()
    {
        weaponIndex += 1;

        if (weaponIndex >= weapons.Length)
        {   
            weaponIndex = weapons.Length - 1;
        }
    }



}
