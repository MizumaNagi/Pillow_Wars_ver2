using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private GameObject pillowPrefab;

    private Vector3[] spawnPos = { new Vector3(-5.5f, 0, -5.5f), new Vector3(5.5f, 0, 5.5f), new Vector3(-5.5f, 0, 5.5f), new Vector3(5.5f, 0, -5.5f), new Vector3(0, 0, 0) };
    private Transform playersParent;
    private Transform npcsParent;

    public Transform PillowParent { get; private set; }
    public List<CharacterData> playerDatas = new List<CharacterData>();
    public List<CharacterData> npcDatas = new List<CharacterData>();

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

        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GameObject obj = Instantiate(playerPrefabs[i], spawnPos[charaIndex], Quaternion.identity);
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
        }

        for(int i = 0; i < GameManager.Instance.joinNpcs; i++)
        {
            GameObject obj = Instantiate(npcPrefabs[i], spawnPos[charaIndex], Quaternion.identity);
            obj.transform.GetChild(0).localPosition = Vector3.zero;
            obj.name = "Npc" + i;
            obj.transform.SetParent(npcsParent, true);

            GameObject pillow = Instantiate(pillowPrefab);
            pillow.name = i + 100.ToString();
            pillow.transform.SetParent(obj.transform);
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            npcDatas.Add(new CharacterData(obj, i, true));

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
