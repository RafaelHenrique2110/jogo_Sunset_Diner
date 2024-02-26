using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class CameraController2 : MonoBehaviour
{
    public static CameraController2 instance;
    public bool isometric = true;



    public Transform followTransform;
    public Transform cameraTransform;



    public float movementSpeed;
    public float movementTime;



    public float normalSpeed;
    public float fastSpeed;



    public float rotationAmount;
    public Vector3 zoomAmount;



    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;



    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;



    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public LayerMask rayMask;

    public float zoomDir;

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    bool canDrag = true;
    bool canRotate = true;

    float dragTime=0;

    Vector3 savedRotation = new Vector3();
    public GameObject lastClickedObject;

    void Start()
    {
        instance = this;



        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }



    void Update()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
            HandleMovementInput();
            HandleMouseInput();
            
        }
        else
        {
            HandleMovementInput();
            HandleMouseInput();
            
        }
    }



    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            //newZoom += Input.mouseScrollDelta.y * zoomAmount * 2;
            //Debug.Log(Input.mouseScrollDelta.y);
            zoomDir = Mathf.Clamp(Input.mouseScrollDelta.y,-1,1) * 5;
        }


        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 8 && PauseManager.instance.gameState == PauseManager.GameStates.EditMode) // If collided with Object Layer
                {
                    //followTransform = hit.transform;
                   

                    if (lastClickedObject != null)
                        ///lastClickedObject.GetComponentInChildren(typeof(Canvas), true).gameObject.SetActive(false);

                    lastClickedObject = hit.transform.gameObject;

                    //hit.transform.GetComponentInChildren(typeof(Canvas), true).gameObject.SetActive(true);

                    canDrag = false;
                }
                else if (hit.transform.gameObject.layer != 5)
                {
                    if (lastClickedObject != null)
                       /// lastClickedObject.GetComponentInChildren(typeof(Canvas), true).gameObject.SetActive(false);

                    followTransform = null;
                    canDrag = true;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;



            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);



                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }


            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 8 && PauseManager.instance.gameState == PauseManager.GameStates.EditMode) // If collided with Object Layer
                {
                    dragTime += Time.deltaTime;

                }

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.transform.gameObject.layer == 8 && PauseManager.instance.gameState == PauseManager.GameStates.EditMode) // If collided with Object Layer
                {
                    if (dragTime > 0.3f) // reposicionar o objeto
                    {
                        dragTime = 0;
                        if (lastClickedObject != null)
                            lastClickedObject.GetComponentInChildren(typeof(Canvas), true).gameObject.SetActive(false);

                        //hit.transform.DOShakePosition(1f, new Vector3(0.1f, 0, 0), 20, 90, false, false);
                        //hit.transform.DOShakeRotation(1f,new Vector3(10, 0,0.2f),12,0,false);
                        if (SistemaFinanceiro.instance.travarCompra == false)
                        {
                            hit.transform.GetComponent<obj>().seguirMouse = true;
                        }
                       

                    }


                }

            }


        }



        //if (isometric == true)// rotação com o mouse estavão aqui dentro
        //{ }



        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;
            newRotation *= Quaternion.Euler((Vector3.up * (-difference.x / 5f)));
        }

    }



    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if (isometric)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += (transform.forward * movementSpeed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += (transform.forward * -movementSpeed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += (transform.right * movementSpeed);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += (transform.right * -movementSpeed);
            }
        }

        



        //if (isometric == true) // Q e E estavam aqui dentro
        //{ }



        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
            zoomDir = 1;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
            zoomDir = -1;
        }
        else if (Input.mouseScrollDelta.y == 0)
        {
            zoomDir = 0;
        }



        if (canDrag)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(newPosition.x,minX,maxX),newPosition.y,Mathf.Clamp(newPosition.z,minZ,maxZ)), Time.deltaTime * movementTime);
        }
        if (canRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        }
        //cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.forward, Time.deltaTime * movementTime);



        RaycastHit hit;
        RaycastHit hit2;
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, rayMask))
        {
            //Debug.Log(Vector3.Distance(cameraTransform.position, hit.point));
            if (Vector3.Distance(cameraTransform.position, hit.point) <= 10 && zoomDir > 0)
            {
                 zoomDir = 0;
            }

            if (Vector3.Distance(cameraTransform.position, hit.point) >= 20 && zoomDir < 0)
            {
                zoomDir = 0;
            }

            //cameraTransform.position += cameraTransform.forward * Time.deltaTime * movementTime * zoomDir;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.position + (cameraTransform.forward * zoomDir), Time.deltaTime * movementTime);
        }
        if (Physics.Raycast(cameraTransform.position + cameraTransform.forward*3, cameraTransform.forward * -1, out hit2, 20f, rayMask))
        {
            cameraTransform.position -= cameraTransform.forward * 3;
        }

    }




    public void TopDown()
    {
        MudaCorCorGrid(true);
        canRotate = false;
        savedRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.DORotate(new Vector3(45, transform.rotation.y, transform.rotation.z), 1f);
        StartCoroutine(StopDrag());
    }



    public void Isometric()
    {
        //MudaCorCorGrid(false);
        Debug.Log(savedRotation);

        if (lastClickedObject != null)
            lastClickedObject.GetComponentInChildren(typeof(Canvas), true).gameObject.SetActive(false);

        //transform.DORotate(savedRotation, 1f);

        StartCoroutine(WaitForTween());
        StartCoroutine(StopDrag());
    }
    IEnumerator WaitForTween()
    {
        yield return new WaitForSeconds(1f);
        canRotate = true;
    }
    IEnumerator StopDrag()
    {
        canDrag = false;
        yield return new WaitForSeconds(1f);
        canDrag = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * 20);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * -20);
    }
    public void MudaCorCorGrid(bool ativar)
    {
        for (int i = 0; i < listas.instance.gridOBJ.Count; i++)
        {
            if(listas.instance.gridOBJ[i] != null)
            listas.instance.gridOBJ[i].SetActive(ativar);
        }
    }
}