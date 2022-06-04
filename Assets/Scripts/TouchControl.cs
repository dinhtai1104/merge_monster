using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
   **********************
	Author : Taii
	Company: SuperGame

   **********************
*/

public class TouchControl : MonoBehaviour
{
    public LayerMask draggableLayer;
    public LayerMask mapLayer;
    private DragObject currentChoosePlayer;

    private PlacerObject currentPlacer;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, draggableLayer))
            {
                GameEventMaster.onPlacerActiveAll?.Invoke();
                currentChoosePlayer = hit.collider.GetComponent<DragObject>();

                if (currentChoosePlayer)
                {
                    currentChoosePlayer.GetCharacter().SetCollider(false);
                }
                
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (currentChoosePlayer != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mapLayer))
                {
                    Vector3 newPos = hit.point + Vector3.up;
                    
                    currentChoosePlayer.OnDrag(newPos);

                    PlacerObject placerObj = hit.collider.GetComponent<PlacerObject>();
                    if (placerObj != null)
                    {

                        Debug.Log("???");
                        // Check Current placerOb
                        if (currentPlacer == null)
                        {
                            currentPlacer = placerObj;
                            currentPlacer.SetActive(true);
                        } else
                        {
                            currentPlacer.SetActive(false);

                            currentPlacer = placerObj;
                            currentPlacer.SetActive(true);
                        }
                    }

                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {

            // Case 1: Kiểm tra placer hiện tại có character nào không
            if (currentChoosePlayer)
            {
                Character character = currentChoosePlayer.GetCharacter();
                if (character != null)
                {
                    PlacerObject placerObjectOfCurrentChar = character.GetCurrentPlacer();
                    PlacerObject placerObjectCurrent = currentPlacer;

                    // TH1: Placer hiện tại không có character
                    //=> Set Placer cho nhân vật
                    if (placerObjectCurrent.GetCharacterOnGround() == null)
                    {
                        if (placerObjectOfCurrentChar)
                        {
                            placerObjectOfCurrentChar.SetCharacterOnGround(null);
                        }
                        character.SetPlacerObject(currentPlacer);
                        currentPlacer.SetCharacterOnGround(character);
                        currentChoosePlayer.OnDrag(currentPlacer.transform.position + Vector3.up);
                    }
                    else
                    {
                        // TH2: Placer hiện tại có character
                        Character character2 = placerObjectCurrent.GetCharacterOnGround();
                        SwapCharacter(character, character2);

                        //character2.SetPlacerObject(character.GetCurrentPlacer());
                        //character.SetPlacerObject(currentPlacer);

                    }
                    character.SetCollider(true);
                }

                currentPlacer.SetActive(false);
                //Vector3 newPos = currentPlacer.transform.position;
                //newPos.y = 1;

                //currentPlacer.SetCharacterOnGround(character);

                //currentChoosePlayer.GetCharacter().SetCollider(true);
                //currentChoosePlayer.OnDrag(newPos);
            }
            currentChoosePlayer = null;
            currentPlacer = null;
        }
    }


    private void SwapCharacter(Character c1, Character c2)
    {
        PlacerObject placer1 = c1.GetCurrentPlacer();
        PlacerObject placer2 = c2.GetCurrentPlacer();
        c1.SetPlacerObject(placer2);
        c2.SetPlacerObject(placer1);

        placer2.SetCharacterOnGround(c1);
        placer1.SetCharacterOnGround(c2);

        c1.GetComponent<DragObject>().OnDrag(placer2.transform.position + Vector3.up);
        c2.GetComponent<DragObject>().OnDrag(placer1.transform.position + Vector3.up);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 newPos = Input.mousePosition;
        newPos.y = 3;

        return Camera.main.ScreenToWorldPoint(newPos);
    }
}
