using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateCtr : MonoBehaviour
{



    private string nextSceneName;

    public string NextSceneName
    {
        get { return nextSceneName; }
        set { nextSceneName = value; }
    }

    [SerializeField] private bool isGateOn;
    public bool IsGateOn
    {
        get { return isGateOn; }
        set { isGateOn = value; }
    }
    [SerializeField]
    private bool isPlayerOnGate;

    public bool IsPlayerOnGate
    {
        get { return isPlayerOnGate; }
    }

    public void NextScene(string _sceneName)
    {
        if (IsGateOn && isPlayerOnGate)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
    


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isPlayerOnGate = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isPlayerOnGate = false;
        }
    }
}
