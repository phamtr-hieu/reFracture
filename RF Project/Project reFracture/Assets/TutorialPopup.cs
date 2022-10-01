using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] GameObject popup;
    static bool tutorialRead = false;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialRead)
            popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClosePopup()
	{
        popup.SetActive(false);
        tutorialRead = true;

	}
}
