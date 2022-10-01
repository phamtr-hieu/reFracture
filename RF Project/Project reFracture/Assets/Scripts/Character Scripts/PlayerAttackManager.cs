using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerAttackManager : MonoBehaviour
{

    //generate list of random attacks from attack deck
    //(remove attacks as they are drawn from the array)
    GameObject character;

    [SerializeField] PlayerLoadout loadout;

    [SerializeField] int numberOfCards = 5; //adjust this to fit the number of cards available
    public TextMeshProUGUI[] card = new TextMeshProUGUI[3];
    public CardDisplay[] cardDisplays = new CardDisplay[3];

    public int[] attackList; //list of available attacks 

    public int[] attackQueue = new int[3];

    int currentAttack = 0;

    int nextAttackID;

    bool dying = false;

    private void Start()
    {
        loadout = GameObject.FindGameObjectWithTag("LoadoutManager").GetComponent<PlayerLoadout>();

        attackList = new int[numberOfCards];

        character = GameObject.FindGameObjectWithTag("Player");
        //setting player's possible attacks 
        // 0 = null
        //todo: optimize and automate this process based on some datasheet
        for (int i = 0; i < attackList.Length; i++) //currently just filling in all the cards, adjust to be a customizable array
        {
            attackList[i] = i + 1;
        }

        //sets all attacks to 0 (no attacks)
        for (int i = 0; i < attackQueue.Length; i++)
        {
            attackQueue[i] = 0;
        }

        //run init code for attack queue
        InitAttackQueue();
    }

    private void Update()
    {
        DisplayAttackID();
        if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetGame();
        }

        //Death handling
        
        if (character.GetComponent<Character>().healthPoint <= 0 && !dying)
        {
            character.GetComponent<Character>().OnDeath();
            Invoke("NextScene", 3.5f);
            dying = true;

        }


    }

    public void OnAttack()
    {
        attackQueue[0] = 0;

        UpdateAttackQueue();
        GenerateNextAttack();
        SortAttackStackID(nextAttackID);
    }

    void InitAttackQueue()
    {
        for (int i = 0; i < attackQueue.Length; i++)
        {
            if (attackQueue[i] < 1)
            {
                GenerateNextAttack();
                SortAttackStackID(nextAttackID);
            }
        }
    }

    void GenerateNextAttack()
    {
        //todo: add sorting system to cycle through a list of attacks without repeating
        if (currentAttack >= loadout.attackList.Length)
        {
            currentAttack = 0;
        }
        nextAttackID = loadout.attackList[currentAttack];
        currentAttack++;
        
    }

    void SortAttackStackID(int attackID)
    {
        //check for empty slots
        int stack = 0;
        foreach (int i in attackQueue)
        {
            if (i > 0)
                stack++;
        }

        attackQueue[stack] = attackID;
    }

    void UpdateAttackQueue()
    {
        for (int i = 0; i < attackQueue.Length; i++)
        {
            if (i < attackQueue.Length - 1)
            {
                if (attackQueue[i] < 1 && attackQueue[i + 1] != 0)
                {
                    attackQueue[i] = attackQueue[i + 1];
                    attackQueue[i + 1] = 0;
                }
            }
            else if (i == attackQueue.Length - 1)
            {
                attackQueue[i] = 0;
            }
        }
    }

    void DisplayAttackID()
    {
        for (int i = 0; i < attackQueue.Length; i++)
        {
            //Debug.LogWarning(i);
            //card[i].text = attackQueue[i].ToString();
            cardDisplays[i].cardID = attackQueue[i];
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    
    void NextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
