using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Paladin;
    public GameObject wizard;
    public float spawnTime = 3f;
    public float spawnTimewizard = 7f;        
    public Transform[] spawnPoints;
    public Transform[] wizardspwanPoints;
    int nextnumber = 0;

    void Start()
    {
        InvokeRepeating("SpawWizard", spawnTimewizard, 0);
        InvokeRepeating("SpawnPaladin", spawnTime, spawnTime);
    }

    void SpawWizard()
    {
        Instantiate(wizard, wizardspwanPoints[0].position, wizardspwanPoints[0].rotation);
    }

    void SpawnPaladin()
    {
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        var enemyClone = Instantiate(Paladin, spawnPoints[nextnumber%spawnPoints.Length].position,spawnPoints[nextnumber%spawnPoints.Length].rotation);
        enemyClone.name = "Paladin" + nextnumber;
        nextnumber++;
    }
}