using UnityEngine;

/**
 * This component chases a given target object.
 */
public class ChaserDijkstra: TargetMover_Dijkstra {
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    public Vector3 TargetObjectPosition() {
        return targetObject.position;
    }

    private void Update() {
        SetTarget(targetObject.position);
    }
}
