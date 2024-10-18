using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpOstacles : MonoBehaviour
{
    private sbyte countToDestroy = 10;
    void Update() => OnMoveForward();
    private void Start()
    {
        StartCoroutine(CountToDestroy());
    }

    IEnumerator CountToDestroy()
    {
        yield return new WaitForSeconds(1);
        countToDestroy -= 1;
        if (countToDestroy <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(CountToDestroy());
    }
    private void OnMoveForward() => transform.position += Vector3.left * Time.deltaTime;
}
