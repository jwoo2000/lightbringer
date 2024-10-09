using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpChoices : MonoBehaviour
{
    [SerializeField]
    MenuController menuController;

    [SerializeField]
    PlayerLevelUp playerLevelUp;

    [SerializeField] private GameObject leftChoice;
    [SerializeField] private GameObject middleChoice;
    [SerializeField] private GameObject rightChoice;

    [SerializeField] private int leftChoiceType;
    [SerializeField] private int middleChoiceType;
    [SerializeField] private int rightChoiceType;

    [SerializeField] private RectTransform leftAnchor;
    [SerializeField] private RectTransform middleAnchor;
    [SerializeField] private RectTransform rightAnchor;

    private GameObject leftInstance;
    private GameObject middleInstance;
    private GameObject rightInstance;

    private Action choiceSelected;

    [SerializeField]
    private TextMeshProUGUI levelUpOverflowText;

    private void Awake()
    {
        choiceSelected = menuController.levelUpChoiceSelected;
    }

    private void OnEnable()
    {
        if (menuController.pendingLevelUps > 1)
        {
            levelUpOverflowText.text = "(+"+menuController.pendingLevelUps.ToString()+")";
        } else
        {
            levelUpOverflowText.text = "";
        }
        //Debug.Log("enabling level up ui");
        if (leftChoice != null)
        {
            leftInstance = Instantiate(leftChoice, leftAnchor);
            leftInstance.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log(menuController.choiceTypes[leftChoiceType]);
                playerLevelUp.upgradeStat(leftChoiceType);
                choiceSelected(); 
            });
        } else {
            Debug.Log("left is null");
        }
        if (middleChoice != null)
        {
            middleInstance = Instantiate(middleChoice, middleAnchor);
            middleInstance.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log(menuController.choiceTypes[middleChoiceType]);
                playerLevelUp.upgradeStat(middleChoiceType);
                choiceSelected(); 
            });
        } else
        {
            Debug.Log("mid is null");
        }
        if (rightChoice != null)
        {
            rightInstance = Instantiate(rightChoice, rightAnchor);
            rightInstance.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log(menuController.choiceTypes[rightChoiceType]);
                playerLevelUp.upgradeStat(rightChoiceType);
                choiceSelected(); 
            });
        } else
        {
            Debug.Log("right is null");
        }
    }

    private void OnDisable()
    {
        //Debug.Log("disabling level up ui");
        if (leftInstance != null)
        {
            leftInstance.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(leftInstance);
            leftChoice = null;
            leftChoiceType = -1;
        }
        if (middleInstance != null)
        {
            middleInstance.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(middleInstance);
            middleChoice = null;
            middleChoiceType = -1;
        }
        if (rightInstance != null)
        {
            rightInstance.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(rightInstance);
            rightChoice = null;
            rightChoiceType = -1;
        }
    }

    public void SetChoices(GameObject left, int leftType, GameObject middle, int midType, GameObject right, int rightType)
    {
        //Debug.Log("levelupchoices: setting choices");
        leftChoice = left;
        leftChoiceType = leftType;
        middleChoice = middle;
        middleChoiceType = midType;
        rightChoice = right;
        rightChoiceType = rightType;
    }
}
