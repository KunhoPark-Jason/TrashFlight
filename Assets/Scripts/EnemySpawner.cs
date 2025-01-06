using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = {-2f, -1f, 0, 1f, 2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // 보스가 이미 소환되었는지 확인하는 플래그
    private bool hasSpawnedBoss = false;
    void Start()
    {
        StartEnemyRoutine();
    }

    void ShuffleArray(float[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            float temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        float moveSpeed = 5f;
        int enemyIndex = 0;
        int spawnCount = 0;
        while(true)
        {
            // foreach(float posX in arrPosX)
            // {
            //     SpawnEnemy(posX, enemyIndex, moveSpeed);
            // }

            int enemyCountEachRound = Random.Range(3, 6); // 3 ~ 5

            // arrPosX를 안전하게 복사해서 섞기
            float[] shufflePosX = (float[])arrPosX.Clone();
            ShuffleArray(shufflePosX);

            // 맨 앞에서 enemyCountEachRound 개만큼 위치를 사용
            for (int i = 0; i < enemyCountEachRound; i++)
            {
                SpawnEnemy(shufflePosX[i], enemyIndex, moveSpeed);
            }

            
            spawnCount ++;

            if (spawnCount % 20 == 0)
            {
                enemyIndex ++;
                yield return new WaitForSeconds(5f);
                moveSpeed += 2;
            }

            // enemyIndex가 배열 크기를 넘어서면 보스를 소환하되, 
            // 아직 보스를 소환하지 않았다면 한 번만 소환
            if (enemyIndex >= enemies.Length && !hasSpawnedBoss)
            {
                SpawnBoss();
                hasSpawnedBoss = true; // 보스를 소환했음을 표시
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0)
        {
            index ++;
        }
        if (index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
