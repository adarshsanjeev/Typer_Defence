using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WordTracker : MonoBehaviour {

    public EnemySpawner espawn_script;

    [System.Serializable]
    public struct WordObjectpair
    {
        public string word;
        public GameObject obj;
        public EnemySpawner.spawn_point sp;
    }
    
    public List<WordObjectpair> enemy_array = new List<WordObjectpair> { }; //will hold all the enemies as pairs of word and object
    List<string> new_array = new List<string> { }; //temporary array to store possible enemy targets based on input text

    public InputField inputbox;
    public WordList worddict_script;
    WordObjectpair nullobj;
    string letters = "abcdefghijklmnopqrstuvwxyz";
    string[] word_list;

    void Start()
    {
        Debug.Log(inputbox);
        inputbox.text = "a";
        Debug.Log("Mainscript initiated");
        nullobj.word = null;
        nullobj.obj = null;
    }

    void Update()
    {

    }

    public void AddEnemy(int tier, GameObject eobj,EnemySpawner.spawn_point tsp)
    {
        char c = letters[Random.Range(0, 26)];
        Debug.Log("tier_" + tier.ToString() + "_" + c.ToString());
        worddict_script.worddict.TryGetValue("tier_" + tier.ToString() + "_" + c.ToString(), out word_list); //gets random word list from dictionary

        Debug.Log(word_list);
        WordObjectpair temp;
        temp.word = word_list[Random.Range(0, word_list.Length)];
        temp.obj = eobj;
        temp.sp = tsp;
        enemy_array.Add(temp);
        eobj.GetComponentInChildren<Typer>().msg = temp.word; //sets the text over enemy in GUI
    }

    public void DeleteEnemy(WordObjectpair enem_obj)
    {
        //TODO : call shoot tower weapon
        enemy_array.Remove(enem_obj);
        freeSpawnPoint(enem_obj);
        espawn_script.enemy_count[enem_obj.sp.choice]--;
        //TODO : Destroy respective GameObject
        if (enem_obj.obj.name.Substring(0, 6) == "Sapper")
        {
            enem_obj.obj.GetComponent<NavMeshAgent>().enabled = false;
            SapperMovement sapper = enem_obj.obj.GetComponent<SapperMovement>();
            sapper.StopCurrent();
            sapper.BeginEffect();
            Destroy(enem_obj.obj);
        }
        else
        {
            Animator myAnimator = enem_obj.obj.GetComponent<Animator>();
            myAnimator.SetTrigger("Dead");
            if(enem_obj.obj.GetComponent<NavMeshAgent>() != null)
                enem_obj.obj.GetComponent<NavMeshAgent>().enabled = false;
            enem_obj.obj.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(enem_obj.obj, 3.2f);
        }
        //ScoreManager.score += scoreValue;
    }

    public void freeSpawnPoint(WordObjectpair enem_obj)
    {
        if (enem_obj.sp.type.Equals("Wizard"))
        {
            espawn_script.wizardspawnPoints[enem_obj.sp.id].inuse = false;
        }
        else if (enem_obj.sp.type.Equals("Archer"))
        {
            espawn_script.archerspawnPoints[enem_obj.sp.id].inuse = false;
        }
        else if (enem_obj.sp.type.Equals("Target"))
        {
            espawn_script.targetspawnPoints[enem_obj.sp.id].inuse = false;
        }
    }

    public void onInput()
    {
        new_array.Clear(); //Empty temp list everytime and refresh with new possible targets.
        string input_text = inputbox.text;
        foreach (WordObjectpair enemy_obj in enemy_array)
        {
            if (input_text.Length <= enemy_obj.word.Length && input_text.Equals(enemy_obj.word.Substring(0, input_text.Length))) //checks for enemy substrings currently matching input
            {
                new_array.Add(enemy_obj.word);
                if (input_text.Length == enemy_obj.word.Length) //checks if absolute match
                {
                    //TODO : shoot weapon which will destoy object
                    Debug.Log("Found word " + enemy_obj.word);
                    DeleteEnemy(enemy_obj); //remove matched string from enemy_array

                    //Clear all necessary variables/objects
                    new_array.Clear();
                    inputbox.text = "";

                    //TODO : updates scores and stuff here
                    break; //enemy_array updated, exit loop else error
                }
            }
        }
    }
}
