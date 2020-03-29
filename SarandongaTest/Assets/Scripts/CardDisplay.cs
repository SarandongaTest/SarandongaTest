using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardDisplay : MonoBehaviour {

    public Text cardDescription;

    private void Awake() {
        this.name = GetInstanceID().ToString();
    }

    public abstract void SetDisplays();
}
