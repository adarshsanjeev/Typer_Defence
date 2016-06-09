using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyManager : MonoBehaviour
{
    private float spawnTime = 5f;
    private float spawnTimewizard = 905f; 
    int nextnumber = 0;

    public EnemySpawner espawn_script;
    public WordTracker wordfuncs_script;
    public WordList worddict_script;
    public int spawn_time;

    void Start()
    {
        //InvokeRepeating("SpawnWizard", spawnTimewizard, 0);
        //InvokeRepeating("SpawnArcher", 600f, 0);
        //InvokeRepeating("SpawnPaladin", spawnTime, spawnTime);
        //InvokeRepeating("SpawnSapper", 9f, 20f);
        //InvokeRepeating("Spawncastleguard1", 8f, 10f);
        //InvokeRepeating("Spawncastleguard2", 3f, 10f);
        espawn_script.InvokeRepeating("RandomGenerate", spawn_time, 10);
    }

    void Update()
    {
        if (Input.anyKey) //Only if a key is pressed
        {
            //autofocus on inputbox
            EventSystem.current.SetSelectedGameObject(wordfuncs_script.inputbox.gameObject, null);
            wordfuncs_script.inputbox.OnPointerClick(new PointerEventData(EventSystem.current));

            wordfuncs_script.onInput();
        }
    }

}