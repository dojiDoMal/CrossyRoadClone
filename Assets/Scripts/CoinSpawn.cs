using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinSpawn : MonoBehaviour
{
    public GameObject[] coins = new GameObject[4];
    public Transform leftPoint;
    public Transform rightPoint;

    public float minTimeToSpawn = 4f;
    public float maxTimeToSpawn = 10f;
    public float timeToFirstSpawn = 10f;
    
    IEnumerator SpawnCoin(){
        yield return new WaitForSeconds(timeToFirstSpawn);
        while(true){
            int index = Random.Range(0,4);
            int position = Random.Range(0,3);
            switch(position){
                case 0:
                    Instantiate(coins[index], leftPoint.transform.position, coins[index].transform.rotation);
                    break;
                case 1:
                    Instantiate(coins[index], transform.position, coins[index].transform.rotation);
                    break;
                case 2:
                    Instantiate(coins[index], rightPoint.transform.position, coins[index].transform.rotation);
                    break;
            }
            yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));
        }
    }

    void Start()
    {
        StartCoroutine(SpawnCoin());
    }

}
