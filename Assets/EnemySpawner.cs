using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable] //makes public class visible in inspector
    public class spawn_point
    {
        public int id;
        public Transform point;
        public bool inuse;
        public string type;
        public int choice;
    }

    public GameObject Paladin;
    public GameObject wizard;
    public GameObject archer;
    public GameObject sapper;
    public GameObject castleguard1;
    public GameObject castleguard2;

    public WordTracker wordfuncs_script;

    public spawn_point[] meleespawnPoints;
    public spawn_point[] wizardspawnPoints;
    public spawn_point[] archerspawnPoints;
    public spawn_point[] targetspawnPoints;

    public string[] enemy_choice = {"Castleguard1","Castleguard2","Archer","Sapper","Paladin","Wizard"};
    public int[] enemy_count = {0,0,0,0,0,0};
    Collider[] hitcollider;
    GameObject glob_obj;
    spawn_point glob_tsp;
    public spawn_point null_point;
    
    void Awake()
    {
        initspawnpoints(meleespawnPoints,"Melee");
        initspawnpoints(wizardspawnPoints,"Wizard");
        initspawnpoints(archerspawnPoints,"Archer");
        initspawnpoints(targetspawnPoints,"Target");
    }

    void initspawnpoints(spawn_point[] spawn_list,string type)
    {
        for (int i = 0; i < spawn_list.Length; i++)
        {
            spawn_list[i].id = i;
            spawn_list[i].type = type;
            spawn_list[i].inuse = false;
        }
    }
    void Start()
    {
        null_point.id = -1;
        null_point.point = null;
        null_point.inuse = false;
        null_point.type = "null object";
    }

    void RandomGenerate()
    {
        int ret = SpawnEnemy(Random.Range(0,enemy_choice.Length));
        //int ret = SpawnEnemy(3);
        if(ret != -1)
        {
          wordfuncs_script.AddEnemy(ret,glob_obj,glob_tsp);
        }
    }

    public int SpawnEnemy(int choice)
    {
        int tier_no = -1;
        if(enemy_choice[choice].Equals("Wizard"))
        {
            tier_no = 1;
            spawn_point temp = getEmptySpawnPoint(wizardspawnPoints);
            if (temp.point != null)
            {
                meleeInstantiate(wizard, temp, "Wizard", choice);
            }
        }
        else if (enemy_choice[choice].Equals("Archer"))
        {
            spawn_point temp = getEmptySpawnPoint(archerspawnPoints);
            if(temp.point != null)
            {
                meleeInstantiate(archer, temp,"Archer",choice);
            }
            tier_no = 2;
        }
        else 
        {
            spawn_point temp = getEmptySpawnPoint(targetspawnPoints);
            spawn_point anothertemp = meleespawnPoints[Random.Range(0, meleespawnPoints.Length)];

            if(temp.point != null)
            {
                temp.inuse = true;
                if (enemy_choice[choice].Equals("Paladin"))
                {
                    meleeInstantiate(Paladin, anothertemp, "Paladin", choice);
                    tier_no = 4;
                }
                else if (enemy_choice[choice].Equals("Castleguard1"))
                {
                    meleeInstantiate(castleguard1, anothertemp, "Castleguard1", choice);
                    tier_no = 0;
                }
                else if (enemy_choice[choice].Equals("Castleguard2"))
                {
                    meleeInstantiate(castleguard2, anothertemp, "Castleguard2", choice);
                    tier_no = 1;
                }
                else if (enemy_choice[choice].Equals("Sapper"))
                {
                    meleeInstantiate(sapper, anothertemp, "Sapper", choice);
                    tier_no = 3;
                }
            }
        }
        return tier_no; 
    }

    void meleeInstantiate(GameObject model, spawn_point tsp, string name,int choice)
    {
        var enemyClone = Instantiate(model, tsp.point.position, tsp.point.rotation);
        enemyClone.name = name + enemy_count[choice].ToString(); //removing this line causes FormatException
        glob_obj = (GameObject)enemyClone;
        tsp.inuse = true;
        tsp.choice = choice;
        glob_tsp = tsp;
        enemy_count[choice]++;
    }

    spawn_point getEmptySpawnPoint(spawn_point[] spawn_list)
    {
        foreach(spawn_point temp in spawn_list)
        {
            if(!temp.inuse)
            {
                return temp;
            }
        }
        return null_point;
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