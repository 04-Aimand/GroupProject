using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxScript : MonoBehaviour
{
    public float speed;
    private Skybox skybox;
    // Start is called before the first frame update
    void Start()
    {
        skybox = GetComponent<Skybox>();
    }

    // Update is called once per frame
    void Update()
    {
        skybox.material.SetFloat("_Rotation", Time.time * speed);
    }
}
