using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float constantSpeed =10, leftSpeed, rightSpeed;
    public Vector3 constantDirection;
    public Rigidbody player;
    public bool grounded, roofed, deltaGrav, invertedGrav, restoreGrav, loseGame;
    public float leftSpeedIN, rightSpeedIN, elevation, maxElevation, switchFloor, switchRoof;
    public float velocityZ, velocityY, velocityX, gravity, gravityMag, height, rotSpeed;
    public Transform roofLocation, normalLerp, invLerp;
    public LayerMask floorLayer, roofLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<Rigidbody>();
        gravity = 0;
        maxElevation = roofLocation.position.y;
        switchFloor = maxElevation / 3;
        switchRoof = maxElevation * 2 / 3;
        deltaGrav = true;
        loseGame = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftSpeedIN = leftSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftSpeedIN = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightSpeedIN = rightSpeed;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightSpeedIN = 0;
        }
        
    }
    public bool GroundCheck()
    {
        Debug.DrawRay(transform.position, Vector3.up, Color.red, 5f);
        return Physics.Raycast(transform.position, Vector3.down, height, floorLayer);
        
    }
    public bool CeilingCheck()
    {
        
        return Physics.Raycast(transform.position, Vector3.up, height, roofLayer);
    }
    public void RestoreGravity()
    {
        restoreGrav = true;
        invertedGrav=false;
    }
    public void InvertGravity()
    {
        restoreGrav = false;
        invertedGrav = true;
    }
    public void SetGravity(bool restGrav, bool invGrav, bool grounded, bool roofed)
    {
        if(invGrav && restGrav)
        {
            Debug.Log("Both on");
        }
        else if(restGrav && !invGrav)
        {
            if(grounded)
            {
                gravity = 0;
            }
            else if(!grounded)
            {
                gravity = -1 * gravityMag;
            }
        }
        else if(!restGrav && invGrav)
        {
            if(roofed || player.position.y >= 70)
            {
                gravity = 0;
                
            }
            else if(!roofed)
            {
                gravity = gravityMag;
            }
        }
        else 
        {
            if(grounded)
            {
                gravity = 0;
            }
            else if(roofed)
            {

            }
            else
            {

            }
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if( col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            loseGame = true;

            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(deltaGrav)
        {
            RestoreGravity();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            RestoreGravity();
            deltaGrav = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            InvertGravity();
            deltaGrav = true;
        }
        
        GroundCheck();
        CeilingCheck();
        roofed = CeilingCheck();
        grounded = GroundCheck();
        elevation = player.position.y;
        SetGravity(restoreGrav, invertedGrav, grounded, roofed);
        
        velocityX = player.velocity.x;
        velocityY = player.velocity.y;
        velocityZ = player.velocity.z;

        constantDirection = new Vector3(constantSpeed * -100 * Time.fixedDeltaTime, gravity * 100 * Time.fixedDeltaTime, ((leftSpeedIN*-1) + rightSpeedIN) * 100 * Time.fixedDeltaTime);
        player.velocity = constantDirection;

        transform.rotation = Quaternion.Lerp(normalLerp.rotation, invLerp.rotation, elevation/maxElevation);
        



    }
}
