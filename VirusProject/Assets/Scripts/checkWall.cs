using UnityEngine;
using System.Collections;

public class checkWall : MonoBehaviour
{
    public bool againstWallVertUp= false;
    public bool againstWallVertDown= false;
    public bool againstWallHorRight= false;
    public bool againstWallHorLeft = false;
    void checkRay()
    {
        int layerMask = 1 << 9;


        Transform myTransform = transform;


        Debug.DrawRay(myTransform.position, Vector2.up, Color.red); //debug ray to see the ray
        Debug.DrawRay(myTransform.position, -Vector2.up, Color.blue); //debug ray to see the ray
        Debug.DrawRay(myTransform.position, Vector2.right, Color.green); //debug ray to see the ray
        Debug.DrawRay(myTransform.position, -Vector2.right, Color.cyan); //debug ray to see the ray

        RaycastHit2D hitUp = Physics2D.Raycast(myTransform.position, Vector2.up, 0.2f, layerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(myTransform.position, -Vector2.up, 0.2f, layerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(myTransform.position, Vector2.right, 0.2f, layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(myTransform.position, -Vector2.right, 0.2f, layerMask);

        //Up
        if ((hitUp != false && hitUp.collider.tag == "wall") || (hitUp != false && hitUp.collider.tag == "door"))
        {

            againstWallVertUp = true;
        }
        else
        {
            againstWallVertUp = false;
        }

        //Down
        if ((hitDown != false && hitDown.collider.tag == "wall") || (hitDown != false && hitDown.collider.tag == "door"))
        {

            againstWallVertDown= true;
        }
        else
        {
            againstWallVertDown = false;
        }

        //Right
        if ((hitRight != false && hitRight.collider.tag == "wall") || (hitRight != false && hitRight.collider.tag == "door"))
        {

            againstWallHorRight = true;
        }
        else
        {
            againstWallHorRight = false;
        }

        //Left
        if ((hitLeft != false && hitLeft.collider.tag == "wall") || (hitLeft != false && hitLeft.collider.tag == "door"))
        {

            againstWallHorLeft = true;
        }
        else
        {
            againstWallHorLeft = false;
        }
    }

    void Update()
    {
        checkRay();
    }
}
