using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private GameObject pillowPrefab;

    private Vector3[] spawnPos = { new Vector3(-6f, 0, -6f), new Vector3(6f, 0, 6f), new Vector3(-6f, 0, 6f), new Vector3(6f, 0, -6f), new Vector3(-3f, 0, -3f), new Vector3(3f, 0, 3f), new Vector3(-3f, 0, 3f), new Vector3(3f, 0, -3f) };
    private Vector3[] spawnRot = { new Vector3(0, 45f, 0), new Vector3(0, 225f, 0), new Vector3(0, 135f, 0), new Vector3(0, 315, 0), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
    private Rect[] twoDivCameraRect = { new Rect(0, 0.5f, 1f, 1f), new Rect(0, 0, 1, 0.5f) };

    private Transform playersParent;
    private Transform npcsParent;

    public Transform PillowParent { get; private set; }
    public List<CharacterData> playerDatas = new List<CharacterData>();
    public List<CharacterData> npcDatas = new List<CharacterData>();

    [Header("‚·‚®‚¯‚¹")] public string[] targetBedName = new string[4];

    public void Init()
    {
        GameObject emptyObj = new GameObject();
        playersParent = emptyObj.transform;
        emptyObj.name = "PlayersParent";

        GameObject emptyObj2 = new GameObject();
        npcsParent = emptyObj2.transform;
        emptyObj2.name = "NpcsParent";

        GameObject emptyObj3 = new GameObject();
        PillowParent = emptyObj3.transform;
        emptyObj3.name = "PillowParent";

        int charaIndex = 0;

        // player
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GameObject obj = Instantiate(playerPrefabs[i], spawnPos[charaIndex], Quaternion.Euler(spawnRot[i]));
            obj.transform.GetChild(0).localPosition = Vector3.zero;
            obj.name = "Player" + i;
            obj.transform.SetParent(playersParent, true);

            GameObject pillow = Instantiate(pillowPrefab);
            pillow.name = i.ToString();
            pillow.transform.SetParent(obj.transform);
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            pillow.transform.SetSiblingIndex(2);
            playerDatas.Add(new CharacterData(obj, i, false));

            // TODO:–³‘Ê‚ÈGetcomponent...
            //pillow.AddComponent<PillowController>();
            pillow.GetComponent<PillowController>().characterData = playerDatas[i];

            charaIndex++;

            if (GameManager.Instance.joinPlayers == 2) playerDatas[i].myCamera.rect = twoDivCameraRect[i];
        }
        // npc
        for (int i = 0; i < GameManager.Instance.joinNpcs; i++)
        {
            GameObject obj = Instantiate(npcPrefabs[i], spawnPos[charaIndex], Quaternion.identity);
            obj.transform.GetChild(0).localPosition = Vector3.zero;
            obj.name = "Npc" + (i + 100);
            obj.transform.SetParent(npcsParent, true);

            GameObject pillow = Instantiate(pillowPrefab);
            pillow.name = (i + 100).ToString();
            pillow.transform.SetParent(obj.transform);
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            pillow.transform.SetSiblingIndex(2);
            npcDatas.Add(new CharacterData(obj, i + 100, true));

            pillow.GetComponent<PillowController>().characterData = npcDatas[i];

            charaIndex++;
        }

        InputManager.Instance.characterDatas = playerDatas;
    }

    public void DataReset()
    {
        playerDatas.Clear();
        npcDatas.Clear();
    }

    public void UpdateMethod()
    {
        CoolTimeElapse();
    }

    private void CoolTimeElapse()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            playerDatas[i].remainthrowCT -= Time.deltaTime;
        }

        for (int i = 0; i < GameManager.Instance.joinNpcs; i++)
        {
            npcDatas[i].remainthrowCT -= Time.deltaTime;
        }
    }
}
