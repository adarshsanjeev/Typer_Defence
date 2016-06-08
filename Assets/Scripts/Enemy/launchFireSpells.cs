﻿using UnityEngine;
using DigitalRuby.PyroParticles;
using System.Collections;


public class launchFireSpells : MonoBehaviour
{
    public GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private FireBaseScript currentPrefabScript;
    private int currentPrefabIndex;
    Transform MainTower;
    int i = 0;
    void Start()
    {
        int my_layer = gameObject.layer;
        //Physics.IgnoreLayerCollision(my_layer, Prefabs[0].layer, true);
        //Physics.IgnoreLayerCollision(my_layer, Prefabs[1].layer, true);
    }
    void Awake()
    {
        MainTower = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(MainTower);
        StopCurrent();
        BeginEffect();
        NextPrefab();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            NextPrefab();
        }
        else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PreviousPrefab();
        }

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

    public void StartCurrent()
    {
        if(i%2!=0)
        {
            StopCurrent();
            BeginEffect();
        }
        i++;
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

    public void NextPrefab()
    {
        currentPrefabIndex++;
        if (currentPrefabIndex == Prefabs.Length)
        {
            currentPrefabIndex = 0;
        }
    }

    public void PreviousPrefab()
    {
        currentPrefabIndex--;
        if (currentPrefabIndex == -1)
        {
            currentPrefabIndex = Prefabs.Length - 1;
        }
    }
}
