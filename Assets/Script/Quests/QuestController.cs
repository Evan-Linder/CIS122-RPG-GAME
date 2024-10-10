using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class QuestController : MonoBehaviour
{
    public GameObject questConvo;
    public GameObject inQuestConvo;
    public GameObject completedQuestConvo;
    public GameObject itemObject;

    public Button yesButton;    
    public Button noButton;
    public Button goodButton;
    public Button giveUpButton;

    public int questReward = 0;
    public bool currentQuest = false;
    public bool hasItem = false;
    public bool questAccepted = false;

    playerScript playerScript;

    void Start()
    {
        playerScript = GetComponent<playerScript>();

        // Add listeners to the Yes and No buttons
        yesButton.onClick.AddListener(AcceptQuest);
        noButton.onClick.AddListener(NoToQuest);
        goodButton.onClick.AddListener(GoodBtn);
        giveUpButton.onClick.AddListener(NoToQuest);
    }

    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!currentQuest && !questAccepted)
            {
                questConvo.SetActive(true);
                inQuestConvo.SetActive(false);
                completedQuestConvo.SetActive(false);
                itemObject.SetActive(false);
            }
            else if (hasItem && currentQuest && questAccepted)
            {
                completedQuestConvo.SetActive(true);
                questConvo.SetActive(false);
                inQuestConvo.SetActive(false);
            }
            else if (currentQuest && questAccepted && !hasItem)
            {
                inQuestConvo.SetActive(true);
                questConvo.SetActive(false);
                completedQuestConvo.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            questConvo.SetActive(false);
            inQuestConvo.SetActive(false);
            completedQuestConvo.SetActive(false);
        }
    }

    public void NoToQuest()
    {
        questConvo.SetActive(false);
        inQuestConvo.SetActive(false);
        completedQuestConvo.SetActive(false);
        currentQuest = false;
        questAccepted = false;
        hasItem = false;
        itemObject.SetActive(false);
    }

    public void AcceptQuest()
    {
        questConvo.SetActive(false);
        inQuestConvo.SetActive(true);
        completedQuestConvo.SetActive(false);
        currentQuest = true;
        questAccepted = true;
        itemObject.SetActive(true);
    }

    public void GoodBtn()
    {
        inQuestConvo.SetActive(false);
    }

    public void CollectGoldBtn()
    {
        completedQuestConvo.SetActive(false);
        playerScript.coinCount += questReward;
        playerScript.UpdateCoinDisplay();
        currentQuest = false;
        questAccepted = false;
        hasItem = false;
        itemObject.SetActive(false);
    }
}


