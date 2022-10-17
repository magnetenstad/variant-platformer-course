using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    public GameObject bullet;
    public GameObject muzzle;
    public GameObject gunBase;

    //TODO: Find a suitable bullet speed
    int bulletSpeed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // TODO: Instantiate a bullet prefab at the muzzle position. Find the direction to shoot. Set the bullet's velocity.
    void Shoot() {
        GameObject newBullet =
            Instantiate(bullet, muzzle.transform.position, Quaternion.identity);
        Vector3 dir =
            (muzzle.transform.position - gunBase.transform.position).normalized;
        newBullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        // if (newBullet.GetComponent<Rigidbody2D>().transform.localScale.x < 0) {
        //     b.GetComponent<SpriteRenderer>().flipX = true;
        // }
    }


    // TODO: Shoot() when this input is called.
    public void OnShootButtonInput(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Shoot!");
            Shoot();
        }
    }

    
}
