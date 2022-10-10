using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerShoot : MonoBehaviour
{
    public Gun gun;
    //public Transform gunPivot; //총배치 기준점
    //public Transform lefthand;  // 총왼쪽손잡이 왼손위치 지점
    //public Transform righthand; // 총오른쪽 손잡이 오른손 위치 지점
    private PlayerInput playerInput; // 플레이어입력
    private Animator playerAnim; // 애니메이터 컴포넌트



    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>(); 
    }
    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1")/*playerInput.fire*/)
        {
            gun.Fire();
        }
        else if(CrossPlatformInputManager.GetButtonDown("Reload"))
        {
            if (gun.Reload())
            {
                //playerAnim.SetTrigger("Reload");
            }
        }
        else if(CrossPlatformInputManager.GetButtonDown("GRB1"))
        {
            Debug.Log("Check");
            gun.Granade();
        }
        

        UpdateUI();
    }

    

    void UpdateUI()
    {
        if(gun != null && UIManager.instance != null)
        {
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }
}
