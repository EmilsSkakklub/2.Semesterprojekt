using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public PlayerScript player;
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
    }


    private IEnumerator respawnPlayerEnumerator() {
        if(player.HP > 0) {
            player.HP = 0;
            
        }
        if (player.getIsDead()) {
            yield return new WaitForSeconds(2f);
            transition.SetBool("Start", true);

            yield return new WaitForSeconds(1f);
            player.transform.position = new Vector3(player.getSpawnPoint().position.x, player.getSpawnPoint().position.y, player.getSpawnPoint().position.z);
            player.transform.transform.eulerAngles = new Vector3(player.getSpawnPoint().eulerAngles.x, player.getSpawnPoint().eulerAngles.y, player.getSpawnPoint().eulerAngles.z);
            player.heal(8);
            player.setIsDead(false);
            player.setRespawnTimer(0);
            yield return new WaitForSeconds(2f);
            transition.SetBool("Start", false);
  
        }
         
    }

    public void respawnPlayer() {
        StartCoroutine(respawnPlayerEnumerator());
    }


    
}
