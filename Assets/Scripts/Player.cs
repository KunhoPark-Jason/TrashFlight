using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;
    
    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.1f;
    private float lastShotTime = 0f;

    void Update() 
    {
    //    키보드로 플레이어어 컨트롤롤 1
    //    float horizontalInput = Input.GetAxisRaw("Horizontal");
    //    float verticalInput = Input.GetAxisRaw("Vertical");
    //    Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
    //    transform.position += moveTo * moveSpeed * Time.deltaTime;

    //    키보드로 플레이어어 컨트롤롤 2
    //    Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    //    if (Input.GetKey(KeyCode.LeftArrow)){
    //     transform.position -= moveTo;
    //    }
    //    else if (Input.GetKey(KeyCode.RightArrow)){
    //     transform.position += moveTo;
    //    }


    //    마우스 좌표 확인하기기
    //    Debug.Log(Input.mousePosition);

    //    마우스로 플레이어 컨트롤롤
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.instance.isGameOver == false)
        {
            Shoot();
        }
    }

    void Shoot() {
        if (Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.SetGameOver();          
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();         
            Destroy(other.gameObject);
        }
    }

    public void Upgrade()
    {
        weaponIndex ++;
        if (weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length - 1;
        }
    }
}
