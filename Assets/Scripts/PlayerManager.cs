using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    public int lives;
    public int score;

    private List<GameObject> uniqueCollectibles;
    #endregion

    public Text CountText;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        uniqueCollectibles = new List<GameObject>();
        count = 0;
        SetCountText ();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Unique Pick Up"))
        {

            foreach (GameObject collectible in GameObject.FindGameObjectsWithTag("Unique Pick Up"))
            {
                if (other.gameObject == collectible)
                {
                    Debug.Log("Player obtained a unique item!", collectible);
                    UpdateCollectibles(collectible);
                    collectible.SetActive(false);
                    
                }
            }
            count = count + 1;
        }
        else if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
    }

    void UpdateCollectibles(GameObject collectible)
    {
        if (collectible == null)
        {
            Debug.LogWarning("Object is null!");
        }
        uniqueCollectibles.Add(collectible);
        count += 10;
        if (uniqueCollectibles.Count >= 5)
        {
            Debug.Log("All collectibles acquired");

        }
    }

    void SetCountText ()
    {
        CountText.text = "count: " + count.ToString();
    }

    public void OnDamage()
    {
        if (lives <= 0)
        {
            StartCoroutine("OnDeath");
        }
        else lives -= 1;
    }

    public void OnDamage(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            StartCoroutine("OnDeath");
        }
    }

    private IEnumerator OnDeath()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);

    }
}
