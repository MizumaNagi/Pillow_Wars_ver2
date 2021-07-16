using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//DefaultExecutionOrder(-100)]
public class AlertContorol : MonoBehaviour
{
    [SerializeField] GameObject AlertPanel;
    [SerializeField] GameObject TimerPanel;

    public Text timertext;
    private int seconds;

    private bool EventTrigger = false;

    // Update is called once per frame
    void Update()
    {
        if (GameEventScript.Instance.isEventStart == true)
        {
            seconds = (int)GameEventScript.Instance.remainEventActiveTime;
            timertext.text = seconds.ToString();
            if (EventTrigger == false)
            {
                EventTrigger = true;
                AlertPanel.SetActive(true);
                TimerPanel.SetActive(true);
                StartCoroutine(Corutine()); 
            }
        }
        if(GameEventScript.Instance.remainEventActiveTime <= 5)
        {
            TimerPanel.SetActive(false);
        }
        else if(GameEventScript.Instance.remainEventActiveTime <= 0)
        {
            EventTrigger = false;
            Debug.Log("aaa");
        }
    }

    private IEnumerator Corutine()
    {
        yield return new WaitForSeconds(3f);

        AlertPanel.SetActive(false);
    }
}
