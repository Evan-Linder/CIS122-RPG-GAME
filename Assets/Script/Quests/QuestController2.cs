//written by Evan Linder

using UnityEngine;
using UnityEngine.UI;

public class QuestController2 : MonoBehaviour
{
    public GameObject questConvo;
    public GameObject inQuestConvo;
    public GameObject completedQuestConvo;

    public Button yesButton;
    public Button noButton;
    public Button goodButton;
    public Button giveUpButton;
    public Button getGoldBtn;

    public int questReward = 5;
    public bool currentQuest = false;
    public bool hasItem = false;
    public bool questAccepted = false;

    public BoxCollider2D questAreaCollider;


    playerScript playerScript;

    void Start()
    {
        playerScript = FindObjectOfType<playerScript>();

        // add listeners to buttons
        yesButton.onClick.AddListener(AcceptQuest);
        noButton.onClick.AddListener(NoToQuest);
        goodButton.onClick.AddListener(GoodBtn);
        giveUpButton.onClick.AddListener(NoToQuest);
        getGoldBtn.onClick.AddListener(CollectGoldBtn);
    }

    // handle which panel the player sees depending on current progress within the quest.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!currentQuest && !questAccepted)
            {
                questConvo.SetActive(true);
                inQuestConvo.SetActive(false);
                completedQuestConvo.SetActive(false);
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

    // methods to handle functions of the buttons
    public void NoToQuest()
    {
        questConvo.SetActive(false);
        inQuestConvo.SetActive(false);
        completedQuestConvo.SetActive(false);
        currentQuest = false;
        questAccepted = false;
        hasItem = false;
        questAreaCollider.enabled = true;
    }

    public void AcceptQuest()
    {
        questConvo.SetActive(false);
        inQuestConvo.SetActive(true);
        completedQuestConvo.SetActive(false);
        currentQuest = true;
        questAccepted = true;
        questAreaCollider.enabled = false;
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
        questAreaCollider.enabled = true;

    }
}
