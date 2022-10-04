using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    float weight = .7f;

    Animator anim;
    Camera mainCam;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void OnAnimatorIK(int LayerIndex)
    {
        anim.SetLookAtWeight(weight, .5f, 1.2f, .5f, 5f);
        // v�cut par�alar�n�n d�nme oran�.

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
        // Ray(ba�lang�� pozisyonu, y�nelimi)

        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
    public void IKLookArt()
    {
        weight = Mathf.Lerp(weight, 1f, Time.fixedDeltaTime);
        // Lerp(var olan de�er, gidilecek de�er)
    }
    public void IKLookAzal()
    {
        weight = Mathf.Lerp(weight, 0, Time.fixedDeltaTime);
        // Lerp(var olan de�er, gidilecek de�er)
    }
}



// Not: Animator'deki Layer ayarlar� k�sm�nda 'IK Pass' ayar� a��lmal�.
