using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_item : Item
{
    public Inventory inventory;
    public UseKeyAtBigDoor useKey;
    public GameObject bigDoor;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        useKey = GameObject.Find("UseTheKey").GetComponent<UseKeyAtBigDoor>();
        bigDoor = GameObject.Find("BigDoor");
        initStart("Golden key",false, false, "A golden key. Maybe it opens something?");
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }

    public override void useItem() {
        print("KEY USED");
        if (useKey.isIntheBox) {
            print("it worked!");
            bigDoor.SetActive(false);
            setIsConsumable(true);
        }
    }
}
