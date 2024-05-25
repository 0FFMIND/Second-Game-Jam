using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestState
{
    WAIT_FOR_START,
    CAN_START,
    IN_PROGRESS,
    CAN_FINISH,
    FINISHED
}
public class Quest
{
    public QuestInfo info;
    public QuestState currentState;
    private QuestStep currentStep;
    private int currentSteps;
    public Quest(QuestInfo info)
    {
        this.info = info;
        currentState = QuestState.WAIT_FOR_START; 
    }
}
