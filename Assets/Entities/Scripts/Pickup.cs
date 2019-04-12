using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamemanager;

public class Pickup : MonoBehaviour
{
    private GameObject gem;
    // Start is called before the first frame update
    void Start()
    {
        gem = this.gameObject;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gamemanager.updateCollectibles(gem);
            
        }
    }
}
