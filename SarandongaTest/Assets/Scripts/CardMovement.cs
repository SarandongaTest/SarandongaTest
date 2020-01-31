using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour {

    private Vector2 boardPosition;
    private Vector2 offset;


    private void OnMouseDown() {
        boardPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag() {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }

    private void OnMouseUp() {
        transform.position = boardPosition;
    }
}
