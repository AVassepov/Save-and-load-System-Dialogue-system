using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionPoint : MonoBehaviour
{
    public int NextSceneIndex;
    public int TeleportaionKey;
    
    private TransitionManager manager;


    private void Awake()
    {
        manager = FindObjectOfType<TransitionManager>();


        
    }   

    private void Start()
    {
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.isTrigger = true;
        if (manager.Key == TeleportaionKey)
        {
            manager.SelectTransitionPoint(transform.parent);
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            manager.Key = TeleportaionKey;
            manager.BeginTransition(NextSceneIndex, SceneManager.GetActiveScene().buildIndex);
        }
    }   
}
