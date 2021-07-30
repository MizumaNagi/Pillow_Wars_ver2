using UnityEngine;
using UnityEngine.UI;

public class CountChangeText : MonoBehaviour
{
    private Text countText;
    private float remainCountTime;

    private void Start()
    {
        countText = GetComponentInChildren<Text>();
        remainCountTime = GameManager.Instance.ruleData.gameStartCount;
    }

    private void Update()
    {
        countText.text = Mathf.Ceil(remainCountTime).ToString();
        remainCountTime -= Time.deltaTime;

        if (remainCountTime < 0) gameObject.SetActive(false);
    }
}
