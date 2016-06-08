using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Paladin;
    public GameObject wizard;
    public WordTracker wordfuncs_script;
    public float spawnTime = 3f;
    public float spawnTimewizard = 7f;
    public Transform[] meleespawnPoints;
    public Transform[] wizardspawnPoints;
    public Transform[] rangerspawnPoints;
    public string[] enemy_choice = {"Paladin","Wizard"};
    Collider[] hitcollider;
    int nextnumber = 0;
    int sp_count,enemy_melee_count, enemy_range_count;
    GameObject inst_obj;

    void Start()
    {
        enemy_melee_count = 0;
        enemy_range_count = 0;
    }

    void RandomGenerate()
    {
        //int ret = SpawnEnemy(Random.Range(0,2));
        int ret = SpawnEnemy(0);
        if(ret != -1)
        {
          wordfuncs_script.AddEnemy(ret,inst_obj);
        }
    }

    public int SpawnEnemy(int choice)
    {
        int tier_no = -1;
        if(enemy_choice[choice].Equals("Wizard"))
        {
            sp_count = 0;
            tier_no = 1;
            if(wizardspawnPoints.Length > enemy_range_count)
            {
                inst_obj = Instantiate(wizard, wizardspawnPoints[0].position, wizardspawnPoints[0].rotation) as GameObject;
                enemy_range_count++;
            }
            
        }
        else if(enemy_choice[choice].Equals("Paladin"))
        {
            //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            var enemyClone = Instantiate(Paladin, meleespawnPoints[nextnumber % meleespawnPoints.Length].position, meleespawnPoints[nextnumber % meleespawnPoints.Length].rotation);
            enemyClone.name = "Paladin" + nextnumber; //removing this line causes FormatException
            inst_obj = (GameObject)enemyClone;
            tier_no = 0;
            nextnumber++;
        }

        return tier_no; 
    }
}


//foreach(Transform spoint in wizardspawnPoints)
//{
//    hitcollider = Physics.OverlapSphere(spoint.position,2);
//    if(hitcollider.Length > 0)
//    {
//        Instantiate(wizard, wizardspawnPoints[0].position, wizardspawnPoints[0].rotation);
//        break;
//    }
//    else
//    {
//        Debug.Log("Spawn Point number " + sp_count.ToString() + " is occupied");
//    }
//    sp_count++;
//}