using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerUpPrefabs;

    private GameManager GM;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    IEnumerator EnemySpawnRoutine()
    {
        while (GM.gameOver == false)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-6f, +6f), 2.0f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator PowerUpSpawnRoutine()
    {
        while (GM.gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3); //0,1,2
            Instantiate(
                powerUpPrefabs[randomPowerUp],
                new Vector3(Random.Range(-6f, +6f), 6.5f, 0f),
                Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

}





