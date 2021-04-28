public class teddy_interact : Dialog {

    bool checkOnce = false;
    // Start is called before the first frame update
    void Awake() {
        initStart(false);
        newDialogLine("Hush! We need to find your brother", 3);  //0=normal 1=sad 2=angry
    }

    // Update is called once per frame
    void Update() {
        TEDTalk();
        dialog();
    }


    private void TEDTalk() {
        if (gm.StoryNumber == 1f) {
            dialogLines.Clear();
            moodSprites.Clear();
            newDialogLine("Woah! It's a big maze.", 3);  //0=normal 1=sad 2=angry
            newDialogLine("You talk?", 0);
            newDialogLine("Hush! We need to find your brother.", 3);  //0=normal 1=sad 2=angry
            checkOnce = true;
        }
        if (gm.StoryNumber == 1.01f) {
            dialogLines.Clear();
            moodSprites.Clear();
            newDialogLine("So far so good!", 0);  //0=normal 1=sad 2=angry
            newDialogLine("Impressive jumping.", 3);  //0=normal 1=sad 2=angry
            newDialogLine("You're doing great buddy!", 3);  //0=normal 1=sad 2=angry
            checkOnce = true;
        }
        if (gm.StoryNumber == 1.02f) {
            dialogLines.Clear();
            moodSprites.Clear();
            newDialogLine("Hmm.. The road seems to split in two.", 0);  //0=normal 1=sad 2=angry
            checkOnce = true;
        }
    }

}



