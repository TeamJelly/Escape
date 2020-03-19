using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{

    public void StartTest()
    {
        Inventory.instance.GetItem("고랑이");
        Inventory.instance.GetItem("손전등");
        Inventory.instance.GetItem("건전지");
        Inventory.instance.GetItem("트럼프");
        Inventory.instance.GetItem("효자손");
        Inventory.instance.GetItem("주민등록증");
        Inventory.instance.GetItem("부러진 열쇠1");
        Inventory.instance.GetItem("부러진 열쇠2");
        Inventory.instance.GetItem("니퍼");
        Inventory.instance.GetItem("테이프");
        Inventory.instance.GetItem("복제된 열쇠");
        Inventory.instance.GetItem("밀가루");
        Inventory.instance.GetItem("지문1");
        Inventory.instance.GetItem("지문2");
    }
   
}
