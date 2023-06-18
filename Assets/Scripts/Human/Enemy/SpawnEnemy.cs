using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject[] Enemy;
    public float spawnTime;
    public List<GameObject> spawnPoints;
    public TMP_Text EnemyScoreText;
    
    public int EnemyScore;

    public int en_0;
    public int en_1;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.1f, spawnTime);
    }

    public void Update()
    {
        EnemyScoreText.text = "Score: " + EnemyScore;
    }

    void Spawn()
    {
        if (playerController.currentHealth <= 0f)
            return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);

        int I = Random.Range(0, 2);
        SpawnEnemyBot(I, spawnPointIndex);
    }

    private void SpawnEnemyBot(int enemyID, int spawnPointIndex)
    {
        Debug.Log(enemyID);
        Debug.Log(spawnPointIndex);
        Instantiate(Enemy[enemyID], spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
    }
    
}
