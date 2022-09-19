using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CollectedWords : MonoBehaviour
{
    [SerializeField] private RectTransform words, newspaper;
    [SerializeField] private TextMeshProUGUI wordsText, newspaperText;


    [HideInInspector] public int totalPoint = 0;
    [HideInInspector] public int point;

    private Vector2[]
        wordsAnchMinMax = new Vector2[2],
        newspaperAnchMinMax = new Vector2[2];

    private void Start()
    {
        point = 0;
        
        wordsAnchMinMax[0] = words.anchorMin;
        wordsAnchMinMax[1] = words.anchorMax;

        newspaperAnchMinMax[0] = new Vector2(0.02022118f, 0.008377596f);
        newspaperAnchMinMax[1] = new Vector2(0.9797506f, 0.5847376f);
    }


    private void NewspaperMaximize()
    {
        newspaperText.text = wordsText.text;

        newspaper.DOAnchorMin(newspaperAnchMinMax[0],1.5f).SetEase(Ease.InBounce);
        newspaper.DOAnchorMax(newspaperAnchMinMax[1],1.5f).SetEase(Ease.InBounce);
    }
    
    
    
    public string GetCollectedWordText()
    {
        return wordsText.text;
    }
    public void SetCollectedWordText(string text)
    {
        if (wordsText.text == "") ShowCollectedWords();
        
        wordsText.text = text;
    }
    public void ShowCollectedWords()
    {
        words.DOAnchorMin(
            new Vector2(0.1f, 0.0375f),
            1f
        ).SetEase(Ease.InBounce);
        
        words.DOAnchorMax(
            new Vector2(0.9f, 0.11f),
            1f
        ).SetEase(Ease.InBounce);
    }

    public void HideCollectedWords()
    {
        words.DOAnchorMin(
            wordsAnchMinMax[0],
            1f
        ).SetEase(Ease.InBounce);
        
        words.DOAnchorMax(
            wordsAnchMinMax[1],
            1f
        ).SetEase(Ease.InBounce);
    }
}
