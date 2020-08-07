using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcess;
    private ChromaticAberration abberation;
    private bool rewind = false;
    [SerializeField] private float abberationIntensity = .5f;
    [SerializeField] private float abberationSpeed = .1f;
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float turnSpeed = 0.1f;
    public CharacterController controller;
    private float turnSmoothVelocity;
    private Animator anim;
    public Transform chairInRange;
    public bool canSit = false;
    public bool sitting = false;
    public bool canMove = true;
    private Transform preSit;
    

    // Start is called before the first frame update
    void Start()
    {
        postProcess.profile.TryGetSettings(out abberation);
        anim = GetComponentInChildren<Animator>();
        Debug.Log(abberation != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
        }

        Interactions();
        Rewind();
    }

    private void Interactions()
    {
        if(!sitting && canSit && chairInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(chairInRange.GetComponentInParent<Chair>().transform.rotation.y);
            preSit = transform;
            transform.position = chairInRange.position;
            transform.eulerAngles = new Vector3(0, chairInRange.GetComponentInParent<Chair>().yRotation, 0);
            Debug.Log(transform.localEulerAngles.y);
            canMove = false;
            anim.SetBool("Sit", true);
            sitting = true;
            
        }
        else if (sitting && Input.GetKeyDown(KeyCode.E))
        {
            this.transform.position = preSit.position;
            transform.rotation = preSit.rotation;
            preSit = null;
            canMove = true;
            anim.SetBool("Sit", false);
            sitting = false;
        }
    }

    private void LateUpdate()
    {
        if(transform.position.y != 1)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1)
        {
            anim.SetBool("Walking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * movementSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private void Rewind()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var reversable = FindObjectsOfType<TimelineController>();
            foreach (var character in reversable)
            {
                rewind = true;
                character.StartRewind();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            var reversable = FindObjectsOfType<TimelineController>();
            foreach (var character in reversable)
            {
                rewind = false;
                character.StopRewind();
            }
        }

        if (rewind && abberation.intensity.value < abberationIntensity)
        {
            abberation.intensity.value += Time.deltaTime * abberationSpeed;
        }
        else if (!rewind && abberation.intensity.value > 0)
        {
            abberation.intensity.value -= Time.deltaTime * abberationSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chair")
        {
            canSit = true;
            chairInRange =  other.GetComponent<Chair>().sitPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Chair")
        {
            canSit = false;
        }
    }
}
