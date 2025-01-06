using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 3f;
    void Update() 
    {
        // GameManager가 존재하고, 게임 오버 상태라면 더 이상 배경을 움직이지 않음
        if (GameManager.instance != null && GameManager.instance.isGameOver || GameManager.instance.isGameClear)
        {
            return; // 아래 이동 로직을 스킵
        }
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y <= -10.5)
        {
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}
