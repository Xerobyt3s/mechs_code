using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipment : MonoBehaviour
{
    
    [SerializeField] GameObject sword;
    [SerializeField] GameObject star;

    [SerializeField] int selectedweapon = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) {
            if (selectedweapon >= 1){
                selectedweapon = 0;
            } else {
                selectedweapon++;
            }
            
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) {
            if (selectedweapon <= 0){
                selectedweapon = 1;
            } else {
                selectedweapon--;
            }
            
        }

        if (selectedweapon == 0f){
            sword.gameObject.SetActive(true);
            star.gameObject.SetActive(false);
        } else if (selectedweapon == 1f){
            star.gameObject.SetActive(true);
            sword.gameObject.SetActive(false);
        }

        
    }
}
