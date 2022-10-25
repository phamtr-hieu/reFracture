using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedLoadoutCheck : MonoBehaviour
{
    [SerializeField] DropSlot[] dropSlots = new DropSlot[5];
    [SerializeField] Button button;
    GameObject loadoutManager;
    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
        loadoutManager = GameObject.FindGameObjectWithTag("LoadoutManager");
        //button.onClick = loadOutManager.GetComponent<SceneLoader>().LoadCardsAndNextLevel();
        button.onClick.AddListener(loadoutManager.GetComponent<SceneLoader>().LoadCardsAndNextLevel);
    }


    private void Update()
    {
        if (dropSlots[0].hasData && dropSlots[1].hasData && dropSlots[2].hasData && dropSlots[3].hasData && dropSlots[4].hasData)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
