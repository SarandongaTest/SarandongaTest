using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour {

    private Vector3 boardPosition;
    private Vector3 offset;
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
            rb.AddForce((objective - transform.position) * 200);
            Vector3 euler = transform.localEulerAngles;
            euler.x = rb.velocity.y;
            euler.y = -rb.velocity.x;
            transform.localEulerAngles = euler;
        } else if (moved) {
            if (Vector3.Distance(transform.position, boardPosition) < 0.25) {
                transform.position = boardPosition;
                transform.localEulerAngles = Vector3.zero;
                rb.velocity = Vector3.zero;
                moved = false;
            } else {
                rb.AddForce((boardPosition - transform.position) * 200);
                Vector3 euler = transform.localEulerAngles;
                euler.x = rb.velocity.y;
                euler.y = -rb.velocity.x;
                transform.localEulerAngles = euler;
            }
        }
    }

    public void SetPosition(Vector3 position) {
        boardPosition = position;
        moved = true;
    }

    private void OnMouseDown() {
        mousePressed = true;
        offset = transform.position - GetWorldPositionOnPlane(Input.mousePosition, 0);
        PlayerHand.instance.RemoveCard(name);
    }

    private void OnMouseDrag() {
        objective = GetWorldPositionOnPlane(Input.mousePosition, 0);
    }

    private void OnMouseUp() {
        mousePressed = false;
        PlayerHand.instance.AddCard(gameObject);
        /*rb.velocity = Vector3.zero;
        transform.position = boardPosition;
        transform.localEulerAngles = Vector3.zero;*/
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
