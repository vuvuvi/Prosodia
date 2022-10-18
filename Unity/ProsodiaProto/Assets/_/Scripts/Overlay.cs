using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Overlay : MonoBehaviour
{
    public GameObject bandTop;
    public GameObject bandDown;
    public TMP_Text Subtitle;
    public TMP_Text Key;
    public AnimationTime animate;
    public bool toHidde;
    private Vector3[] initialPosition = new Vector3[2];
    public TMP_Text[] textControls;
    public NoteInfoProvider NoteInfoProvider;

    private void Start()
    {
        TextControlsRefresh();
        Key.material = new Material(Key.material);
        gameObject.SetActive(true);
    }

    public void TextControlsRefresh()
    {
        for (int i = 0; i < NoteInfoProvider.Colors.Count-1; i++)
        {
            TMP_Text tMP_Text = textControls[i];
            //tMP_Text.color = NoteInfoProvider.Colors[i];
        }
        //textControls[textControls.Length-1].color = Color.white;
    }

    public void SetTextSubtitle(string text)
    {
        Subtitle.text = text;
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
