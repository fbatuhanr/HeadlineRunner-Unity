using UnityEngine;
using DG.Tweening;

public class Settings : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private RectTransform screen;
    
    private Vector2[] screenAnchorMinMax = new Vector2[2];

    private void Start()
    {
        screenAnchorMinMax[0] = screen.anchorMin;
        screenAnchorMinMax[1] = screen.anchorMax;
    }

    public void Settings_Button()
    {
        if (!screen.gameObject.activeSelf)
        {
            screen.gameObject.SetActive(true);

            screen.DOAnchorMin(screenAnchorMinMax[0]+Vector2.up, .5f).SetEase(Ease.InOutBack).SetUpdate(true);
            screen.DOAnchorMax(screenAnchorMinMax[1]+Vector2.up, .5f).SetEase(Ease.InOutBack).SetUpdate(true);
            
            //_playerMovement.isMovementAvailable = false;
            Time.timeScale = 0;
        }
        else
        {
            screen.DOAnchorMin(screenAnchorMinMax[0], .5f).SetEase(Ease.InOutBack).SetUpdate(true);
            screen.DOAnchorMax(screenAnchorMinMax[1], .5f).SetEase(Ease.InOutBack).SetUpdate(true)
                .OnComplete(() => screen.gameObject.SetActive(false));
            
            //_playerMovement.isMovementAvailable = true;
            Time.timeScale = 1;
        }
    }
}
