using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandle : MonoBehaviour
{
    
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (Input.GetMouseButtonDown(0)) // Left mouse button (or touch)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                SelectCharacter obj = hit.transform.GetComponent<SelectCharacter>();
                MenuManager.Instance.ShowStats(obj);
            }
            return;
        }
        //if (Input.touchCount > 0) // Check if there's at least one touch
        //{
        //    Touch touch = Input.GetTouch(0); // Get the first touch (you can handle multiple touches if needed)

        //    if (touch.phase == TouchPhase.Began) // Check if the touch just began
        //    {
        //        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        //        if (hit.collider != null)
        //        {
        //            SelectCharacter obj = GetComponent<SelectCharacter>();
        //            MenuManager.Instance.ShowStats(obj);
        //        }
        //    }
        //}
    }
}
