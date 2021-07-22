using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DelayInvisibleUI : MonoBehaviour
{
    [SerializeField] private Image damageUI;
    private float frameDownAlpha = 6 / 255f;
    private Coroutine runCorutine;

    private void Start()
    {
        damageUI = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if(runCorutine != null)
        {
            StopCoroutine(runCorutine);
            runCorutine = null;
        }

        runCorutine = StartCoroutine(DownAlphaValue());
    }

    IEnumerator DownAlphaValue()
    {
        damageUI.color = Color.white;

        while(true)
        {
            yield return null;
            damageUI.color = new Color(damageUI.color.r, damageUI.color.g, damageUI.color.b, damageUI.color.a - frameDownAlpha);
            if(damageUI.color.a <= 0)
            {
                StopCoroutine(runCorutine);
                gameObject.SetActive(false);
            }
        }
    }
}
