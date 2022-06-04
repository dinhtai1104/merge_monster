using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
   **********************
	Author : Taii
	Company: SuperGame

   **********************
*/

public static class GameEventMaster 
{
	public delegate void GameEvent();
	public static GameEvent onPlacerDeactiveAll;
	public static GameEvent onPlacerActiveAll;
}
