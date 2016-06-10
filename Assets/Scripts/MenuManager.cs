using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public mainMenu currentmenu;

    public void Start()
    {
//        currentmenu = GameObject.FindGameObjectWithTag("Canvas");
        //currentmenu = GetComponent(GameMenu);
        ShowMenu(currentmenu);
    }

    public void ShowMenu(mainMenu menu)
    {
        if (currentmenu != null)
            currentmenu.isOpen = false;

        currentmenu = menu;
        currentmenu.isOpen = true;
    }

}
