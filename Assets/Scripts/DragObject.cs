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

public class DragObject : MonoBehaviour
{
    public Character character;


    private void OnValidate()
    {
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        
    }

    public void OnDrag(Vector3 pos)
    {
        transform.position = pos;
    }

    public Character GetCharacter()
    {
        return character;
    }
}
