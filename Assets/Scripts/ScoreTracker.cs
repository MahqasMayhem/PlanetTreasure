using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ScoreTracker : MonoBehaviour
{
    #region Variables

    private Gamemanager GameManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Unique Pick Up"))
        {
            foreach (GameObject collectible in GameObject.FindGameObjectsWithTag("Unique Pick Up"))
            {
                if (other.gameObject == collectible)
                {
                    Debug.Log("Player Obtained a unique item!", collectible);
                    GameManager.UpdateCollectibles(collectible);
                    collectible.SetActive(false);
                }
                else Debug.Log("Nice try");
            }
        }
    }
}
