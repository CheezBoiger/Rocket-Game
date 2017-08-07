using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {

    public void LoadMenu()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(0);
        loading.allowSceneActivation = true;
    }
}
