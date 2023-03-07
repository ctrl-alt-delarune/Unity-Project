using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float   walking_velocity        = 0.0f;
    private Vector3 walking_velocity_vector = Vector3.zero;
    private Vector3 last_walk_forward       = Vector3.zero;
    private Vector3 last_walk_right         = Vector3.zero;
    public  float   vertical_velocity       = 0.0f;
    private bool    jump_command            = false;

    private CharacterController character_controller;

    const float MOVE_SPEED =  1.0f;
    const float DECEL_RATE =  1.0f;
    const float MAX_WSPEED =  1.0f;
    const float SPEED_MULT = 10.0f;

    const float GVEL =   5.0f;
    const float JVEL =   2.0f;
    const float VVEL =  10.0f;
    const float TVEL = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        character_controller = GetComponent<CharacterController>();

        last_walk_forward = transform.forward;
        last_walk_right   = transform.right  ;

    }
  
    // Update is called once per frame
    void Update()
    {

        walking_velocity_vector.x = Input.GetAxis("XMovement");
        walking_velocity_vector.z = Input.GetAxis("YMovement");

        if (walking_velocity_vector.x != 0 || walking_velocity_vector.z != 0)
        {

            walking_velocity += Time.deltaTime;

            last_walk_forward = transform.forward;
            last_walk_right   = transform.right  ;

        }

        //Apply deceleration
        walking_velocity -= walking_velocity * 0.3f * Time.deltaTime;

        //Cap the player's move velocity
        if (walking_velocity >= MAX_WSPEED)
            walking_velocity  = MAX_WSPEED;

        Vector3 walk_amount = new Vector3(0, 0, 0);
        walk_amount += walking_velocity_vector.x * last_walk_right  ;
        walk_amount += walking_velocity_vector.z * last_walk_forward;

        //Jump logic
        if (Input.GetKeyDown(KeyCode.Space))
            jump_command = true;
        if (Input.GetKeyUp(KeyCode.Space))
            jump_command = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.down, 0.5f, 0b001);

        if (colliders.Length != 0)
        {
            
            if (jump_command && vertical_velocity <= 0)
                vertical_velocity = JVEL;

        }
        vertical_velocity -= Time.deltaTime * GVEL;

        //Terminal velocity cap
        if (vertical_velocity < TVEL)
            vertical_velocity = TVEL;

        //Move the player by the acceleration
        CollisionFlags cf = character_controller.Move(
                 (walk_amount * walking_velocity * SPEED_MULT
                + vertical_velocity * VVEL * Vector3.up)
            * Time.deltaTime);

        if ((cf & CollisionFlags.Below) != 0)
            vertical_velocity = 0;

    }

}
