using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSwitch : MonoBehaviour
{
    private CoffeeShopManager cManager;
    private bool interactable = false;
    private bool switched = false;
    public GameObject mugMan;
    public GameObject mugGirl;
    public GameObject mugMe;
    public GameObject mugDate;
    private Vector3 mugManTransform;
    private Vector3 mugGirlTransform;
    private Vector3 mugMeTransform;
    private Vector3 mugDateTransform;
    public GameObject prompt;


    // Start is called before the first frame update
    void Start()
    {
        cManager = FindObjectOfType<CoffeeShopManager>();
        mugManTransform = mugMan.transform.position;
        mugGirlTransform = mugGirl.transform.position;
        mugMeTransform = mugMe.transform.position;
        mugDateTransform = mugDate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable && Input.GetKeyDown(KeyCode.E))
        {
            cManager.mugSwitched = true;
            mugMan.transform.position = mugMeTransform;
            mugGirl.transform.position = mugDateTransform;
            mugMe.transform.position = mugManTransform;
            mugDate.transform.position = mugGirlTransform;
        }

        if(switched && !cManager.mugSwitched)
        {
            switched = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && cManager.path2)
        {
            interactable = true;
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            interactable = false;
            prompt.SetActive(false);
        }
    }
}
