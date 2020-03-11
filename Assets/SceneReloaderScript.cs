using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloaderScript : MonoBehaviour
{
    [SerializeField] private Animator Scene;
    [SerializeField] private float duration;
    public void ReStart(bool nextScene = false)
    {
        StartCoroutine(ReloadScene(nextScene));
    }
    IEnumerator ReloadScene(bool nextScene = false)
    {
        Scene.SetBool("Close", true);
        yield return new WaitForSeconds(duration);
        if(!nextScene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//reload self 
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
