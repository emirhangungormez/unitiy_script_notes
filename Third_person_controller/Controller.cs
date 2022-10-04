using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;
    float normalFov;
    public float sprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera mainCamera;

    // Yava� ve H�zl� ko�ma tu� atamas�
    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.LeftAlt;

    void Start()
    {
        Anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        normalFov = mainCamera.fieldOfView;
    }                                

    void LateUpdate()
    {
        InputMove();
        InputRotation();
        Movement();
    }

    void Movement()
    {
        StickDirection = new Vector3(inputX, 0, inputY);

        /*
             O   O   O          
                                (Diker) Y Ekseni
             O   M   O          (Yatay) X Ekseni
                                Tu�lara bas�ld���nda karakter "M"den "O"lara y�nelecek.
             O   O   O
        */

        if (Input.GetKey(SprintButton)) // Ko�uyor ise:
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, sprintFov, Time.deltaTime * 2);
            // Lerp(var olan de�er, gidilecek de�er)
            // Kameran�n a��s� de�i�tirilir

            maxSpeed = 2;
            inputX = 2 * Input.GetAxis("Horizontal"); // Yatay
            inputY = 2 * Input.GetAxis("Vertical"); // Dikey
        }
        else if (Input.GetKey(WalkButton)) // Daha yava� y�r�yor ise:
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
            // Lerp(var olan de�er, gidilecek de�er)
            // Kameran�n a��s� de�i�tirilir

            maxSpeed = 0.3f;
            inputX = Input.GetAxis("Horizontal"); // Yatay
            inputY = Input.GetAxis("Vertical"); // Dikey
        }
        else // Y�r�yor ise:
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
            // Lerp(var olan de�er, gidilecek de�er)
            // Kameran�n a��s� de�i�tirilir

            maxSpeed = 1;
            inputX = Input.GetAxis("Horizontal"); // Yatay
            inputY = Input.GetAxis("Vertical"); // Dikey
        }
    }

    void InputMove() // Hareket etme
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
        // Hareketin 0 ile 1 de�eri aras�nda gidip gelmesi
        // damp = soft bir ge�i� sa�lar.
    }

    void InputRotation() // Kamera-Hareket uyumu
    {
        Vector3 rotOfset = mainCamera.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);
        // Slerp =  dairsel d�n�� i�in

        // Kameran�n hareket etmesi sonucunda hareket vekt�r�leri belirlenir

        /*
        
        �lk bak��:

               (V)
            O   O   O          
                               V = Hareket y�n�
            O   M   O          K = KAmera
                               
            O   O   O
               (K)

        �kinci bak��:


            O   O   O          
                               V = Hareket y�n�
        (V) O   M   O (K)      K = KAmera
                               
            O   O   O
      */
    }
}
