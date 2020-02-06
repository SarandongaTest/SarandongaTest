using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour {

    private Vector3 boardPosition;
    private Rigidbody2D rb;

    private Vector3 objective;
    private bool mousePressed;
    private bool moved = false;

    private void Awake() {
        boardPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (mousePressed) {
            MoveToObjective();
        } else if (moved) {
            if (Vector3.Distance(transform.position, boardPosition) > 0.25) { 
                /************************/
                objective = boardPosition;
                /* PUEDE
                * BUGEAR
                * *************************/
                MoveToObjective();
            } else {
                transform.position = boardPosition;
                transform.localEulerAngles = Vector3.zero;
                rb.velocity = Vector3.zero;
                moved = false;
            }
        }
    }

    private void MoveToObjective() {
        rb.AddForce((objective - transform.position) * 200);
        Vector3 euler = transform.localEulerAngles;
        euler.x = rb.velocity.y;
        euler.y = -rb.velocity.x;
        transform.localEulerAngles = euler;
    }

    public void SetPosition(Vector3 position) {
        boardPosition = position;
        moved = true;
    }

    private void OnMouseDown() {
        mousePressed = true;
        PlayerHand.instance.RemoveCard(name);
    }

    private void OnMouseDrag() {
        objective = GetWorldPositionOnPlane(Input.mousePosition, 0);
    }

    private void OnMouseUp() {
        mousePressed = false;
        PlayerHand.instance.AddCard(gameObject);
    }

    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        xy.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}
