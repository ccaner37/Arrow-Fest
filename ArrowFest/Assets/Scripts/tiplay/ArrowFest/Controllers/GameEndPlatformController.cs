using System;
using System.Collections;
using System.Collections.Generic;
using tiplay.ArrowFest.Controllers;
using tiplay.ArrowFest.Managers;
using UnityEngine;

public class GameEndPlatformController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ConsumeArrows(other.transform);
    }

    private void ConsumeArrows(Transform arrows)
    {
        arrows.GetComponent<PlayerArrowController>().RemoveArrows(10 * Mathf.RoundToInt(GameManager.Instance.CoinMultiplier + 0.2f));
        GameManager.Instance.CoinMultiplier += 0.2f;

        if (transform.CompareTag("FinalPlatform"))
        {
            StartCoroutine(GameManager.Instance.LevelCompleted());
        }
    }
}
