using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadSceneIndex(int index)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(index);
        loading.allowSceneActivation = false;

        TitleEffect effect = GetComponentInParent<TitleEffect>();
        if (effect) {
            effect.target = effect.startPosition;
            effect.startScene = true;
            effect.loadScene = loading;
        }
    }
}
