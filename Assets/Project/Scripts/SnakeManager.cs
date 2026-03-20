using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using System;

namespace DragonWorm {
    public class SnakeManager : MonoBehaviour {
        private SplineContainer path;
        [SerializeField] private GameObject bodySegmentPrefab;
        private List<GameObject> activeSegments = new List<GameObject>();

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float turnSpeed = 450f; // Degrees per second
        [SerializeField] private float deadzoneRadius = 0.25f;

        [Header("Snake Settings")]
        [SerializeField] private float segmentSpacing = 1.0f;
        [SerializeField] private float knotResolution = 0.2f;
        [SerializeField] private int initialSize = 5;

        private Vector3 targetWorldPos;
        private bool isMoving = false;

        private void Start() {
            InitializeSpline();
            CreateInitialBody();
        }

        void InitializeSpline() {
            GameObject splineObj = new GameObject("SnakePath_World");
            path = splineObj.AddComponent<SplineContainer>();

            path.Spline.Clear();
            float3 localStart = path.transform.InverseTransformPoint(transform.position);
            path.Spline.Add(new BezierKnot(localStart));
        }

        void CreateInitialBody() {
            for (int i = 0; i < initialSize; i++) {
                GameObject segment = Instantiate(bodySegmentPrefab, transform.position, Quaternion.identity);
                activeSegments.Add(segment);
            }
        }

        private void Update() {
            UpdateTargetPosition();
            MoveHead();
            UpdateSplinePath();
        }

        private void UpdateTargetPosition() {
            if (Mouse.current == null) { return; }

            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            targetWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
            targetWorldPos.z = 0;
        }

        void MoveHead() {
            Vector3 vectorToTarget = targetWorldPos - transform.position;
            float distanceToTarget = vectorToTarget.magnitude;

            if (distanceToTarget > deadzoneRadius) {
                isMoving = true;

                // Calculate the rotation needed to face the mouse
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                // Smoothly rotate toward that target
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

                // Always move forward in the direction the head is currently facing
                transform.position += transform.up * moveSpeed * Time.deltaTime;
            } else {
                isMoving = false;
            }
        }

        void UpdateSplinePath() {
            if (!isMoving) { return; }

            float3 currentPos = path.transform.InverseTransformPoint(transform.position);
            float3 lastKnotPos = path.Spline[path.Spline.Count - 1].Position;

            // Only add a knot if we've moved far enough to maintain resolution
            if (math.distance(currentPos, lastKnotPos) > knotResolution) {
                path.Spline.Add(new BezierKnot(currentPos));

                // Set the previous knot to AutoSmooth to keep the curve flowing
                int lastIndex = path.Spline.Count - 1;
                path.Spline.SetTangentMode(lastIndex, TangentMode.AutoSmooth);
            }
        }

        private void LateUpdate() {
            PositionBodySegments();
            TrimSpline();
        }

        void PositionBodySegments() {
            float totalSplineLength = path.Spline.GetLength();

            for (int i = 0; i < activeSegments.Count; i++) {
                // Calculate distance from the head (which is at the end of the spline)
                float distanceOffset = (i + 1) * segmentSpacing;
                float targetDistance = totalSplineLength - distanceOffset;

                // Only position the segment if the spline is long enough to reach it
                if (targetDistance >= 0) {
                    // Get position and forward direction (tangent)
                    float3 pos, tangent, up;
                    path.Evaluate(targetDistance / totalSplineLength, out pos, out tangent, out up);

                    activeSegments[i].transform.position = path.transform.TransformPoint((Vector3)pos);

                    // Ensure the segment faces the direction of the path
                    if (!tangent.Equals(float3.zero)) {
                        activeSegments[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, tangent);
                    }
                }
            }
        }

        void TrimSpline() {
            // Keep the spline just long enough to cover all segments plus a small buffer
            float totalNeededLength = (activeSegments.Count + 1) * segmentSpacing;

            // We use a while loop to prune old knots from the start (index 0)
            // but we keep at least 2 knots so the spline remains a valid path.
            while (path.Spline.Count > 2 && path.Spline.GetLength() > totalNeededLength + 2.0f) {
                path.Spline.RemoveAt(0);
            }
        }
    }
}
