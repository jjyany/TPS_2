using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void OnPlayerDead()
    {
        Invoke("ReStart", 5f);
    }

    private void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
