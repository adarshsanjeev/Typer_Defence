using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Paladin;
    public GameObject wizard;
    public GameObject archer;
    public GameObject sapper;
    public GameObject castleguard1;
    public GameObject castleguard2;
    private float spawnTime = 5f;
    private float spawnTimewizard = 905f;        
    public Transform[] paladinspawnPoints;
    public Transform[] wizardspwanPoints;
    public Transform[] archerspawnpoints;
    int nextnumber = 0;
    int sappernumber = 0;
    int guard1 = 0;
    int guard2 = 0;

    public EnemySpawner espawn_script;
    public WordTracker wordfuncs_script;
    public WordList worddict_script;
    public int spawn_time;
    void Start()
    {
        InvokeRepeating("SpawnWizard", spawnTimewizard, 0);
        InvokeRepeating("SpawnArcher", 600f, 0);
        InvokeRepeating("SpawnPaladin", spawnTime, spawnTime);
        InvokeRepeating("SpawnSapper", 9f, 20f);
        InvokeRepeating("Spawncastleguard1", 8f, 10f);
        InvokeRepeating("Spawncastleguard2", 3f, 10f);
        espawn_script.InvokeRepeating("RandomGenerate", spawn_time, 10);
    }
    void Spawncastleguard1()
    {
        var enemyClone = Instantiate(castleguard1, paladinspawnPoints[guard1 % paladinspawnPoints.Length].position, paladinspawnPoints[guard1 % paladinspawnPoints.Length].rotation);
        enemyClone.name = "CastleGuard1" + sappernumber;
        guard1++;
    }
    void Spawncastleguard2()
    {
        var enemyClone = Instantiate(castleguard2, paladinspawnPoints[guard2 % paladinspawnPoints.Length].position, paladinspawnPoints[guard2 % paladinspawnPoints.Length].rotation);
        enemyClone.name = "CastleGuard2" + sappernumber;
        guard2++;
    }

    void SpawnWizard()
    void Update()
    {
        Instantiate(wizard, wizardspwanPoints[0].position, wizardspwanPoints[0].rotation);
    }
        if (Input.anyKey) //Only if a key is pressed
        {
            //autofocus on inputbox
            EventSystem.current.SetSelectedGameObject(wordfuncs_script.inputbox.gameObject, null);
            wordfuncs_script.inputbox.OnPointerClick(new PointerEventData(EventSystem.current));
    void SpawnArcher()
    {
        Instantiate(archer, archerspawnpoints[0].position, archerspawnpoints[0].rotation);
        Instantiate(archer, archerspawnpoints[1].position, archerspawnpoints[1].rotation);
    }
            wordfuncs_script.onInput();
        }
    }
    void SpawnSapper()
    {
        var enemyClone = Instantiate(sapper, paladinspawnPoints[sappernumber % paladinspawnPoints.Length].position, paladinspawnPoints[sappernumber % paladinspawnPoints.Length].rotation);
        enemyClone.name = "Sapper" + sappernumber;
        sappernumber++;
    }

}