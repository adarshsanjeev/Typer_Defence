using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform MainTower;
    GameObject tower;
    //EnemyManager enmg = new EnemyManager();
    
    NavMeshAgent nav;
    private Animator myAnimator;
    public Transform[] targetPoints;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Awake ()
    {
        tower = GameObject.FindGameObjectWithTag("Player");
        MainTower = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <NavMeshAgent> ();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tower )
        {
            myAnimator.SetBool("reachedTower", true);
        }
    }


    void OnTriggerExit(Collider other)
    {
    }

    void Update ()
    {
        if (myAnimator.GetBool("reachedTower") == false)
        {
            int index = int.Parse(gameObject.name[gameObject.name.Length-1].ToString());
            nav.SetDestination(targetPoints[index%targetPoints.Length].position);
        }
        else if(myAnimator.GetBool("reachedTower") == true)
        {
            nav.enabled = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            if(GetComponent<NavMeshObstacle>() == null)
            {
                NavMeshObstacle nvm = gameObject.AddComponent<NavMeshObstacle>();
                nvm.center = new Vector3(0f, 0.92f, 0f);
                nvm.carving = true;
            }
            // rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }

        if (Input.GetKey(KeyCode.K))
        {
            Destroy(this.gameObject);
        }
    }

   
}
