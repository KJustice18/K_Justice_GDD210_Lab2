using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private float MouseSens = 10f;
    public Transform camTrans;
    public CharacterController cc;
    public GameObject Door1;
    public GameObject Door2;

    private float camRotation = 0f;

    public float MoveSpeed;
    public float Grav = -9.8f;
    public float JumpSpeed;

    public float vertSpeed;

    public TMP_Text WinTxt;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        WinTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit))
            {
                Debug.DrawLine(camTrans.position + new Vector3(0f, -1f, 0f), hit.point, Color.green, 5f);

                if (hit.collider.gameObject.tag == "Bounce") 
                {
                    RaycastHit hit2;
                    if (Physics.Raycast(hit.point, hit.normal, out hit2)) 
                    {
                        Debug.DrawLine(hit.point, hit2.point, Color.green, 5f);

                        if (hit2.collider.gameObject.tag == "Bounce")
                        {
                            RaycastHit hit3;
                            if (Physics.Raycast(hit2.point, hit2.normal, out hit3))
                            {
                                Debug.DrawLine(hit2.point, hit3.point, Color.green, 5f);

                                if (hit3.collider.gameObject.tag == "Button1")
                                {
                                    Destroy(hit3.collider.gameObject);
                                    Door1.SetActive(false);
                                }
                                else if (hit3.collider.gameObject.tag == "Button2")
                                {
                                    Destroy(hit3.collider.gameObject);
                                    Door2.SetActive(false);
                                }
                                else if (hit3.collider.gameObject.tag == "WinButton")
                                {
                                    WinTxt.enabled = true;
                                }
                            }

                        }
                        else if (hit2.collider.gameObject.tag == "Button1")
                        {
                            Destroy(hit2.collider.gameObject);
                            Door1.SetActive(false);
                        }
                        else if (hit2.collider.gameObject.tag == "Button2")
                        {
                            Destroy(hit2.collider.gameObject);
                            Door2.SetActive(false);
                        }
                        else if (hit2.collider.gameObject.tag == "WinButton")
                        {
                            WinTxt.enabled = true;
                        }
                    }

                }

                else if (hit.collider.gameObject.tag == "Button1")
                {
                    Destroy(hit.collider.gameObject);
                    Door1.SetActive(false);
                }
                else if (hit.collider.gameObject.tag == "Button2")
                {
                    Destroy(hit.collider.gameObject);
                    Door2.SetActive(false);
                }
                else if (hit.collider.gameObject.tag == "WinButton")
                {
                    WinTxt.enabled = true;
                }
            }
        }

        float mouseInputY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
        camRotation -= mouseInputY;
        camRotation = Mathf.Clamp(camRotation, -90f, 90f);
        camTrans.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

        float mouseInputX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));

        Vector3 movement = Vector3.zero;

        // X/Z movement
        float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

        if (cc.isGrounded)
        {
            vertSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vertSpeed = JumpSpeed;
            }
        }

        vertSpeed += (Grav * Time.deltaTime);
        movement += (transform.up * vertSpeed * Time.deltaTime);

        cc.Move(movement);

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(0);
        }
    }
}
