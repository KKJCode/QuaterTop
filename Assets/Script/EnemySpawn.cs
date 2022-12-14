using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public Enemy enemyPrefab;
    //public GameObject enemyObj;

    public Transform[] spawnPoints;

    public float damageMax = 40f;
    public float damageMin = 20f;

    public float healthMax = 200f;
    public float healthMin = 100f;

    public float speedMax = 3f;
    public float speedMin = 1f;

    public Color strongEnemyColor = Color.red;

    private List<Enemy> enemies = new List<Enemy> ();
    private int wave;

    private void Update()
    {
        if(GameManager.instance != null&& GameManager.instance.isGameover)
        {
            return;
        }

        if(enemies.Count <=0)
        {
            SpawnWave();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.instance.UpdateWaveText(wave, enemies.Count);
    }

    private void SpawnWave()
    {
        wave++;

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);
        for(int i=0; i<spawnCount; i++)
        {
            float enemyIntensity = Random.Range(0f,1f);
            CreateEnemy(enemyIntensity);

        }
    }

    private void CreateEnemy(float intensity)
    {
        //스킨컬러 > 스케일값 변경
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed =  Mathf.Lerp(speedMin, speedMax, intensity);
        Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.Setup(health, damage, speed,skinColor);
        enemies.Add(enemy);

        enemy.gameObject.GetComponent<BoxCollider>().enabled = true;
        enemy.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        enemy.gameObject.GetComponent<NavMeshAgent>().enabled = true;

        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        enemy.gameObject.layer = 3;
    }
}
