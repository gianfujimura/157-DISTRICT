using District.Character.Player;
using UnityEngine;
using UnityEngine.Scripting;

public class TurnTowardController : MonoBehaviour
{
    [SerializeField, RequiredMember] PlayerController m_controller;
    public float turnSpeed = 50f;

    Transform tr;
    float currentYRotation;
    const float fallOffAngle = 90f;

    void Start()
    {
        tr = transform;
        currentYRotation = tr.eulerAngles.y;
    }

    private void LateUpdate()
    {
        //Vector3 velocity = Vector3.ProjectOnPlane(m_controller.GetMovementVelocity(), tr.parent.up);
        Vector3 velocity = Vector3.ProjectOnPlane(m_controller.Systems.Movement.GetMovementVelocity(), tr.parent.up);
        if (velocity.magnitude < 0.001f) return;

        //float angleDifference = VectorMath.GetAngle(tr.forward, velocity.normalized, tr.parent.up);

        float angleDifference = Vector3.SignedAngle(
                                    tr.forward,
                                    velocity.normalized,
                                    tr.parent.up);

        float step = Mathf.Sign(angleDifference) *
                     Mathf.InverseLerp(0f, fallOffAngle, Mathf.Abs(angleDifference)) *
                     Time.deltaTime * turnSpeed;

        currentYRotation += Mathf.Abs(step) > Mathf.Abs(angleDifference) ? angleDifference : step;

        tr.localRotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
}
