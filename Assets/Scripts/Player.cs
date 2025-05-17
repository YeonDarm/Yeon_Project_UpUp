using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; //캐릭터매니저에 Player 정보를 넣는다.
        //외부에서 Player를 통해 접근 할 수 있게끔.
        controller = GetComponent<PlayerController>();
    }
}
