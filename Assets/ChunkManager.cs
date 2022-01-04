using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{

    public Transform player;
    public float offset = 20f;
   
    // Update is called once per frame
    void Update()
    {
        if(player.position.z - offset > transform.position.z){
            this.gameObject.SetActive(false);
        }        
    }
}
