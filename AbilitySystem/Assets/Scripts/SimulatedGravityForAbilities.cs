using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedGravityForAbilities : MonoBehaviour
{
    bool isInAir = true;
    bool isSetInGround = false;


    void Update()
    {
        if (isInAir)
        {
            transform.position -= Physics2D.gravity.y * Time.deltaTime * Vector3.up;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isInAir = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!isSetInGround)
        {
            transform.position += Physics2D.gravity.y * Time.deltaTime * Vector3.up;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isSetInGround = true;
        transform.position -= 0.005f * Vector3.up;
    }
}
