using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSC
{
    [SerializeField] List<Scene> sceneList;

    public void LoadScene(int sceneOder, bool isLoad)
    {
        switch (sceneOder)
        {
            case 0:
                SceneManager.LoadScene("00_LoadScene");
                break;
            case 1:
                SceneManager.LoadScene("Jump");
                break;
        }
    }
}
