    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerScript playerScript;
    private Inventory inventory;

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> enemiesInCombat = new List<GameObject>();

    public float StoryNumber = 0;
    public bool CheckStory = true;
    private static GameManager instance = null;
    
    //singelton pattern
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();

        //save the player at the start of the game
        //playerScript.savePlayer();
    
    }

    private void Update() {
        updateEnemyLists();
    }


    private void updateEnemyLists() {
        //add enemies to list when loading new scene
        if (enemies.Count == 0) {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                enemies.Add(enemy);
            }
        }

        //remove from list if dead
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i].GetComponent<Enemy>().getIsDead()) {
                for (int j = 0; j < enemiesInCombat.Count; j++) {
                    if (enemies[i] == enemiesInCombat[j]) {
                        enemiesInCombat.RemoveAt(j);
                    }
                }
                enemies.RemoveAt(i);
            }
        }

        //if one or more enemies have detected player -> player is in combat
        if(enemiesInCombat.Count > 0) {
            playerScript.setIsInCombar(true);
        }
        else if(enemiesInCombat.Count == 0) {
            playerScript.setIsInCombar(false);
        }


        //add to list 'chasingEnemies' when detected player
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i].GetComponent<Enemy>().getDetectedPlayer()) {
                if(!enemiesInCombat.Contains(enemies[i])) {
                    enemiesInCombat.Add(enemies[i]);
                }
            }

            else if (!enemies[i].GetComponent<Enemy>().getDetectedPlayer()) {
                if(enemiesInCombat.Count != 0) {
                    for (int j = 0; j < enemiesInCombat.Count; j++) {
                        if(enemiesInCombat[j] == enemies[i]) {
                            enemiesInCombat.RemoveAt(j);
                        }
                    }
                }
            }
        }
    }


    public void clearEnemyList() {
        enemies.Clear();
    }



}
