using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemming : MonoBehaviour
{
    //TODO verander parachuter naar een boolean, aangezien een lemming zowel een type moet kunnen zijn als een parachute moet kunnen hebben.
    public enum LemmingTypes { Basic, Stopper, Dimensional, Parachuter, Climber } 
    [SerializeField] LemmingTypes lemmingType = LemmingTypes.Basic;
    [SerializeField] LayerMask terrainHitMask;
    [SerializeField] float movementSpeed;

    [SerializeField] float groundDetectionHeight = 0.1f;
    [SerializeField] bool isGrounded = false;

    [SerializeField] Material stopperMat;
    [SerializeField] Material dimensionalMat;

    [SerializeField] bool dirRight;
    [SerializeField] bool onXAxis = true;
    [SerializeField] bool changedDimension;

    private Vector3 velocity;
    private MeshRenderer meshRend;

    private void Awake()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        CalculateBehaviour();
        CheckForInteractables();
    }

    private void FixedUpdate()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void CalculateBehaviour()
    {
        if (Grounded())
        {
            switch (lemmingType)
            {
                default:
                case LemmingTypes.Basic:
                    CheckForCollision();
                    velocity = DefaultVelocity();
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

        else if (lemmingType == LemmingTypes.Parachuter) velocity = new Vector3(0, -1, 0);

        else velocity = new Vector3(0, -5, 0);
       
    }

    private void CheckForCollision()
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, CurrentDirection(), out _hit, 0.5f, terrainHitMask))
        {
            Lemming _lemming = _hit.transform.GetComponent<Lemming>();

            if (_lemming != null && _lemming.IsDimensionalLemming())
            {
                if (!changedDimension)
                {
                    transform.position = _lemming.transform.position;
                    onXAxis = onXAxis ? false : true;
                    changedDimension = true;
                }
            }

            else
            {
                dirRight = dirRight ? false : true;
                changedDimension = false;
            }
        }

        else changedDimension = false;
    }

    private void CheckForInteractables()
    {
        Collider[] _cols = Physics.OverlapBox(transform.position, transform.localScale/2);

        foreach (var _col in _cols)
        {
            IInteractable _interactable = _col.transform.GetComponent<IInteractable>();
            if(_interactable != null) _interactable.Interact(gameObject);      
        }
        
    }

    private bool Grounded()
    {
        bool _returnValue = false;

        Vector3 _size = meshRend.bounds.size;
        Vector3 _scale = transform.localScale;

        float _yOffset = -transform.localScale.y / 2;

        if (RaycastDown(new Vector3(0, _yOffset, 0))) _returnValue = true; //Raycast from center

        if (RaycastDown(new Vector3(_size.x * _scale.x, _yOffset, 0))) _returnValue = true; //Raycast from right edge
        if (RaycastDown(new Vector3(-_size.x * _scale.x, -transform.localScale.y / 2, 0))) _returnValue = true; //Raycast from left edge

        if (RaycastDown(new Vector3(0, _yOffset, _size.z * _scale.z))) _returnValue = true; //Raycast from front edge
        if (RaycastDown(new Vector3(0, _yOffset, -_size.z * _scale.z))) _returnValue = true; //Raycast from back edge

        return isGrounded = _returnValue;
    }

    /// <summary>
    /// Fires a ray downwards to check for ground.
    /// </summary>
    /// <param name="_originOffset"></param>
    /// <returns></returns>
    private bool RaycastDown(Vector3 _originOffset)
    {
        return Physics.Raycast(transform.position + _originOffset, Vector3.down, groundDetectionHeight, terrainHitMask);
    }

    /// <summary>
    /// Returns the current direction that the lemming should be facing.
    /// </summary>
    /// <returns></returns>
    private Vector3 CurrentDirection()
    {
        return onXAxis ? (dirRight ? Vector3.right : Vector3.left) : (dirRight ? Vector3.forward : Vector3.back);
    }

    /// <summary>
    /// Returns the velocity that the lemming should have when in a default state.
    /// </summary>
    /// <returns></returns>
    private Vector3 DefaultVelocity()
    {
        float _newMoveSpeed = dirRight ? movementSpeed : -movementSpeed; 
        return new Vector3(onXAxis ? _newMoveSpeed : 0, 0, onXAxis ? 0 : _newMoveSpeed);

        //DISCUSS Ik heb zoals je zei de nested ternary operator in deze functie gezet en heb hem zo aangepast dat hij niet meer nested is. Ik ben het er volkomen mee eens dat het erg onleesbaar wordt
        //zodra je ze gaat nesten, wel vind ik dat de manier waarop ik ze hierboven gebruik erg efficiënt is en zeer leesbaar blijft. De regels hieronder zouden mijn alternatief zijn geweest om het 
        //zonder ternary operator te maken, maar zelf vind ik dit geen hele efficiënte code. Had ik dit anders kunnen doen, of is er een andere reden om geen ternary operator te gebruiken?

        //float _newMoveSpeed = movementSpeed;
        //if (dirRight ?) _newMoveSpeed = -_newMoveSpeed;

        //Vector3 _newVelocity = Vector3.zero;
        //if (onXAxis) _newVelocity.x = _newMoveSpeed;
        //else _newVelocity.z = _newMoveSpeed;

        //return _newVelocity;
    }

    private void OnMouseDown()
    {
        if (Grounded() && lemmingType == LemmingTypes.Basic) 
        {
            switch (CameraBehaviour.Instance.CurrentType)
            {
                default:
                case 1:
                    lemmingType = LemmingTypes.Stopper;
                    gameObject.layer = 9; //Make lemming part of the terrain
                    GetComponent<Renderer>().material = stopperMat;
                    break;
                case 2:
                    lemmingType = LemmingTypes.Dimensional;
                    gameObject.layer = 9; //Make lemming part of the terrain
                    GetComponent<Renderer>().material = dimensionalMat;
                    GetComponent<Collider>().isTrigger = true;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //De code hier is nog erg slordig, maar aangezien dit puur een tijdelijke debug is vind ik het zonde van mijn tijd om het op te schonen.
        Gizmos.DrawLine(transform.position, transform.position + CurrentDirection());

        Gizmos.DrawLine(transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0), transform.position + new Vector3(GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2, 0), transform.position + new Vector3(-GetComponent<MeshRenderer>().bounds.size.x * transform.localScale.x, -transform.localScale.y / 2 + velocity.y * groundDetectionHeight, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, -transform.localScale.y / 2, 0), transform.position + new Vector3(0, -transform.localScale.y / 2 + velocity.y * 0.1f, 0));

        //Gizmos.DrawCube(transform.position, transform.localScale);
    }

    public bool IsDimensionalLemming()
    {
        return lemmingType == LemmingTypes.Dimensional ? true : false;
    }
}
