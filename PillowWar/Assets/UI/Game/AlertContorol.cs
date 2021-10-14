using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//DefaultExecutionOrder(-100)]
public class AlertContorol : MonoBehaviour
{
    [SerializeField] GameObject AlertPanel;
    [SerializeField] GameObject TimerPanel;

    [SerializeField] TeacherController[] teacherControllersEachStage;

    public Text[] timertexts;
    private int seconds;
    private bool EventTrigger = false;

    private void Start()
    {
        AlertPanel.SetActive(false);
        TimerPanel.SetActive(false);

        foreach(Text timerText in timertexts)
        {
            timerText.enabled = false;
        }

        if(GameManager.Instance.joinPlayers == 2)
        {
            timertexts[0].enabled = true;
            timertexts[1].enabled = true;
        }
        else
        {
            for(int i = 2; i <= 5; i++)
            {
                timertexts[i].enabled = true;
            }
        }
    }

    void Update()
    {
        if (GameEventScript.Instance.isEventStart == true)
        {
            seconds = (int)GameEventScript.Instance.remainEventActiveTime;
            foreach(Text timerText in timertexts)
            {
                if(timerText.enabled == true) timerText.text = seconds.ToString();
            }

            if (EventTrigger == false)
            {
                if (GameManager.Instance.selectStageNo == 0) teacherControllersEachStage[0].ReadyNextEvent();
                else teacherControllersEachStage[1].ReadyNextEvent();

                EventTrigger = true;
                AlertPanel.SetActive(true);
                TimerPanel.SetActive(true);
                StartCoroutine(Corutine()); 
            }

            if (GameEventScript.Instance.remainEventActiveTime <= 5)
            {
                TimerPanel.SetActive(false);
            }
        }
        if (GameEventScript.Instance.triggerEventEnd == true)
        {
            EventTrigger = false;
        }
    }

    private IEnumerator Corutine()
    {
        yield return new WaitForSeconds(3f);

        AlertPanel.SetActive(false);
    }
}
