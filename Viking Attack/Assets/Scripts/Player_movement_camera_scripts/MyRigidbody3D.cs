using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidbody3D : MonoBehaviour
{
    [SerializeField] private float gravity = 3f;
    [SerializeField] private float staticFrictionCoefficient = 0.3f;
    [SerializeField] private float kineticFrictionCoefficient = 0.2f;
    [SerializeField] private float airResistance = 0.0001f;
    private float colliderMargin = 0.01f;
    private float groundCheckDistance = 0.01f;
    private CapsuleCollider capsuleCollider;
    private LayerMask collisionMask;
    public Vector3 velocity;
    private Vector3 point1;
    private Vector3 point2;
    private bool onGround;
    void Awake()
    {
        collisionMask = ~(1 << gameObject.layer);
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {        
        velocity +=  Vector3.down * gravity;

        velocity *= Mathf.Pow(airResistance, Time.deltaTime);
        
        point1 = gameObject.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        point2 = gameObject.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);

        PhysicsObjectFrictionFunction();
        UpdateVelocity();

        transform.position += velocity * Time.deltaTime;
    }
    public bool GroundedBool()
    {
        RaycastHit hit = new RaycastHit();
        return Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out hit ,(groundCheckDistance + colliderMargin),collisionMask);
    }
    public RaycastHit Grounded()
    {
        RaycastHit hit = new RaycastHit();
        Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out hit ,(groundCheckDistance + colliderMargin),collisionMask);
        return hit;
    }
    private void UpdateVelocity()
    {
        if(velocity.magnitude  < 0.0001f)
        {
            velocity = Vector3.zero;
            return;
        }
        RaycastHit hit1;
        Vector3 normalForce = Vector3.zero;
        if(Physics.CapsuleCast(point1, point2, capsuleCollider.radius, velocity.normalized, out hit1 ,Mathf.Infinity,collisionMask))
        {
            float distanceToColliderNeg = colliderMargin / Vector3.Dot(velocity.normalized, hit1.normal);
            float allowedMovementDistance = hit1.distance + distanceToColliderNeg;
            if (allowedMovementDistance > velocity.magnitude * Time.deltaTime) 
            {
                return;
            }
            if (allowedMovementDistance > 0.0f) 
            {
                velocity += velocity.normalized * allowedMovementDistance;
            }
            normalForce += GetComponent<GeneralHelpFunctions3D>().CalculateNormalForce(velocity,hit1.normal);
            velocity += normalForce;
            UpdateVelocity();
        }
        Collider[] hitList = Physics.OverlapCapsule(point1, point2, capsuleCollider.radius, collisionMask);
        if(hitList.Length > 0)
        {
            Vector3 direction;
            float distance = Mathf.Infinity;
            Collider colliderToStartWith = null;
            foreach(Collider hit2 in hitList)
            {
                Vector3 tempDirection;
                float tempDistance;
                Physics.ComputePenetration(capsuleCollider,capsuleCollider.transform.position,capsuleCollider.transform.rotation,hit2,hit2.transform.position,hit2.transform.rotation,out tempDirection,out tempDistance);
                if(tempDistance < distance)
                {
                    distance = tempDistance;
                    colliderToStartWith = hit2;
                }
            }
            Physics.ComputePenetration(capsuleCollider,capsuleCollider.transform.position,capsuleCollider.transform.rotation,colliderToStartWith,colliderToStartWith.transform.position,colliderToStartWith.transform.rotation,out direction,out distance);

            Vector3 separationVector = direction * distance;
            transform.position += separationVector + direction.normalized * colliderMargin * Time.deltaTime;

            normalForce += GetComponent<GeneralHelpFunctions3D>().CalculateNormalForce(velocity, direction.normalized);
            velocity += normalForce;
            UpdateVelocity();
        }
        FrictionFunction(normalForce);

    }
    private void FrictionFunction(Vector3 normalForce)
    {
        if (velocity.magnitude < normalForce.magnitude * staticFrictionCoefficient)
            velocity = Vector3.zero;
        else
            velocity -= velocity.normalized * normalForce.magnitude * kineticFrictionCoefficient;
    }
    private void PhysicsObjectFrictionFunction()
    {
        if(GroundedBool() && Grounded().transform.gameObject.GetComponent<MyRigidbody3D>() != null)
        {
            Vector3 normalForce = GetComponent<GeneralHelpFunctions3D>().CalculateNormalForce(velocity,Grounded().normal);
            if (velocity.magnitude - Grounded().transform.gameObject.GetComponent<MyRigidbody3D>().velocity.magnitude < normalForce.magnitude * staticFrictionCoefficient)
                velocity = Grounded().transform.gameObject.GetComponent<MyRigidbody3D>().velocity;
            else
                velocity -= velocity.normalized * normalForce.magnitude * kineticFrictionCoefficient;
        }
    }
}