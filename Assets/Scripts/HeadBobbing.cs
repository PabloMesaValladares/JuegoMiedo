using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class HeadBobbing : MonoBehaviour
{
    [Range(0.00f, 1f)]
    public float amount = 0.002f;
    [Range(0.1f, 100f)]
    public float frequency = 10.0f;
    [Range(0.1f, 100f)]
    public float smooth = 10.0f;
    [Range(0.1f, 100f)]
    public float multiplierPos = 1.5f;
    [Range(0.1f, 200f)]
    public float multiplierRot = 25.0f;
    [Range(0.1f, 100f)]
    public float multiplierFre = 4.0f;

    Vector3 startPos;
    Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBobbingTrigger();
        StopBobbing();
    }

    private void CheckBobbingTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if(inputMagnitude > 0)
        {
            StartBobbing();
        }
    }

    private Vector3 StartBobbing()
    {
        Vector3 pos = Vector3.zero;
        Vector3 rot = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * multiplierPos, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Sin(Time.time * frequency / 2f) * amount * multiplierPos, smooth * Time.deltaTime);
        rot.z += Mathf.Lerp(rot.z, Mathf.Sin(Time.time * frequency / multiplierFre) * amount * multiplierRot, smooth * Time.deltaTime);
        transform.localPosition += pos;
        transform.Rotate(rot);
        //transform.localRotation.z += rot;


        return pos;
    }

    private void StopBobbing()
    {
        if(transform.localPosition == startPos)
        {
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);// Z vuelve a 0
            return;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 5f * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, startRot, 5f * Time.deltaTime);

        /*
        //transform.localRotation = Vector3.Lerp(0,0, 10 * Time.deltaTime);
        if (transform.localRotation.z > 0)
        {
           transform.localRotation = Quaternion.Slerp(transform.rotation, startRot, 10 * Time.deltaTime);
        }
        else if(transform.localRotation.z < 0) 
        {
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }
        */
        //if(transform.localRotation == startRot) return;

        //transform.Rotate.z = new Vector3(0,0,0);
        //transform.rotation = Quaternion.Euler(new Vector3(0,0,0));

    }
}
