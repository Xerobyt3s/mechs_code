using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] Transform cameraposition;

    void Update() {
        transform.position = cameraposition.position;
    }
}
