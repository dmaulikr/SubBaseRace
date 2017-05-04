using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    // Destory Animation After Playing
    void DestroyGameObject() {
        Destroy(gameObject);
    }
}
