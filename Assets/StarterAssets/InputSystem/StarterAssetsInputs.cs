using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Fatigue Customization")]
        public ControlsFatigueInfluence fatigueControls;
        private float influence;
        private bool isMoving;
        private Vector2 lastInput;
        public bool moveAllowed = true;

        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private void Update()
        {
            influence = fatigueControls.GetFatigueNoiseInfluence();
            if (isMoving)
            {
                Vector2 influenceV = lastInput + new Vector2(influence, influence);
                MoveInput(influenceV);
            }
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            if (!moveAllowed)
            {
                isMoving = false;
                move = Vector2.zero;
                return;
            }


            //Debug.Log($"Noise Influence: {influence}");
            //float slowness = fatigueControls.GetFatigueValue();
            //Debug.Log($"Slowness Influence: {slowness}");
            Vector2 movement = value.Get<Vector2>();
            lastInput = movement;
            if (movement.x != 0f || movement.y != 0f)
            {
                movement.x += influence;
                movement.y += influence;
                isMoving = true;
            }
            if (movement.x == 0f && movement.y == 0f) isMoving = false;
            //movement.x *= slowness;
            //movement.y *= slowness;

            MoveInput(movement);
        }
        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        // public void OnJump(InputValue value)
        // {
        // 	if(!moveAllowed)
        // 		return;

        // 	JumpInput(value.isPressed);
        // }

        // public void OnSprint(InputValue value)
        // {
        // 	if(!moveAllowed)
        // 		return;

        // 	SprintInput(value.isPressed);
        // }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        //private void OnApplicationFocus(bool hasFocus)
        //{
        //    SetCursorState(cursorLocked);
        //}

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}