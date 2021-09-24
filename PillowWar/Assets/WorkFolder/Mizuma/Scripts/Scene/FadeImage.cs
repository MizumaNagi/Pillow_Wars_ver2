using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeImage : MonoBehaviour
{
    public enum FadeStatus
    {
        FadeIn,
        FadeOut,
        NONE
    }

    [SerializeField] private Image fadeInImg;
    [SerializeField] private Image fadeOutImg;
    [SerializeField] private Text loadingTxt;
    [SerializeField] private GameObject pillowMovie;

    [SerializeField] private float fadeInCompTime;
    [SerializeField] private float fadeOutStartTimeAfterCompFadeIn;
    [SerializeField] private float fadeOutCompTime;

    private FadeStatus fadeStatus = FadeStatus.NONE;
    private float totalDeltaTime = 0;

    public static SCENE_NAME beforeScene;
    public static SCENE_NAME afterScene;

    private void Start()
    {
        fadeInImg.color = Color.black;
        fadeOutImg.color = Color.clear;

        StartFade(FadeStatus.FadeOut);
        loadingTxt.enabled = false;
    }

    private void StartFade(FadeStatus fadeStatus)
    {
        if (fadeStatus == FadeStatus.FadeIn)
        {
            SceneController.Instance.LoadScene(afterScene);
            loadingTxt.enabled = false;
        }

        totalDeltaTime = 0;
        this.fadeStatus = fadeStatus;
    }

    private bool UpdateFade()
    {
        float deltaTime = Time.deltaTime;
        float chgAlphaValue = deltaTime / fadeInCompTime;
        totalDeltaTime += deltaTime;

        if (fadeStatus == FadeStatus.FadeIn)
        {
            fadeInImg.color -= new Color(0, 0, 0, chgAlphaValue);

            if (totalDeltaTime > fadeInCompTime)
            {
                return true;
            }
            else return false;
        }
        else if (fadeStatus == FadeStatus.FadeOut)
        {
            fadeOutImg.color += new Color(0, 0, 0, chgAlphaValue);

            if (totalDeltaTime > fadeOutCompTime)
            {
                loadingTxt.enabled = true;
                pillowMovie.SetActive(true);
                StartCoroutine(DelayStartFadeIn());
                SceneController.Instance.UnLoadScene(beforeScene);
                return true;
            }
            else return false;
        }
        else
        {
            Debug.LogError("fadeStatusÇ™à”ê}ÇµÇ»Ç¢êîíl");
            return false;
        }
    }

    private IEnumerator DelayStartFadeIn()
    {
        yield return new WaitForSeconds(fadeOutStartTimeAfterCompFadeIn);
        StartFade(FadeStatus.FadeIn);
    }

    private void Update()
    {
        if (fadeStatus != FadeStatus.NONE)
        {
            bool isCompFade = UpdateFade();
            if (isCompFade) fadeStatus = FadeStatus.NONE;
        }
    }
}
