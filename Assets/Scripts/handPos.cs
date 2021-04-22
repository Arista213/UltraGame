using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handPos : MonoBehaviour
{
    [SerializeField] private readonly Vector3 _verticalHandPos = new Vector3 (0f, 0.8f, 0f);
    [SerializeField] private readonly Vector3 _horizontalHandPos = new Vector3 (0.8f, 0.1f, 0f);
    [SerializeField] private float _valueOfChangeHand = 0.2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") >= _valueOfChangeHand)
            transform.position = GameObject.FindWithTag("Player").transform.position + _verticalHandPos;
        else if (Input.GetAxis("Vertical") <= -_valueOfChangeHand)
            transform.position = GameObject.FindWithTag("Player").transform.position - _verticalHandPos;
        else if (Input.GetAxis("Horizontal") <= -_valueOfChangeHand)
            transform.position = GameObject.FindWithTag("Player").transform.position - _horizontalHandPos;
        else if (Input.GetAxis("Horizontal") >= _valueOfChangeHand)
            transform.position = GameObject.FindWithTag("Player").transform.position + _horizontalHandPos;
    }
}