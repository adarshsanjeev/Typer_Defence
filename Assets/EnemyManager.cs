using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Paladin;
    public GameObject wizard;
    public GameObject archer;
    private float spawnTime = 12f;
    private float spawnTimewizard = 7f;        
    public Transform[] paladinspawnPoints;
    public Transform[] wizardspwanPoints;
    public Transform[] archerspawnpoints;
    int nextnumber = 0;

    void Start()
    {
        InvokeRepeating("SpawnWizard", spawnTimewizard, 0);
        InvokeRepeating("SpawnArcher", 5f, 0);
        InvokeRepeating("SpawnPaladin", spawnTime, spawnTime);
    }

    void SpawnWizard()
    {
        Instantiate(wizard, wizardspwanPoints[0].position, wizardspwanPoints[0].rotation);
    }
    void SpawnArcher()
    {
        Instantiate(archer, archerspawnpoints[0].position, archerspawnpoints[0].rotation);
        Instantiate(archer, archerspawnpoints[1].position, archerspawnpoints[1].rotation);
    }
    void SpawnPaladin()
    {
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        var enemyClone = Instantiate(Paladin, paladinspawnPoints[nextnumber% paladinspawnPoints.Length].position, paladinspawnPoints[nextnumber% paladinspawnPoints.Length].rotation);
        enemyClone.name = "Paladin" + nextnumber;
        nextnumber++;
    }
}