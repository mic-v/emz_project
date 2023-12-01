using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stamina MyStam;
    private PlayerInput MyInput;
    public GameObject Head;
    public GameObject StaminaBar;
    public GameObject HealthBar;
    private bool CanMove = true;
    // Start is called before the first frame update
    void Start()
    {
        MyInput = new PlayerInput(gameObject, Head);
        MyStam = new Stamina(100, 12.5f, 40.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        MyInput.Update(MyStam, CanMove);
        Vector3 StaminaBarSize = StaminaBar.transform.localScale;
        StaminaBar.transform.localScale = new Vector3(MyStam.GetStamina() / 100, StaminaBarSize.y, StaminaBarSize.z);
    }

    public void SetCanMove(bool temp)
    {
        CanMove = temp;
    }
}
