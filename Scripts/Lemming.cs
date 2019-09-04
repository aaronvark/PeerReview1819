using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemming : MonoBehaviour
{
    public enum LemmingTypes { Basic, Stopper, Dimensional, Parachuter, Climber }
    [SerializeField] private LemmingTypes lemmingType = LemmingTypes.Basic;
    [SerializeField] private LayerMask terrainHitMask;

    [SerializeField] private float groundDetectionHeight = 0.1f;
    [SerializeField] private bool isGrounded = false;

    [SerializeField] private Material stopperMat;
    [SerializeField] private Material dimensionalMat;


    [SerializeField] private bool dirRight;
    [SerializeField] private bool onXAxis = true;
    [SerializeField] private bool changedDimension;

    //private Rigidbody rb;
    private Vector3 velocity;

    private void Awake() {
        //rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        CalculateBehaviour();
    }

    private void FixedUpdate() {
        transform.position += velocity * Time.deltaTime;
        //rb.velocity = velocity;
    }

    private void CalculateBehaviour() {

        //if (!isStopper) {
        //    rb.velocity = new Vector3(rb.velocity.y != 0 ? 0 : (dirRight ? 3 : -3), rb.velocity.y, 0);
        //    CheckForCollision();
        //}
        //else
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (Grounded()) {
            switch (lemmingType) {
                default:
                case LemmingTypes.Basic:
                    CheckForCollision();
                    velocity = new Vector3(onXAxis ? (dirRight ? 3 : -3) : 0, 0, onXAxis ? 0 : (dirRight ? 3 : -3));
                    break;
                case LemmingTypes.Stopper:
                    velocity = Vector3.zero;
                    break;
                case LemmingTypes.Dimensional:
                    velocity = Vector3.zero;
                    break;
                case LemmingTypes.Climber:
                    break;
            }

        }
        else if (lemmingType == LemmingTypes.Parachuter) {
            velocity = new Vector3(0, -1, 0);
        }
        else {
            velocity = new Vector3(0, -5, 0);
        }
    }

    private void CheckForCollision() {

        RaycastHit _hit;

        if (Physics.Raycast(transform.position, CurrentDirection(), out _hit, 0.5f, terrainHitMask)) {

            Lemming _lemming = _hit.transform.GetComponent<Lemming>();

            if (_lemming != null && _lemming.IsDimensionalLemming()) {

                if (!changedDimension) {
                    transform.position = _lemming.transform.position;
                    onXAxis = onXAxis ? false : true;
                    changedDimension = true;
                }
            }

            else {
                dirRight = dirRight ? false : true;
                changedDimension = false;
            }
        }

        else {
            changedDimension = false;
        }
    }

    private bool Grounded() {

        bool _returnValue = false;

        if (Physics.Raycast(transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0), Vector3.down, groundDetectionHeight, terrainHitMask)) _returnValue = true;
        if (Physics.Raycast(transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0), Vector3.down, groundDetectionHeight, terrainHitMask)) _returnValue = true;

        if (Physics.Raycast(transform.position + new Vector3(0, -transform.localScale.y / 2, GetComponent<MeshRenderer>().bounds.size.z * transform.localScale.z), Vector3.down, groundDetectionHeight, terrainHitMask)) _returnValue = true;
        if (Physics.Raycast(transform.position + new Vector3(0, -transform.localScale.y / 2, -GetComponent<MeshRenderer>().bounds.size.z * transform.localScale.z), Vector3.down, groundDetectionHeight, terrainHitMask)) _returnValue = true;

        if (Physics.Raycast(transform.position + new Vector3(0, -transform.localScale.y / 2, 0), Vector3.down, groundDetectionHeight, terrainHitMask)) _returnValue = true;

        //if (Physics.Linecast(transform.position + new Vector3(0, -transform.localScale.y / 2, 0), transform.position + new Vector3(0, -transform.localScale.y / 2 + velocity.y * 0.1f, 0))) _returnValue = true;

        //if (Physics.Linecast(transform.position + new Vector3(0, -transform.localScale.y / 2, GetComponent<MeshRenderer>().bounds.size.z * transform.localScale.z),
        //    transform.position + new Vector3(0, -transform.localScale.y / 2 + velocity.y * groundDetectionHeight,
        //    GetComponent<MeshRenderer>().bounds.size.z * transform.localScale.z))) _returnValue = true;

        //if (Physics.Linecast(transform.position + new Vector3(0, -transform.localScale.y / 2, -GetComponent<MeshRenderer>().bounds.size.z * transform.localScale.z),
        //    transform.position + new Vector3(0, -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 
        //    -GetComponent<MeshRenderer>().bounds.size.z * transform.localScale.z))) _returnValue = true;

        //if (Physics.Linecast(transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0),
        //    transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x,
        //    -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 0))) _returnValue = true;

        //if (Physics.Linecast(transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0),
        //    transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x,
        //    -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 0))) _returnValue = true;

        return isGrounded = _returnValue;
    }

    /// <summary>
    /// Return the current direction that the lemming should be facing.
    /// </summary>
    /// <returns></returns>
    private Vector3 CurrentDirection() {
        return onXAxis ? (dirRight ? Vector3.right : Vector3.left) : (dirRight ? Vector3.forward : Vector3.back);
    }

    private void OnMouseDown() {

        switch (CameraBehaviour.Instance.CurrentType) {

            default:
            case 1:
                if (Grounded()) {
                    lemmingType = LemmingTypes.Stopper;
                    gameObject.layer = 9; //Make lemming part of the terrain
                    GetComponent<Renderer>().material = stopperMat;
                }
                break;
            case 2:
                if (Grounded()) {
                    lemmingType = LemmingTypes.Dimensional;
                    gameObject.layer = 9; //Make lemming part of the terrain
                    GetComponent<Renderer>().material = dimensionalMat;
                    GetComponent<Collider>().isTrigger = true;
                }
                break;
        }

        //if(Grounded()) {
        //    lemmingType = LemmingTypes.Dimensional;
        //    gameObject.layer = 9; //Make lemming part of the terrain
        //    GetComponent<Renderer>().material = dimensionalMat;
        //    GetComponent<Collider>().isTrigger = true;
        //}
    }

    private void OnDrawGizmos() {

        Gizmos.DrawLine(transform.position, transform.position + CurrentDirection());

        Gizmos.DrawLine(transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0), transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0), transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, -transform.localScale.y / 2, 0), transform.position + new Vector3(0, -transform.localScale.y / 2 + velocity.y * 0.1f, 0));
    }

    //private void OnTriggerExit(Collider other) {
    //    Lemming _lemming = other.transform.GetComponent<Lemming>();
    //    Debug.Log(_lemming);
    //    if (_lemming != null && _lemming.IsDimensionalLemming() && changedDimension)
    //        changedDimension = false;
    //}

    public bool IsDimensionalLemming() {
        return lemmingType == LemmingTypes.Dimensional ? true : false;
    }

}
