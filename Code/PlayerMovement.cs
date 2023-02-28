using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float   walking_velocity        = 0;
    private Vector3 walking_velocity_vector = Vector3.zero;
    private Vector3 last_walk_forward       = Vector3.zero;
    private Vector3 last_walk_right         = Vector3.zero;

    private CharacterController character_controller;

    const float MOVE_SPEED =  1.0f;
    const float DECEL_RATE =  1.0f;
    const float MAX_WSPEED =  1.0f;
    const float SPEED_MULT = 10.0f;

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

        //Move the player by the acceleration
        character_controller.Move(walk_amount * walking_velocity * Time.deltaTime * SPEED_MULT);

    }

}
