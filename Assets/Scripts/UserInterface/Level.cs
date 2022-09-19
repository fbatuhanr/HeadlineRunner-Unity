
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public int currentLevel = 1;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = currentLevel.ToString();
    }
}
