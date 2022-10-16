using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public string MainText = "PRESS";
    public string[] TutosTexts;
    public Overlay overlay;

    public void RefreshText(int index, Color color)
    {
        string text = TutosTexts[index];
        overlay.SetTextSubtitle(MainText + "\n\n" + text);
        overlay.Key.text = "O";
        color.a = 1;
        overlay.Key.color = color;
    }
}
