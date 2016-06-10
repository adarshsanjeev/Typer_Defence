using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public Transform pausemenu;
    public GameObject Paladin;
    public GameObject wizard;
    public GameObject archer;
    public GameObject sapper;
    public GameObject castleguard1;
    public GameObject castleguard2;

    public WordTracker wordfuncs_script;
    public Wavelist wavescript;
    
    public spawn_point[] meleespawnPoints;
    public spawn_point[] wizardspawnPoints;
    public spawn_point[] archerspawnPoints;
    public spawn_point[] targetspawnPoints;

    public string[] enemy_choice = {"Castleguard1","Castleguard2","Archer","Sapper","Paladin","Wizard"};
    public int[] enemy_count = {0,0,0,0,0,0};
    public int[] wave_list;
    List<int> num_list = new List<int>(); //temporary list

    GameObject glob_obj;
    spawn_point glob_tsp;
    public spawn_point null_point;
    public int level=0;

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
        level = 0;
        loadNextLevel();
        //Debug.Log(Random.Range(1, 1));
    }

    void loadNextLevel()
    {
        level++;
        wavescript.wave_dict.TryGetValue("wave_" + level.ToString(), out wave_list);
        Debug.Log(wave_list);
    }

    int addIntList(int[] temp_list)
    {
        int sum = 0;
        foreach(int num in temp_list)
        {
            sum += num;
        }
        return sum;
    }

    int getRandomFromLeftover(int[] temp_list)
    {
        num_list.Clear();
        for(int i=0;i<temp_list.Length;i++)
        {
            if(temp_list[i] != 0)
            {
                num_list.Add(i);
            }
        }

        return num_list[Random.Range(0, num_list.Count)]; //num_list.Count will always > 0 since wave_list(temp_list) not empty
    }

    void orderedGenerate()
    {
        if(wave_list == null)
        {
            Debug.Log("Game Over");
            return;
        }

        if(addIntList(wave_list) > 0)
        {
            int choice = getRandomFromLeftover(wave_list);
            if (wave_list[choice] > 0)
            {
                int ret = SpawnEnemy(choice);
                if (ret != -1)
                {
                    wave_list[choice]--;
                    wordfuncs_script.AddEnemy(ret, glob_obj, glob_tsp);
                }
            } 
        }
        else if(addIntList(enemy_count) == 0)
        {
            Debug.Log("Call for next level");
            loadNextLevel();
        }
    }

    void RandomGenerate()
    {
        int ret = SpawnEnemy(Random.Range(0,enemy_choice.Length));

        if (ret != -1)
        {
            wordfuncs_script.AddEnemy(ret,glob_obj,glob_tsp);
        }
    }

    public int SpawnEnemy(int choice)
    {
        int tier_no = -1;
        if(enemy_choice[choice].Equals("Wizard"))
        {
            spawn_point temp = getEmptySpawnPoint(wizardspawnPoints);
            if (temp.point != null)
            {
                rangeInstantiate(wizard, temp, "Wizard", choice);
                tier_no = 1;
            }
        }
        else if (enemy_choice[choice].Equals("Archer"))
        {
            spawn_point temp = getEmptySpawnPoint(archerspawnPoints);
            if(temp.point != null)
            {
                rangeInstantiate(archer, temp,"Archer",choice);
                tier_no = 2;
            }
        }
        else 
        {
            spawn_point temp = getEmptySpawnPoint(targetspawnPoints);
            spawn_point anothertemp = meleespawnPoints[Random.Range(0, meleespawnPoints.Length)];

            if(temp.point != null)
            {
                if (enemy_choice[choice].Equals("Paladin"))
                {
                    meleeInstantiate(Paladin, anothertemp, "Paladin", choice, temp);
                    tier_no = 4;
                }
                else if (enemy_choice[choice].Equals("Castleguard1"))
                {
                    meleeInstantiate(castleguard1, anothertemp, "Castleguard1", choice, temp);
                    tier_no = 0;
                }
                else if (enemy_choice[choice].Equals("Castleguard2"))
                {
                    meleeInstantiate(castleguard2, anothertemp, "Castleguard2", choice, temp);
                    tier_no = 1;
                }
                else if (enemy_choice[choice].Equals("Sapper"))
                {
                    meleeInstantiate(sapper, anothertemp, "Sapper", choice, temp);
                    tier_no = 3;
                }
            }
        }
        return tier_no; 
    }

    void rangeInstantiate(GameObject model, spawn_point tsp, string name,int choice)
    {
        var enemyClone = Instantiate(model, tsp.point.position, tsp.point.rotation);
        enemyClone.name = name + enemy_count[choice].ToString(); //removing this line causes FormatException
        glob_obj = (GameObject)enemyClone;
        tsp.inuse = true;
        tsp.choice = choice;
        glob_tsp = tsp;
        enemy_count[choice]++;
    }

    void meleeInstantiate(GameObject model, spawn_point tsp, string name, int choice, spawn_point targ_sp)
    {
        var enemyClone = Instantiate(model, tsp.point.position, tsp.point.rotation);
        enemyClone.name = name + enemy_count[choice].ToString(); //removing this line causes FormatException
        glob_obj = (GameObject)enemyClone;
        targ_sp.inuse = true;
        targ_sp.choice = choice;
        glob_tsp = targ_sp;
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

    void Pause()
    {
        if (pausemenu.gameObject.activeInHierarchy == false)
        {
            pausemenu.gameObject.SetActive(true);
            //Time.timeScale = 0;
        }
        else if (pausemenu.gameObject.activeInHierarchy == true)
        {
            pausemenu.gameObject.SetActive(false);
            // Time.timeScale = 1;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}


