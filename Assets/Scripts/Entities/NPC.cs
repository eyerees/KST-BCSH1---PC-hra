using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private System.Action onDialogueFinished;

    private enum QuestState { NotStarted, InProgress, Completed, HandedIn }
    private QuestState questState = QuestState.NotStarted;
    public List<QuestReward> questRewards = new List<QuestReward>();

    void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public bool CanInteract() => !isDialogueActive;

    public void Interact(System.Action onFinished)
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        onDialogueFinished = onFinished;
        if (isDialogueActive) NextLine();
        else StartDialogue();
    }

    void StartDialogue()
    {
        SyncQuestState();

        if (questState == QuestState.HandedIn)
            dialogueIndex = dialogueData.questCompletedIndex;
        else if (questState == QuestState.InProgress && QuestController.Instance.IsQuestCompleted(dialogueData.quest.questID))
            dialogueIndex = dialogueData.questCompletedIndex;
        else if (questState == QuestState.InProgress)
            dialogueIndex = dialogueData.questInProgressIndex;
        else
            dialogueIndex = 0;

        isDialogueActive = true;
        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPotrait);
        dialogueUI.ShowDialogueUI(true);
        PauseController.SetPause(true);
        DisplayCurrentLine();
    }

    private void SyncQuestState()
    {
        if (dialogueData.quest == null) return;
        string questID = dialogueData.quest.questID;

        if (QuestController.Instance.IsQuestHandedIn(questID))
            questState = QuestState.HandedIn;
        else if (QuestController.Instance.IsQuestActive(questID))
            questState = QuestState.InProgress;
        else
            questState = QuestState.NotStarted;
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
            return;
        }

        if (questState == QuestState.HandedIn && dialogueIndex >= dialogueData.dialogueLines.Length - 1)
        {
            EndDialogue();
            return;
        }

        dialogueUI.ClearChoices();

        if (dialogueData.endProgressLines.Length > dialogueIndex && dialogueData.endProgressLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
            DisplayCurrentLine();
        else
            EndDialogue();
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text + letter);
            SoundEffectManager.PlayVoice(dialogueData.voiceSound, dialogueData.voicePitch);
            yield return new WaitForSecondsRealtime(dialogueData.typingSpeed);
        }

        isTyping = false;
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSecondsRealtime(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        int count = Mathf.Min(choice.choices.Length, choice.nextDialogueIndexes.Length, choice.givesQuest.Length);

        for (int i = 0; i < count; i++)
        {
            bool isQuestGiver = choice.givesQuest[i];

            if (isQuestGiver && questState != QuestState.NotStarted) continue;

            int nextIndex = choice.nextDialogueIndexes[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex, isQuestGiver));
        }
    }

    void ChooseOption(int nextIndex, bool givesQuest)
    {
        if (givesQuest)
        {
            QuestController.Instance.AcceptQuest(dialogueData.quest);
            questState = QuestState.InProgress;
        }

        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    void EndDialogue()
    {
        if (questState == QuestState.InProgress && QuestController.Instance.IsQuestCompleted(dialogueData.quest.questID))
        {
            string questID = dialogueData.quest.questID;

            // Give NPC-defined rewards before handing in (quest is still in activateQuests)
            if (questRewards != null && questRewards.Count > 0)
                foreach (QuestReward reward in questRewards)
                    RewardController.Instance.GiveQuestRewards(reward);

            QuestController.Instance.HandInQuest(questID);
            questState = QuestState.HandedIn;
        }

        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        PauseController.SetPause(false);
        onDialogueFinished?.Invoke();
        onDialogueFinished = null;
    }
}

[System.Serializable]
public class QuestReward
{
    public RewardType type;
    public int rewardID;
    public int amount = 1;
}

public enum RewardType { Item, Gold, Experience, Custom }