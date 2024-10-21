using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsManager : Singleton<AdsManager>
{
    private bool isDisableAds; //Use this for buying no ads in shop
    void Start()
    {
        isDisableAds = false;
    }
}
