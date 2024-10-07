using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpUIController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    public bool isNewWep = false;

    [SerializeField] private GameObject leftChoice;
    [SerializeField] private GameObject middleChoice;
    [SerializeField] private GameObject rightChoice;

    [SerializeField] private int leftChoiceType;
    [SerializeField] private int middleChoiceType;
    [SerializeField] private int rightChoiceType;

    [SerializeField] private RectTransform leftAnchor;
    [SerializeField] private RectTransform middleAnchor;
    [SerializeField] private RectTransform rightAnchor;

}
