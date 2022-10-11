using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Overlay : MonoBehaviour
{
    public GameObject bandTop;
    public GameObject bandDown;
    public TMP_Text subtitle;
    public AnimationTime animate;
    public bool toHidde;
    private Vector3[] initialPosition = new Vector3[2];

    public void SetTextSubtitle(string text)
    {
        subtitle.text = text;
    }

    public void ShowBands()
    {
        if(animate.State == StateAnime.STARTED) return;
        toHidde = false;
        animate.StartAnimation();
    }

    public void HiddeBands()
    {
        if(animate.State == StateAnime.STARTED) return;
        toHidde = true;
        
        RectTransform rectTop = bandTop.GetComponent<RectTransform>();
        RectTransform rectDown = bandDown.GetComponent<RectTransform>();
        initialPosition[0] = rectTop.localPosition;
        initialPosition[1] = rectDown.localPosition;
        animate.StartAnimation();
    }

    public void UpdateAnimation(float time)
    {
        float progress = time / animate.Duration;

        RectTransform rectTop = bandTop.GetComponent<RectTransform>();
        RectTransform rectDown = bandDown.GetComponent<RectTransform>();

        var direction = toHidde ? progress : 1 - progress;

        var value = new Vector3(0, (rectTop.rect.height/2) * direction, 0);
        rectTop.localPosition = initialPosition[0] + value;
        rectDown.localPosition = initialPosition[1] - value;
    }
}
