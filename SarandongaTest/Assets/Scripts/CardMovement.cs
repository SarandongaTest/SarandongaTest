using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour {

    private Vector3 boardPosition;
    private Rigidbody2D rb;
    public int cardSpeed = 200;

    private Vector3 mousePostion;
    private bool mousePressed;
    private bool moved = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (mousePressed) {
            MoveToObjective(mousePostion);
        } else if (moved) {
            if (Vector3.Distance(transform.position, boardPosition) > 0.25) {
                MoveToObjective(boardPosition);
            } else {
                transform.position = boardPosition;
                transform.localEulerAngles = Vector3.zero;
                rb.velocity = Vector3.zero;
                moved = false;
            }
        }
    }

    /// <summary>
    /// Moves the CardDisplay towards the objective
    /// </summary>
    /// <param name="objective"></param>
    private void MoveToObjective(Vector3 objective) {
        rb.AddForce((objective - transform.position) * cardSpeed);
        Vector3 euler = transform.localEulerAngles;
        euler.x = rb.velocity.y;
        euler.y = -rb.velocity.x;
        transform.localEulerAngles = euler;
    }

    /// <summary>
    /// Set the cardDisplay position in the board
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position) {
        boardPosition = position;
        moved = true;
    }

    private void OnMouseOver() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !mousePressed) {
            UIController.instance.SetInfoCard(true, GetComponent<CardDisplay>().card);
        }
    }

    private void OnMouseDown() {
        UIController.instance.SetInfoCard(false);
        mousePressed = true;
        PlayerHand.instance.RemoveCard(name);
    }

    private void OnMouseDrag() {
        mousePostion = UIController.GetWorldPositionOnPlane(Input.mousePosition, 1);
    }

    private void OnMouseUp() {
        mousePressed = false;
        PlayerHand.instance.AddCard(gameObject);
    }
}
