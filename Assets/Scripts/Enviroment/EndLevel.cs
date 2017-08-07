using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int loadIndex = SceneManager.GetActiveScene().buildIndex + 1;

        AsyncOperation loading = SceneManager.LoadSceneAsync(loadIndex);
        loading.allowSceneActivation = true;
    }
}
