using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField] private GameObject diamond;
    [SerializeField] private Transform roadsParent, character, moneyText;
    
    private void Start()
    {
        // create diamonds on all roads in the game
        for (int i = 1; i < roadsParent.childCount-1; i++)
        {
            Renderer roadMesh = roadsParent.GetChild(i).GetChild(0).GetComponent<Renderer>(); 
            float randomX = Random.Range(roadMesh.bounds.min.x + 0.75f, roadMesh.bounds.max.x - 0.75f);
            float randomZ = Random.Range(roadMesh.bounds.min.z + 0.5f, roadMesh.bounds.max.z - 0.5f);

            Vector3 diamondPosition = new Vector3(randomX, roadMesh.bounds.max.y + 0.35f, randomZ);


            // diamond count on a road.
            int diamondCount = Random.Range(3, 12);

            for (int j = 0; j < diamondCount; j++)
            {
                DrawRay_SpawnDiamond(diamondPosition + new Vector3(0,0,j));
            }
        }
    }

    // check diamond on the ground or not, if so spawn it!
    private void DrawRay_SpawnDiamond(Vector3 diamondPos)
    {
        RaycastHit hit; // check raycast will hit the ground or not?
        if (Physics.Raycast(diamondPos, Vector3.down, out hit)) {
                
            GameObject newDiamond = Instantiate(diamond, diamondPos, Quaternion.identity);
            newDiamond.transform.parent = transform;

            Transform newDiamondModel = newDiamond.transform.GetChild(0);

            newDiamondModel.transform
                .DOMove(newDiamondModel.transform.position + Vector3.up*0.15f, Random.Range(0.5f, 1.5f))
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            newDiamondModel.transform
                .DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
            
        Debug.DrawRay(diamondPos, Vector3.down, Color.green); 
    }
    

    // diamond to money text ui animation
    public void DiamondToMoneyText(Transform diamond)
    {
        diamond.GetChild(0).DOKill();
        diamond.GetChild(0).DOScale(new Vector3(0.05f, 0.05f, 0.05f), 0.5f);
        
        //diamondPos.parent = null;
        
        Vector3 screenPoint = moneyText.position + new Vector3(0,0,7);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        StartCoroutine(DiamondToPointUiCoroutine(diamond, worldPos));
    }
    private IEnumerator DiamondToPointUiCoroutine(Transform diamond, Vector3 worldPos)
    {
        float progress = 0;
        while (true)
        {
            progress += Time.deltaTime;

            if (progress < 0.5f)
            {
                diamond.position = 
                    Vector3.Lerp(
                        diamond.position, 
                        new Vector3(worldPos.x, worldPos.y, character.position.z), 
                        progress);
            }
            else
            {
                Destroy(diamond.gameObject);
                break;
                
            }
            
            yield return null;
        }
        
    }

}
