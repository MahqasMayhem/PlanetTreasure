using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    #region Variables
    protected float gravity, playerSpeed, enemySpeed;
    protected int lives;

    private int score;
    private ArrayList sceneTargets;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    #region Public Functions

    public void DamageTarget(GameObject entity)
    {

    }

    public void Kill(GameObject entity)
    {

    }

    public void Pause()
    {

    }
    #endregion
}
