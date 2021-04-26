using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        initStart("Gnome",1,10,2, 0.8f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        initUpdate();
    }
}
