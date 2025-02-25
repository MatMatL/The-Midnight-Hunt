using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float MoveSpeed;
    public float RotationSpeed;
    public float JumpHeight;

    private int ObjectUnderPlayer = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Input.mousePositionDelta.x*RotationSpeed ,0));

        Vector3 CurrentSpeed = transform.forward * Input.GetAxis("Vertical") * MoveSpeed + transform.right * Input.GetAxis("Horizontal") * MoveSpeed;
        CurrentSpeed.y = GetComponent<Rigidbody>().linearVelocity.y;

        if (Input.GetKeyDown(KeyCode.Space) && ObjectUnderPlayer > 0 )
        {
            CurrentSpeed.y += JumpHeight;
        }

        GetComponent<Rigidbody>().linearVelocity = CurrentSpeed;

    }

    private void OnCollisionEnter(Collision collision)
    {
       //if (collision.GetComponent<tag>() == "Wolf" )
        //life bar
        //CurrentHealth--;
        //LifeBar.transform.localScale = new Vector3((float)CurrentHealth/(float)StartHealth, 1 , 1);
        ObjectUnderPlayer++;
    }

    private void OnCollisionExit(Collision collision)
    {
        ObjectUnderPlayer--;
    }

    //button function
    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0,1000,0));
    }
}
