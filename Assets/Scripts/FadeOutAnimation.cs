using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FadeOutAnimation : MonoBehaviour
{
    public float Duration;

    [SerializeField]
    GameObject Clouds;

    public void Fade()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FadeOut(canvGroup, canvGroup.alpha, 0));
    }

    public IEnumerator FadeOut(CanvasGroup canvGroup, float start, float end) 
    {
        float counter = 0;
        while(counter < Duration)
        {
            counter += Time.fixedDeltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }

        StartCoroutine(WaitforAnimation());
    }

    private IEnumerator WaitforAnimation()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("test");
    }
}
