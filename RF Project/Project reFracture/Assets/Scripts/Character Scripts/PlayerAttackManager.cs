using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    //generate list of random attacks from attack deck
    //(remove attacks as they are drawn from the array)
    GameObject character;

    [SerializeField] PlayerLoadout loadout;

    [SerializeField] int numberOfCards = 5; //adjust this to fit the number of cards available
    public TextMeshProUGUI[] card = new TextMeshProUGUI[3];
    public CardDisplay[] cardDisplays = new CardDisplay[3];

    public int[] attackList; //list of available attacks, shuffled

    public int[] attackQueue = new int[5];

    int currentAttackIndex = 0;

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

        ShuffleDeck();

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
        GenerateNextAttackCycle();
        SortAttackStackID(nextAttackID);
    }
    public void OnAttack2()
    {
        attackQueue[1] = 0;

        UpdateAttackQueue();
        GenerateNextAttackCycle();
        SortAttackStackID(nextAttackID);
    }


    void InitAttackQueue()
    {
        for (int i = 0; i < attackQueue.Length; i++)
        {
            if (attackQueue[i] < 1)
            {
                GenerateNextAttackCycle();
                SortAttackStackID(nextAttackID);
            }
        }
    }

    void GenerateNextAttackCycle()
    {
        if (currentAttackIndex >= attackList.Length)
        {
            if (attackQueue[0] <= 0)
            {
                currentAttackIndex = 0;
                ShuffleDeck();
                InitAttackQueue();
            }
        }
        if (currentAttackIndex < attackList.Length)
            nextAttackID = attackList[currentAttackIndex];
        else
            nextAttackID = 0;
        currentAttackIndex++;
        print(currentAttackIndex);

        //if (attackQueue[0] <= 0)
        //{
        //    ShuffleDeck();
        //    InitAttackQueue();
        //}
    }

    void SortAttackStackID(int attackID)
    {
        //check for empty slots
        if(attackID <= 0)
        {
            attackID = 0;
        }

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

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    void ShuffleDeck()
    {
        for (int i = 0; i < loadout.attackList.Length; i++)
        {
            attackList[i] = loadout.attackList[i];
        }

        for (int i = 0; i < attackList.Length; i++)
        {
            int rand = Random.Range(0, numberOfCards);
            int temp = attackList[rand];
            attackList[rand] = attackList[i];
            attackList[i] = temp;
        }
    }

}
