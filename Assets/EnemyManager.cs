using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyManager : MonoBehaviour
{
    public EnemySpawner espawn_script;
    public WordTracker wordfuncs_script;
    public WordList worddict_script;
    public int spawn_time;
    void Start()
    {
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