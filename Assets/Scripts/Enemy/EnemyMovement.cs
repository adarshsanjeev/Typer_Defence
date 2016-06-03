using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform MainTower;
    GameObject tower;
    //EnemyHealth enemyHealth;
    NavMeshAgent nav;
    private Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Awake ()
    {
        tower = GameObject.FindGameObjectWithTag("Player");
        MainTower = GameObject.FindGameObjectWithTag ("Player").transform;
        
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tower)
        {
            myAnimator.SetBool("reachedTower", true);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == tower)
        {
            myAnimator.SetBool("reachedTower", false);
        }
    }

    void Update ()
    {
        if (myAnimator.GetBool("reachedTower") == false)
        {
            nav.SetDestination (MainTower.position);
        }
        else if(myAnimator.GetBool("reachedTower") == true)
        {
            nav.enabled = false;
        }
    }
}
