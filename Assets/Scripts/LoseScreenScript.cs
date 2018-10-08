using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenScript : MonoBehaviour {

    public void ReloadGame()
    {
        SceneManager.LoadScene("Main");
    }
}
