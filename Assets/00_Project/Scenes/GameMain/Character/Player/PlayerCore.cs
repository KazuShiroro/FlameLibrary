using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Character.Player
{
    public class PlayerCore : CharacterCore
    {
        private void Start()
        {
            Initialize(new Vector3(0f, 0f, 0f));
        }
    }
}
