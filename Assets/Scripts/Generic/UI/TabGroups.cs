using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class TabGroups : MonoBehaviour
{
    [SerializeField]
    //icons for tabs, need button gameobjects as child
    GameObject tabIconHolder;

    [SerializeField]
    // tab contents
    GameObject tabContentHolder;

    private List<Button> tabButtons;

    private List<GameObject> tabPanels = new List<GameObject>();

    private int activePanel;
    void Awake()
    {
        // ugly, TODO FIX
        int buttonIdx = 0;
        foreach (Transform transform in tabIconHolder.transform)
        {
            int idx = buttonIdx;
            transform.GetComponent<Button>().onClick.AddListener(() => EnablePanel(idx));
            buttonIdx++;
        }
        foreach (Transform transform in tabContentHolder.transform)
        {
            tabPanels.Add(transform.gameObject);
            transform.gameObject.SetActive(false);
        }

        //the first tab is the default active tab
        activePanel = 0;
        EnablePanel(activePanel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EnablePanel(int index)
    {
        tabPanels[activePanel].SetActive(false);
        Debug.Log("Panel " + index + " Enabled");
        tabPanels[index].SetActive(true);
        activePanel = index;

    }
}
