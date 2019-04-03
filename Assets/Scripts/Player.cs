using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    #region Variables
    private GameObject character;
    private bool characterJump;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("2DPlayer");

    }

    // Update is called once per frame
    void Update()
    {
        characterJump = CrossPlatformInputManager.GetButtonDown("Jump");

    }

    void FixedUpdate()
    {

    }
}
