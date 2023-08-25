using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
   [SerializeField] private Camera _camera;

    private void Awake()
    {
        this.transform.localScale = new Vector3 ((float)(0.2963978*_camera.aspect/1.778571), this.transform.localScale.y, this.transform.localScale.z);
    }

    private void Update()
    {
        Debug.Log(_camera.aspect); //1.778571
    }
}
