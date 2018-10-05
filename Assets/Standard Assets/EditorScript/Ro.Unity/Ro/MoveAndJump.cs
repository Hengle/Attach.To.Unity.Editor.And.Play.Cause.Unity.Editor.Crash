#if UNITY_EDITOR
using UnityEngine;

namespace Script.Ro
{
    public abstract class MoveAndJump : MonoBehaviour
    {
        // Use this for initialization
        private CharacterController cc;

        void Start()
        {
            cc = GetComponent<CharacterController>();
        }

        float speed = 6f;
        float jumpSpeed = 8.0f;
        float gravity = 20.0f;
        private Vector3 moveDire = Vector3.zero;

        void Update()
        {
            CharacterController controller = GetComponent<CharacterController>();
            
            if (controller.isGrounded)
            {
                var h = Input.GetAxis("Horizontal");
                var v = Input.GetAxis("Vertical");
                moveDire = new Vector3(h, 0, v);
//            moveDirection = transform.TransformDirection(moveDirection);
                moveDire *= speed;
                if (Input.GetButton("Jump"))
                {
                    moveDire.y = jumpSpeed;
                }

                if (h != 0 || v != 0)
                {
                    transform.forward = new Vector3(moveDire.x, 0, moveDire.z);
                }
            }

            moveDire.y -= gravity * Time.deltaTime;

            if (moveDire.x == 0 && moveDire.z == 0)
            {
                controller.Move(new Vector3(0, moveDire.y * Time.deltaTime, 0));
            }
            else
            {
                controller.Move(moveDire * Time.deltaTime);
            }
        }
    }
}
#endif