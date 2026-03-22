using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using System;

namespace DragonWorm {
    [RequireComponent(typeof(Rigidbody2D))]
    public class SnakeManager : MonoBehaviour {
        private SplineContainer path;
        [SerializeField] private BodySegment bodySegmentPrefab;
        private List<BodySegment> activeSegments = new List<BodySegment>();
        private Rigidbody2D rb;

        [Header("Movement Settings")]
        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField, Tooltip("Degrees per second")] private float turnSpeed = 450f;
        [Space]
        [SerializeField, Header("Start braking")] private float slowdownRadius = 3f;
        [SerializeField, Header("Stop")] private float deadzoneRadius = 0.25f;

        [Header("Snake Settings")]
        [SerializeField] private float segmentSpacing = 1.0f;
        [SerializeField] private float knotResolution = 0.2f;
        [SerializeField] private int initialSize = 5;

        private Vector3 targetWorldPos;
        private float currentSpeed;
        private float speedVelocity;
        private bool isMoving = false;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

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
                BodySegment segment = Instantiate(bodySegmentPrefab, transform.position, Quaternion.identity);
                activeSegments.Add(segment);
            }
        }

        private void Update() {
            UpdateTargetPosition();
        }

        private void UpdateTargetPosition() {
            if (Mouse.current == null) { return; }

            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            targetWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
            targetWorldPos.z = 0;
        }

        private void FixedUpdate() {
            MoveHead();
            UpdateSplinePath();
            PositionBodySegments();
            TrimSpline();
        }

        void MoveHead() {
            Vector3 vectorToTarget = targetWorldPos - (Vector3)rb.position;
            float distance = vectorToTarget.magnitude;

            if (distance > deadzoneRadius) {
                // Calculate the rotation needed to face the mouse
                float targetAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

                float rotStep = turnSpeed * Time.fixedDeltaTime;
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotStep));
            }

            float targetSpeed = 0f;

            if (distance > slowdownRadius) {
                targetSpeed = maxSpeed;
            } else if (distance > deadzoneRadius) {
                float t = (distance - deadzoneRadius) / (slowdownRadius - deadzoneRadius);
                targetSpeed = maxSpeed * t;
            }

            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothTime);

            if (currentSpeed > 0.01f) {
                isMoving = true;
                Vector2 moveStep = (Vector2)transform.right * currentSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + moveStep);
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

                    Vector3 worldPos = path.transform.TransformPoint((Vector3)pos);

                    float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
                    Quaternion worldRot = Quaternion.Euler(0, 0, angle);

                    activeSegments[i].MoveTo(worldPos, worldRot);
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

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, deadzoneRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, slowdownRadius);
        }
    }
}
