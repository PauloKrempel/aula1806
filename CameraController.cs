using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector2 rotacaoMouse;
    public int sensibilidade;
    void Start()
    {
        
    }
    void Update()
    {
        Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime, rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotacaoMouse.x, transform.eulerAngles.z);
        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);
        
        cameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);
    }
}
