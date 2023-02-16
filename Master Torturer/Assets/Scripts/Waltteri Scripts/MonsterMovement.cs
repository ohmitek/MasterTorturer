using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Vector3[] targetPositions;

    private Vector3 startPosition;
    private int targetIndex = 0;

    void Start()
    {
        startPosition = transform.position;
    }

    public void MoveToNextPosition()
    {
            targetIndex = (targetIndex + 1) % targetPositions.Length;
            transform.position = targetPositions[targetIndex];
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        targetIndex = 0;
    }
}
