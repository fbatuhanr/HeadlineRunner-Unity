using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;
    [SerializeField] private UiController _uiController;
    [SerializeField] private DoorController _doorController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private DiamondController _diamondController;

    [SerializeField] private Gameover _gameover;
    [SerializeField] private Money _money;
    [SerializeField] private CollectedWords _collectedWords;
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private bool checkRay = true;
    private void Update()
    {
        if (checkRay)
        {
            if (Physics.SphereCast(transform.position + transform.up, 0.15f, -transform.up, out RaycastHit hit, 1)) {
        
            }
            else
            {
                _playerMovement.isMovementAvailable = false;
                _playerAnimation.SetAnimBool("isFalling", true);
        
                _cameraController.isFollowingAvailable = false;
                _cameraController.PlayerDeathCameraAnimation();
                
                _gameover.Invoke("GameoverScreen", 0.5f);
        
                checkRay = false;
            }
        }
    }
    void OnDrawGizmos()
    {
        bool isHit = Physics.SphereCast(transform.position + transform.up, 0.15f, -transform.up, out RaycastHit hit, 1);
        if (isHit)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }

    private void animationIsPassedFalse()
    {
        _playerAnimation.SetAnimBool("isPassed", false);
    }

    [HideInInspector] public bool doorTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("LeftDoor") || other.CompareTag("RightDoor")) && !doorTrigger)
        {
            _playerAnimation.SetAnimBool("isPassed", true);
            Invoke("animationIsPassedFalse", 0.9f);
            
            if(_doorController.spawnedDoorCount < 5) 
                _doorController.Invoke("StartDoorSpawnCoroutine", _doorController.doorSpawnTimeAfterPass);
            
            _cameraController.PlayerPassedDoorAnimation();
            
            Transform doorTextCanvas = other.transform.parent.GetChild(0).GetChild(0);

            doorTextCanvas.GetChild(0).GetComponent<TextMeshProUGUI>().enableWordWrapping = false;

            string collectedWord = doorTextCanvas.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            string currentCollectedWordText = _collectedWords.GetCollectedWordText();
            string collectedWordResultText = "";


            if (other.transform.parent.parent.parent.CompareTag("nameDoor"))
            {
                collectedWordResultText =
                    currentCollectedWordText.Contains("and") 
                        ? collectedWord + " "
                        : collectedWord + " and ";
            }
            else if (other.transform.parent.parent.parent.CompareTag("verbDoor"))
            {
                collectedWordResultText = "were " + collectedWord + " ";
            }
            else if (other.transform.parent.parent.parent.CompareTag("actionDoor"))
            {
                collectedWordResultText = collectedWord + " ";
            }
            else if (other.transform.parent.parent.parent.CompareTag("placeDoor"))
            {
                collectedWordResultText = collectedWord;
            }

            int wordPoint = int.Parse(doorTextCanvas.GetChild(0).GetChild(0).name);
            _collectedWords.point += wordPoint;
            // string newMoneyText = (int.Parse(_uiController.GetMoneyText())*wordPoint).ToString();
            // _uiController.SetMoneyText(newMoneyText);
            
            _collectedWords.SetCollectedWordText( currentCollectedWordText + collectedWordResultText );
            
            _doorController.MoveDoorTextToNewspaper(doorTextCanvas);
            _doorController.DoorCloseAnimation(other.transform.parent.parent.parent);
            
            doorTrigger = true;
        }
        else if (other.CompareTag("diamond"))
        {
            string newMoneyText = (int.Parse(_money.GetMoneyText()) + 1).ToString();
            _money.SetMoneyText(newMoneyText);

            _diamondController.DiamondToMoneyText(other.transform);
            
            //Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Invoke("GameFinished", 0.5f);
            _collectedWords.HideCollectedWords();
            Debug.Log("finish");
        }
    }

    private void GameFinished()
    {
        _playerMovement.isMovementAvailable = false;
        _playerAnimation.SetAnimBool("isFinished", true);
        
        _uiController.GameFinished();
    }
    
}
