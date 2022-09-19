using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private RectTransform screen, screenContent;
    private Vector2[] screenAnchorMinMax = new Vector2[2];
    
    [SerializeField] private LeaderboardContainer _leaderboardContainer;
    [SerializeField] private GameObject leaderboardRow;
    [SerializeField] private DoorController _doorController;
    [SerializeField] private Money _money;
    [SerializeField] private CollectedWords _collectedWords;
    private void Start()
    {
        screenAnchorMinMax[0] = screen.anchorMin;
        screenAnchorMinMax[1] = screen.anchorMax;
    }

    private void LeaderboardScreen()
    {
        int money = int.Parse(_money.GetMoneyText());

        int potentialWordsPoint = _doorController.potentialWordsPoint;
        Debug.Log("Potential: " + potentialWordsPoint);
        float percentage = (float) _collectedWords.point / (float) potentialWordsPoint;
        Debug.Log("Collected: " + _collectedWords.point);
        
        Debug.Log("Percentage: " + percentage);
        int rank = (int)(100 - percentage * 100) + 1;

        Debug.Log("Rank: " + rank);
        
        GameObject rankRow = Instantiate(leaderboardRow, leaderboardRow.transform.position, Quaternion.identity);
        rankRow.transform.parent = screenContent;

        rankRow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rank.ToString();
        rankRow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "player";
        rankRow.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Turkey";
        rankRow.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (money*_collectedWords.point).ToString();
        
        for (int i = 0; i <= 3; i++)
        {
            rankRow.transform.GetChild(i).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            rankRow.transform.GetChild(i).GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
        }


        for (int i = 0; i < 100; i++)
        {
            if (i+1==rank)
            {
                continue;
            }
            GameObject newRow = Instantiate(leaderboardRow, leaderboardRow.transform.position, Quaternion.identity);
            newRow.transform.parent = screenContent;

            newRow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            newRow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _leaderboardContainer.RandomPlayerUsername() + i;
            newRow.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _leaderboardContainer.RandomPlayerCountry();
            newRow.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                i+1<rank 
                    ? ((money*_collectedWords.point+100-i)*1.1f).ToString("F0") 
                    : ((money*_collectedWords.point+100-i)*0.9f).ToString("F0");
        }
        
        rankRow.transform.SetSiblingIndex(rank-1);
        
        screen.GetChild(0).GetComponent<ScrollRect>().verticalNormalizedPosition = (100-rank)*0.01f;
        
        screen.gameObject.SetActive(true);
        
        screen.DOAnchorMin(screenAnchorMinMax[0]+Vector2.down*0.5f, 1f).SetEase(Ease.InOutBack);
        screen.DOAnchorMax(screenAnchorMinMax[1]+Vector2.down*0.5f, 1f).SetEase(Ease.InOutBack);   
    }
}
