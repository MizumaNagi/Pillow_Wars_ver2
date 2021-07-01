using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoserCameraController : MonoBehaviour
{
    [SerializeField] private Camera[] loserCameras;

    private void Start()
    {
        for (int i = 0; i < loserCameras.Length; i++)
        {
            loserCameras[i].enabled = false;
        }

        //for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        //{
        //    loserCameras[i].enabled = true;
        //}

        if (GameManager.Instance.joinPlayers == 2)
        {
            loserCameras[0].rect = new Rect(0, 0.5f, 1, 1);
            loserCameras[1].rect = new Rect(0, 0, 1, 0.5f);
        }

        if (GameManager.Instance.joinPlayers == 4)
        {
            loserCameras[0].rect = new Rect(-0.5f, 0.5f, 1, 1);
            loserCameras[1].rect = new Rect(0.5f, 0.5f, 1, 1);
            loserCameras[2].rect = new Rect(-0.5f, 0, 1, 0.5f);
            loserCameras[3].rect = new Rect(0.5f, 0, 1, 0.5f);
        }
    }
}
