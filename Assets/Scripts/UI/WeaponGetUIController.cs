using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGetUIController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;
    [SerializeField]
    private WeaponController weaponController;

    [SerializeField] private GameObject leftChoice;
    [SerializeField] private GameObject middleChoice;
    [SerializeField] private GameObject rightChoice;

    [SerializeField] private GameObject leftWepPrefab;
    [SerializeField] private GameObject midWepPrefab;
    [SerializeField] private GameObject rightWepPrefab;

    [SerializeField] private RectTransform leftAnchor;
    [SerializeField] private RectTransform middleAnchor;
    [SerializeField] private RectTransform rightAnchor;

    private GameObject leftInstance;
    private GameObject middleInstance;
    private GameObject rightInstance;

    private Action choiceSelected;

    private void Awake()
    {
        choiceSelected = menuController.newWeaponChosen;
    }

    private void OnEnable()
    {
        if (leftChoice != null)
        {
            leftInstance = Instantiate(leftChoice, leftAnchor);
            leftInstance.GetComponent<Button>().onClick.AddListener(() => {
                weaponController.equipWeapon(leftWepPrefab);
                choiceSelected();
            });
        }
        else
        {
            Debug.Log("left is null");
        }
        if (middleChoice != null)
        {
            middleInstance = Instantiate(middleChoice, middleAnchor);
            middleInstance.GetComponent<Button>().onClick.AddListener(() => {
                weaponController.equipWeapon(midWepPrefab);
                choiceSelected();
            });
        }
        else
        {
            Debug.Log("mid is null");
        }
        if (rightChoice != null)
        {
            rightInstance = Instantiate(rightChoice, rightAnchor);
            rightInstance.GetComponent<Button>().onClick.AddListener(() => {
                weaponController.equipWeapon(rightWepPrefab);
                choiceSelected();
            });
        }
        else
        {
            Debug.Log("right is null");
        }
    }

    private void OnDisable()
    {
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

    public void SetChoices((GameObject left, GameObject leftPanel, GameObject mid, GameObject midPanel, GameObject right, GameObject rightPanel) choices)
    {
        leftChoice = choices.leftPanel;
        leftWepPrefab = choices.left;
        middleChoice = choices.midPanel;
        midWepPrefab = choices.mid;
        rightChoice = choices.rightPanel;
        rightWepPrefab = choices.right;
    }
}
