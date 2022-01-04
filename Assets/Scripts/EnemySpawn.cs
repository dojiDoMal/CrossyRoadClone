using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
    public GameObject[] birds = new GameObject[3];
    public Transform leftPoint;
    public Transform rightPoint;

    public Facing facing;
    public float minTimeToSpawn = 4f;
    public float maxTimeToSpawn = 10f;
    public float timeToFirstSpawn = 10f;
    
    IEnumerator SpawnEnemy(){
        yield return new WaitForSeconds(timeToFirstSpawn);
        while(true){
            int index = Random.Range(0,3);
            if(birds[index].GetComponent<BirdController>().facingDirection == Facing.LEFT){
                Instantiate(birds[index], rightPoint.position, birds[(int)index].transform.rotation);
            } else if(birds[index].GetComponent<BirdController>().facingDirection == Facing.RIGHT){
                Instantiate(birds[index], leftPoint.position, birds[(int)index].transform.rotation);
            } else{
                Instantiate(birds[index], transform.position, birds[(int)index].transform.rotation);
            }
            yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

}
