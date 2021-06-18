using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioGame.Pads
{
    public class GamePadsController : Singleton<GamePadsController>
    {
        public bool isOnMobile;

        bool canMoveLeft;
        bool canMoveRight;

        public bool CanMoveLeft { get => canMoveLeft; set => canMoveLeft = value; }
        public bool CanMoveRight { get => canMoveRight; set => canMoveRight = value; }

        private void Update()
        {
            if (!isOnMobile)
                PCHandle();
        }

        void PCHandle()
        {
            canMoveLeft = Input.GetAxis("Horizontal") < 0;
            canMoveRight = Input.GetAxis("Horizontal") > 0;

        }
    }
}
  


