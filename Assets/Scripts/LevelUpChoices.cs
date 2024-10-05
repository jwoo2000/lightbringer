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
        leftInstance = Instantiate(leftChoice, leftAnchor);
        leftInstance.GetComponent<Button>().onClick.AddListener(() => choiceSelected());
        middleInstance = Instantiate(middleChoice, middleAnchor);
        middleInstance.GetComponent<Button>().onClick.AddListener(() => choiceSelected());
        rightInstance = Instantiate(rightChoice, rightAnchor);
        rightInstance.GetComponent<Button>().onClick.AddListener(() => choiceSelected());
    }

    private void OnDisable()
    {
        if (leftInstance != null) Destroy(leftInstance);
        if (middleInstance != null) Destroy(middleInstance);
        if (rightInstance != null) Destroy(rightInstance);
    }

    public void SetChoices(GameObject left, GameObject middle, GameObject right)
    {
        leftChoice = left;
        middleChoice = middle;
        rightChoice = right;
    }
}
