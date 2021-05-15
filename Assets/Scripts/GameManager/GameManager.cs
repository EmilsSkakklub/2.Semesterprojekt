    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{

    private PlayerScript playerScript;
    private GameObject player;
    private AudioManager audioManager;
    private Animator transition;
    public LevelLightmapData llmd;
    

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> enemiesInCombat = new List<GameObject>();

    public float StoryNumber = 0;
    public bool CheckStory = true;
    private static GameManager instance = null;

    Camera mcam;
    Camera fbcam;
    GameObject tpcam;
    Canvas canvas;

    GameObject hp;
    GameObject stamina;
    GameObject objective;

    public Material SkyLvl0;
    public Material SkyLvl2;
    public Material SkyLvl3;

    Text obText;

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
        llmd = GameObject.Find("LevelLightmapData").GetComponent<LevelLightmapData>();
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        audioManager = GetComponent<AudioManager>();
        transition = GameObject.Find("Crossfade").GetComponent<Animator>();
        mcam = GameObject.Find("Main Camera").GetComponent<Camera>();   
        fbcam = GameObject.Find("FootballCamera").GetComponent<Camera>();
        tpcam = GameObject.Find("Third Person Camera");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        hp = GameObject.Find("HP");
        stamina = GameObject.Find("Stamina");
        objective = GameObject.Find("Objective");
        //obText = GameObject.Find("ObjectiveText").GetComponent<Text>();




        mcam.enabled = true;
        tpcam.SetActive(true);
        fbcam.enabled = false;
    }

    private void Update() {
        updateEnemyLists();
        StartCoroutine(Cutscene1());
        //ObjectiveChanger();
        ImmobilizePlayer();
        MusicManager();
        LightChanger();
    }
    void LightChanger() {
        if (Input.GetKeyDown(KeyCode.I)) {
            llmd.LoadLightingScenario(0);
            Debug.Log("Day");

        }
        if (Input.GetKeyDown(KeyCode.O)) {
            llmd.LoadLightingScenario(1);
            Debug.Log("Evening");
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            llmd.LoadLightingScenario(2);
            Debug.Log("Night");
        }
    }
    IEnumerator Cutscene1() {
        if (StoryNumber == 0.05f || StoryNumber == 0.06f) {
            if (transition.GetBool("Start")) {          
                mcam.enabled = false;
                tpcam.SetActive(false);
                fbcam.enabled = true;
                canvas.worldCamera = fbcam;
            }
        } else {
            mcam.enabled = true;
            tpcam.SetActive(true);
            fbcam.enabled = false;
            canvas.worldCamera = mcam;
        }

        if (fbcam.enabled) {
            playerScript.IsImmobile = true;
            hp.SetActive(false);
            stamina.SetActive(false);
            objective.SetActive(false);
        } else if(!fbcam.enabled && StoryNumber != 0.02f && StoryNumber != 0.08f) {
            playerScript.IsImmobile = false;
            hp.SetActive(true);
            stamina.SetActive(true);
            objective.SetActive(true);
        }
        yield return null;
    }

    public void ImmobilizePlayer() {
        if(StoryNumber == 0.02f) {
            playerScript.IsImmobile = true;
            playerScript.getAnimator().SetBool("isWalking", false);
            playerScript.getAnimator().SetBool("isRunning", false);
            playerScript.getAnimator().SetBool("isCrouching", true);
        }
        else if (StoryNumber == 0.08f) {
            playerScript.IsImmobile = true;
            playerScript.getAnimator().SetBool("isWalking", false);
            playerScript.getAnimator().SetBool("isRunning", false);
            playerScript.getAnimator().SetBool("isCrouching", false);
        }
    }

    /*
    void ObjectiveChanger() {
        switch (StoryNumber) {
        case 0f:
            obText.text = "- Press E to interact" +
                          "\n- Run to the grill!";
            break;
        case 0.01f:
            obText.text = "- Talk to your brother.\n- Go hide.\n\n\nTip: Find some tall grass to crouch in (Press C)";
            break;
        case 0.02f:
            obText.text = "- Stay hidden.";
            break;
        case 0.03f:
            obText.text = "- Spook your brother.";
            break;
        case 0.04f:
            obText.text = "- Get the ball to your brother.";
            break;
        case 0.07f:
            obText.text = "- Talk to your brother.";
            break;
        case 0.09f:
            obText.text = "- Go check on your brother.";
            break;
        case 1.00f:
            obText.text = "- Go through the maze.";
            break;
        case 1.01f:
            obText.text = "- Go through the maze.";
            break;
        case 1.02f:
            obText.text = "- Go through the maze.";
            break;
        default:
            obText.text = "";
            break;
        }
    }
    */
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

    public bool music0Playing;
    public bool music1Playing;
    public bool music2Playing;
    public bool music3Playing;


    public void MusicManager() {
        string level = playerScript.getSpawnPointName();
        if (level.Contains("L0") && !music0Playing) {
            RenderSettings.skybox = SkyLvl0;
            llmd.LoadLightingScenario(0);
            audioManager.Play("Level0Music", true, 0.01f, 1f);
            music0Playing = true;
        }
        else if (!level.Contains("L0")) {
            audioManager.Stop("Level0Music");
            music0Playing = false;
        }

        if (level.Contains("L1") && !music1Playing) {
            audioManager.Play("Level1Music", true, 0.1f, 1f);
            music1Playing = true;
        }
        else if (!level.Contains("L1")) {
            audioManager.Stop("Level1Music");
            music1Playing = false;
        }

        if (level.Contains("L2") && !music2Playing) {
            audioManager.Play("Level2Music", true, 0.1f, 1f);
            RenderSettings.skybox = SkyLvl2;
            llmd.LoadLightingScenario(1);
            music2Playing = true;
        }
        else if (!level.Contains("L2")) {
            audioManager.Stop("Level2Music");
            music2Playing = false;
        }

        if (level.Contains("L3") && !music3Playing) {
            RenderSettings.skybox = SkyLvl3;
            llmd.LoadLightingScenario(2);
            audioManager.Play("Level3Music", true, 0.1f, 0.75f);
            music3Playing = true;
        }
        else if (!level.Contains("L3")) {
            audioManager.Stop("Level3Music");
            music3Playing = false;
        }
    }

}
