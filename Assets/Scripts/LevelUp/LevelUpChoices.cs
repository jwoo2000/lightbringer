using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpChoices : MonoBehaviour
{
    [SerializeField]
    MenuController menuController;

    [SerializeField] private GameObject leftChoice;
    [SerializeField] private GameObject middleChoice;
    [SerializeField] private GameObject rightChoice;

    [SerializeField] private RectTransform leftAnchor;
    [SerializeField] private RectTransform middleAnchor;
    [SerializeField] private RectTransform rightAnchor;

    private GameObject leftInstance;
    private GameObject middleInstance;
    private GameObject rightInstance;

    private Action choiceSelected;

    private void Awake()
    {
        choiceSelected = menuController.levelUpChoiceSelected;
    }

    private void OnEnable()
    {
        //Debug.Log("enabling level up ui");
        if (leftChoice != null)
        {
            leftInstance = Instantiate(leftChoice, leftAnchor);
            leftInstance.GetComponent<Button>().onClick.AddListener(() => {
                //Debug.Log("left");
                choiceSelected(); 
            });
        } else {
            Debug.Log("left is null");
        }
        if (middleChoice != null)
        {
            middleInstance = Instantiate(middleChoice, middleAnchor);
            middleInstance.GetComponent<Button>().onClick.AddListener(() => {
                //Debug.Log("mid");
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
                //Debug.Log("right");
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
        }
        if (middleInstance != null)
        {
            middleInstance.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(middleInstance);
            middleChoice = null;
        }
        if (rightInstance != null)
        {
            rightInstance.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(rightInstance);
            rightChoice = null;
        }
    }

    public void SetChoices(GameObject left, GameObject middle, GameObject right)
    {
        //Debug.Log("levelupchoices: setting choices");
        leftChoice = left;
        middleChoice = middle;
        rightChoice = right;
    }
}
