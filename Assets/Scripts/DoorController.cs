using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform character, newspaper;

    [Range(1, 20)] public float doorSpawnTimeAfterPass = 5;
    [SerializeField] private GameObject door;
    [SerializeField] private SentenceContainer sentences;
    
    private List<string> nameList = new List<string>();
    private Dictionary<string, int> verbList = new Dictionary<string, int>();
    private Dictionary<string, int> placeList = new Dictionary<string, int>();
    private Dictionary<string, int> actionList = new Dictionary<string, int>();
    
    
    [HideInInspector] public int spawnedDoorCount;
    [HideInInspector] public int potentialWordsPoint;
    
    private enum wordLists
    {
        names,verbs,places,actions
    }
    
    
    private void Start()
    {
        potentialWordsPoint = 0;
        
        SetRandomWords(nameList, 4, wordLists.names);
        SetRandomWordsDictionary(verbList, 2, wordLists.verbs);
        SetRandomWordsDictionary(placeList, 2, wordLists.places);
        SetRandomWordsDictionary(actionList, 2, wordLists.actions);

        potentialWordsPoint += verbList.Values.Max();
        potentialWordsPoint += placeList.Values.Max();
        potentialWordsPoint += actionList.Values.Max();
        
        spawnedDoorCount = 0;
        StartCoroutine(SpawnDoor());
    }

    private void SetRandomWordsDictionary(Dictionary<string, int> list, int totalCount, wordLists whichWord)
    {
        string randomWord = "";
        int randomPoint = 0;

        int wordCounter = 0;
        while (wordCounter < totalCount)
        {
            switch (whichWord) {
                case wordLists.verbs: 
                    var randomVerb = sentences.RandomVerbs();
                    randomWord = randomVerb.verbName;
                    randomPoint = (int)randomVerb.verbPoint;
                    break;
                case wordLists.places: 
                    var randomPlace = sentences.RandomPlaces();
                    randomWord = randomPlace.placeName;
                    randomPoint = (int)randomPlace.placePoint;
                    break;
                case wordLists.actions: 
                    var randomAction = sentences.RandomActions();
                    randomWord = randomAction.actionName;
                    randomPoint = (int)randomAction.actionPoint;
                    break;
            }

            if (!list.ContainsKey(randomWord))
            {
                list.Add(randomWord, randomPoint);
                wordCounter++;
            }
        }
    }
    
    private void SetRandomWords(List<string> list, int totalCount, wordLists whichWord)
    {
        string randomWord;
        int counter = 0;
        while(counter < totalCount){
            
            switch (whichWord) {
                case wordLists.names: randomWord = sentences.RandomName(); break;
                default: randomWord = sentences.RandomName(); break;
            }
            
            if( !list.Contains(randomWord) ) {
                list.Add(randomWord);
                counter++;
            }
        }
    }
    
    
    
    public void MoveDoorTextToNewspaper(Transform doorTextCanvas)
    {
        doorTextCanvas.parent = null;
        
        Vector3 screenPoint = newspaper.position + new Vector3(0,0,7);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        StartCoroutine(MoveDoorTextToNewspaperCoroutine(doorTextCanvas, worldPos));
    }
    private IEnumerator MoveDoorTextToNewspaperCoroutine(Transform doorTextCanvas, Vector3 worldPos)
    {
        float progress = 0;
        while (true)
        {
            progress += Time.deltaTime;
            
            doorTextCanvas.position = 
                Vector3.Lerp(
                    doorTextCanvas.position, 
                    new Vector3(worldPos.x, worldPos.y, character.position.z), 
                    progress);

            if (progress >= 0.5f)
            {
                Destroy(doorTextCanvas.gameObject);
                break;
            }
            
            yield return null;
        }
        
    }
    
    
    private void DoorOpenAnimation(Transform thisDoor)
    {
        Vector3 doorEndPosition = new Vector3(thisDoor.position.x, 3.6f, thisDoor.position.z);
            thisDoor
                .DOMove(doorEndPosition, 2f)
                .SetEase(Ease.InOutBack);
    }

    public void DoorCloseAnimation(Transform thisDoor)
    {
        Vector3 doorEndPosition = new Vector3(thisDoor.position.x, 0, thisDoor.position.z);
        thisDoor
            .DOMove(doorEndPosition, 1f)
            .SetEase(Ease.InOutBack)
            .OnComplete(() => Destroy(thisDoor.gameObject));
    }



    
    public void StartDoorSpawnCoroutine() {
        StartCoroutine(SpawnDoor());
    }
    
    private Vector3[] rayDirections = new Vector3[4];
    private IEnumerator SpawnDoor()
    {
        while (true)
        {
            float characterAndDoorOffset = character.position.z + 45f;
            
            rayDirections[0] = new Vector3(-1.85f,4, characterAndDoorOffset-1);
            rayDirections[1] = new Vector3(1.85f,4, characterAndDoorOffset-1);
            rayDirections[2] = new Vector3(-1.85f,4, characterAndDoorOffset+1);
            rayDirections[3] = new Vector3(1.85f,4, characterAndDoorOffset+1);


            if (isDoorHitsGround(rayDirections))
            {
                Vector3 spawnPosition = new Vector3(0, 0, characterAndDoorOffset);
                GameObject spawnedDoor = Instantiate(door, spawnPosition, Quaternion.identity);
                spawnedDoor.transform.parent = transform;
                spawnedDoorCount++;
                SetDoorTexts(spawnedDoor.transform);
                DoorOpenAnimation(spawnedDoor.transform);

                character.GetComponent<PlayerTrigger>().doorTrigger = false;
            
                break;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
    private bool isDoorHitsGround(Vector3[] rayPositions)
    {
        RaycastHit hit;
        foreach (Vector3 rayPosition in rayPositions)
            if (!Physics.Raycast(rayPosition, Vector3.down*10, out hit)) 
                return false; 
        return true;
    }



    private void SetDoorTexts(Transform thisDoor)
    {
        Transform thisLeftDoorTextObject = thisDoor.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        Transform thisRightDoorTextObject = thisDoor.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        
        switch (spawnedDoorCount)
        {
            case 1: 
                thisDoor.tag = "nameDoor";
                thisLeftDoorTextObject.GetComponent<TextMeshProUGUI>().text = nameList[0];
                thisRightDoorTextObject.GetComponent<TextMeshProUGUI>().text = nameList[1];
                break;
            
            case 2:
                thisDoor.tag = "nameDoor";
                thisLeftDoorTextObject.GetComponent<TextMeshProUGUI>().text = nameList[2];
                thisRightDoorTextObject.GetComponent<TextMeshProUGUI>().text = nameList[3];
                break;
            
            case 3:
                thisDoor.tag = "verbDoor";
                thisLeftDoorTextObject.GetComponent<TextMeshProUGUI>().text = verbList.ElementAt(0).Key;
                thisLeftDoorTextObject.GetChild(0).name = verbList.ElementAt(0).Value.ToString();

                thisRightDoorTextObject.GetComponent<TextMeshProUGUI>().text = verbList.ElementAt(1).Key;
                thisRightDoorTextObject.GetChild(0).name = verbList.ElementAt(1).Value.ToString();
                break;
            
            case 4:
                thisDoor.tag = "actionDoor";
                thisLeftDoorTextObject.GetComponent<TextMeshProUGUI>().text = actionList.ElementAt(0).Key;
                thisLeftDoorTextObject.GetChild(0).name = actionList.ElementAt(0).Value.ToString();
                
                thisRightDoorTextObject.GetComponent<TextMeshProUGUI>().text = actionList.ElementAt(1).Key;
                thisRightDoorTextObject.GetChild(0).name = actionList.ElementAt(1).Value.ToString();
                break;
            
            case 5:
                thisDoor.tag = "placeDoor";
                thisLeftDoorTextObject.GetComponent<TextMeshProUGUI>().text = placeList.ElementAt(0).Key;
                thisLeftDoorTextObject.GetChild(0).name = placeList.ElementAt(0).Value.ToString();
                
                thisRightDoorTextObject.GetComponent<TextMeshProUGUI>().text = placeList.ElementAt(1).Key;
                thisRightDoorTextObject.GetChild(0).name = placeList.ElementAt(1).Value.ToString();
                break;
            
            // default: 
            //     break;
        }
        
    }
}
