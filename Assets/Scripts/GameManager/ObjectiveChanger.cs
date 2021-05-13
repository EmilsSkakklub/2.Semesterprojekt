using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveChanger : MonoBehaviour
{
    public Text objectiveText;
    public int storyNumber;

    public List<string> objectives = new List<string>();

    public Item waterGun;
    public Item raceCar;
    public Item racket;
    public Item football;
    public Item littleTeddy;

    public List<Item> toys = new List<Item>();
    public List<Item> toysInInventory = new List<Item>();

    public Enemy neighbor;

    // Start is called before the first frame update
    void Start()
    {
        InitStart();
        InitNewObjectives();
    }

    // Update is called once per frame
    void Update() {
        ShowObjectives(storyNumber);
        CheckToyInInventory();
        ActivateNeighbor();


    }

    private void InitStart() {
        objectiveText = GameObject.Find("ObjectiveText").GetComponent<Text>();
        neighbor = GameObject.Find("Neighbor").GetComponent<Enemy>();

        waterGun = GameObject.Find("waterGun").GetComponent<Item>();
        raceCar = GameObject.Find("ToyRacecar").GetComponent<Item>();
        racket = GameObject.Find("BadmintonRacket").GetComponent<Item>();
        football = GameObject.Find("Football(Item)").GetComponent<Item>();
        littleTeddy = GameObject.Find("littleTeddy").GetComponent<Item>();

        toys.Add(waterGun);
        toys.Add(raceCar);
        toys.Add(racket);
        toys.Add(football);
        toys.Add(littleTeddy);
    }

    //u´sed for the 5 toys
    private void CheckToyInInventory() {
        for(int i = 0; i < toys.Count; i++) {
            if (toys[i].isInInventory && !toysInInventory.Contains(toys[i])) {
                toysInInventory.Add(toys[i]);
                SetStoryNumber(storyNumber + 1);
            }
        }
    }

    public void InitNewObjectives() {
        SetStoryNumber(0);

        NewObjective("- Press E to interact\n- Run to the grill.");
        NewObjective("- Talk to your brother.\n- Find some tall grass to hide in.\n\n-Tip: Press C to crouch.");
        NewObjective("- Stay hidden.\n- Tip: Use your mouse to look around.");
        NewObjective("- Spook your brother.");
        NewObjective("- Get the ball to your brother.");
        NewObjective("");
        NewObjective("");
        NewObjective("- Go check on your brother.");
        NewObjective("");
        NewObjective("- Go through the hole in the hedge.");
        NewObjective("- Go though the maze.\n\nTip: Press right Shift to run."); // 10
        NewObjective("- Explore the backyard.");
        NewObjective("- Find the 5 lost toys (0/5)");
        NewObjective("- Find the 5 lost toys (1/5)");
        NewObjective("- Find the 5 lost toys (2/5)");
        NewObjective("- Find the 5 lost toys (3/5)");
        NewObjective("- Find the 5 lost toys (4/5)");
        NewObjective("- Defeat the neighbor!");
        NewObjective("- Find your brother.");
    }



    private void ActivateNeighbor() {
        if (storyNumber == 17) {
            neighbor.gameObject.SetActive(true);
        }
        else {
            neighbor.gameObject.SetActive(false);
        }
    }



    private void ShowObjectives(int storyNumber) {
        objectiveText.text = objectives[storyNumber];
    }

    private void NewObjective(string objective) {
        objectives.Add(objective);
    }

    public void SetStoryNumber(int newStoryNumber) {
        storyNumber = newStoryNumber;
    }



}
