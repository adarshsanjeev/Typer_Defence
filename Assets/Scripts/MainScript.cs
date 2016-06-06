using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainScript : MonoBehaviour {

    List<string> enemy_array = new List<string> { "abcd", "efgh", "lorem", "epsim" }; //will hold all the enemies by word
    List<string> new_array = new List<string> { }; //temporary array to store possible enemy targets based on input text

    public InputField inputbox;

    void Start()
    {
        Debug.Log(inputbox);
        inputbox.text = "script started";
        Debug.Log("Mainscript initiated");
    }

    void Update()
    {
        if(Input.anyKey) //Only if a key is pressed
        {
            onInput();
        }

        //TODO : if timeup create enemy object.
    }

    void onInput()
    {
        new_array.Clear(); //Empty temp list everytime and refresh with new possible targets.
        string input_text = inputbox.text;
        foreach (string enemy_string in enemy_array)
        {
            if (input_text.Length <= enemy_string.Length && input_text.Equals(enemy_string.Substring(0,input_text.Length))) //checks for enemy substrings currently matching input
            {
                new_array.Add(enemy_string);
                if (input_text.Length == enemy_string.Length) //checks if absolute match
                {
                    //TODO : shoot weapon which will destoy object
                    Debug.Log("Found word " + enemy_string);
                    enemy_array.Remove(enemy_string); //remove matched string from enemy_array

                    //Clear all necessary variables/objects
                    new_array.Clear();
                    inputbox.text = "";

                    //TODO : updates scores and stuff here
                    break; //enemy_array updated, exit loop else error
                }
            }
        }
        printList(new_array);
    }

    void printList(List<string> print_list)
    {
        foreach(string temp in print_list)
        {
            Debug.Log(temp);
        }
    }
}
