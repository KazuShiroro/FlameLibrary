using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Character.Player
{
    public class CusroController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
