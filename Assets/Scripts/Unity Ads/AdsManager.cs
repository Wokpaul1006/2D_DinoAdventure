using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : Singleton<AdsManager>
{
    private bool isDisableAds;
    void Start()
    {
        isDisableAds = false;
    }
}
