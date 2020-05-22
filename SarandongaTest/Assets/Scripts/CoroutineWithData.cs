using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineWithData {
    public Coroutine Coroutine { get; private set; }
    public object result;
    private IEnumerator target;

    public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
        this.target = target;
        this.Coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run() {
        while (target.MoveNext()) {
            result = target.Current;
            yield return result;
        }
    }
}
