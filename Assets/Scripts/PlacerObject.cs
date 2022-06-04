using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
   **********************
	Author : Taii
	Company: SuperGame

   **********************
*/

public class PlacerObject : MonoBehaviour
{
	private static int ID = 0;
	private int placerID;
	public MeshRenderer mainMesh;

	public Color normalColor;
	public Color pickerColor;

	protected Character currentCharacterOnGround;
	private void Awake()
	{
		placerID = ID++;

		GameEventMaster.onPlacerActiveAll += this.OnPlacerActiveAll;
		GameEventMaster.onPlacerDeactiveAll += this.OnPlacerDeactiveAll;
	}


	public void SetCharacterOnGround(Character c)
    {
		this.currentCharacterOnGround = c;
    }

	public Character GetCharacterOnGround() => this.currentCharacterOnGround;

    private void OnPlacerDeactiveAll()
    {
		gameObject.SetActive(false);
		SetActive(false);
	}

	private void OnPlacerActiveAll()
    {
		gameObject.SetActive(true);
		SetActive(false);
    }

    public void SetActive(bool active)
    {
		mainMesh.material.color = active ? pickerColor : normalColor;
    }
}
