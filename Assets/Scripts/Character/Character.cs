using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/* 
   **********************
	Author : Taii
	Company: SuperGame

   **********************
*/

public class Character : MonoBehaviour
{
    private static int ID_Char;
    public static int ID;
    [SerializeField] protected Collider mainCollider;
    protected PlacerObject currentPlacer;
    protected bool isStartGame = false;

    private void Awake()
    {
        ID = ID_Char++;
    }

    public void SetCollider(bool active)
    {
        mainCollider.enabled = active;
    }

    public PlacerObject GetCurrentPlacer()
    {
        return this.currentPlacer;
    }

    public void StartGame()
    {
        // Attach
        isStartGame = true;
    }

    public void SetPlacerObject(PlacerObject newPlacer)
    {
        this.currentPlacer = newPlacer;
    }

    protected virtual void UpdateCharacter()
    {
        
    }

    private void Update()
    {
        if (!isStartGame) return;
        this.UpdateCharacter();
    }



    #region Ability
    protected List<Ability> listAbilies = new List<Ability>();

    public void AddAbility(Ability ability)
    {
        listAbilies.Add(ability);
    }

    public Ability GetAbility(Type typeAbitity)
    {
        foreach (Ability a in listAbilies)
        {
            if (a.GetType() == typeAbitity)
            {
                return a;
            }
        }
        return null;
    }
    #endregion
}
