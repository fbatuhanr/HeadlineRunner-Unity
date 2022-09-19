using UnityEngine;
using DG.Tweening;
public class Gameover : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    
    [SerializeField] private RectTransform screen;

    private Vector2[] screenAnchorMinMax = new Vector2[2];

    private void Start()
    {
        screenAnchorMinMax[0] = screen.anchorMin;
        screenAnchorMinMax[1] = screen.anchorMax;
    }
    
    public void GameoverScreen()
    {
        _uiController.DestroyInGameElements();
        
        screen.gameObject.SetActive(true);
        
        screen.DOAnchorMin(screenAnchorMinMax[0]+Vector2.up, 1f).SetEase(Ease.InOutBack);
        screen.DOAnchorMax(screenAnchorMinMax[1]+Vector2.up, 1f).SetEase(Ease.InOutBack);
    }
}
