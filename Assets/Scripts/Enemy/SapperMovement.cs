using UnityEngine;
using System.Collections;
using DigitalRuby.PyroParticles;

public class SapperMovement : MonoBehaviour
{
    Transform MainTower;
    GameObject tower;
    NavMeshAgent nav;
    public GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private FireBaseScript currentPrefabScript;
    private int currentPrefabIndex;

    private Animator myAnimator;
    public Transform[] targetPoints;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Awake()
    {
        tower = GameObject.FindGameObjectWithTag("Player");
        MainTower = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        transform.LookAt(MainTower);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tower)
        {
            Debug.Log("Allahu Akbar");
            sapperExplode();
        }
    }
    public void sapperExplode()
    {
        StopCurrent();
        BeginEffect();
        Destroy(gameObject);
    }
    private void BeginEffect()
    {
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = transform.rotation;
                pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position;// + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other friendly layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FriendlyLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }

    private void StopCurrent()
    {
        // if we are running a constant effect like wall of fire, stop it now
        if (currentPrefabScript != null && currentPrefabScript.Duration > 10000)
        {
            currentPrefabScript.Stop();
        }
        currentPrefabObject = null;
        currentPrefabScript = null;
    }

    void Update()
    {
        int index = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());
        nav.SetDestination(targetPoints[index % targetPoints.Length].position);
    }

}
