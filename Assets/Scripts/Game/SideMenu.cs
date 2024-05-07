using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuButton : MonoBehaviour
{
   public GameObject SideMenu;

   

   public void ShowHideMenu()
   {
      if (SideMenu != null)
      {
         Animator animator = SideMenu.GetComponent<Animator>();
         if (animator != null)
         {
            bool isShown = animator.GetBool("Show");
            animator.SetBool("Show", !isShown);
         }
      }
   }
}
