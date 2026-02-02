using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventoryRework : MonoBehaviour
{
    private PlayerControls InventoryController;
    [Header("Slot textures")]
    [SerializeField] GameObject IContainer1;
    [SerializeField] GameObject IContainer2;
    [SerializeField] GameObject IContainer3;
    [SerializeField] GameObject HudSlot1;
    [SerializeField] GameObject HudSlot2;
    [SerializeField] GameObject HudSlot3;
    [Header("Textures")]
    [SerializeField] Texture SelectedSlot;
    [SerializeField] Texture NormalSlot;
    [Header("Inventory Objects")]
    [SerializeField] Texture Sword;
    [SerializeField] Texture Key;
    [SerializeField] GameObject SwordObject;
    [SerializeField] GameObject KeyObject;
    [SerializeField] AudioClip SwordSound;
    [SerializeField] AudioClip KeySound;
    RawImage Image1;
    RawImage Image2;
    [Header("PickUp Logic")]
    [SerializeField] GameObject SwordItem; // Sword
    [SerializeField] GameObject KeyItem; // Key
    [SerializeField] LayerMask ItemLayer;
    [Header("Script Controls")]
    [SerializeField] float PickUpDistance = 30f;
    [SerializeField] AudioSource Speaker;
    public bool hasKey = false;
    public static int selectedSlot = 1;
    public static int CurrentInventory1 = 0;
    public static int CurrentInventory2 = 0;
    public static int CurrentInventory3 = 0;
    void Start()
    {
        Inventory1(1);
        InventoryController = new PlayerControls();
        InventoryController.Enable();
    }

    void Update()
    {
        InventoryController.GamePlay.InventorySelect.performed += ctx => ToggleSlot(ctx);
        InventoryController.GamePlay.Use.performed += ctx => Pickup();
        if (selectedSlot == 1)
        {
            if(CurrentInventory1 == 0)
                {
                SwordObject.GetComponent<ParentConstraint>().weight = 0;
                KeyObject.GetComponent<ParentConstraint>().weight = 0;
            }
            if (CurrentInventory1 == 1)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 1;
                KeyObject.GetComponent<ParentConstraint>().weight = 0;
            }
            if (CurrentInventory1 == 2)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 0;
                KeyObject.GetComponent<ParentConstraint>().weight = 1;
                hasKey = true;
            }
        }
        if (selectedSlot == 2)
        {
            if (CurrentInventory2 == 0)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 0;
                KeyObject.GetComponent<ParentConstraint>().weight = 0;
            }
            if (CurrentInventory2 == 1)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 1;
                KeyObject.GetComponent<ParentConstraint>().weight = 0;
            }
            if (CurrentInventory2 == 2)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 0;
                KeyObject.GetComponent<ParentConstraint>().weight = 1;
                hasKey = true;
            }
        }
        if (selectedSlot == 3)
        {
            if (CurrentInventory3 == 0)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 0;
                KeyObject.GetComponent<ParentConstraint>().weight = 0;
            }
            if (CurrentInventory3 == 1)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 1;
                KeyObject.GetComponent<ParentConstraint>().weight = 0;
            }
            if (CurrentInventory3 == 2)
            {
                SwordObject.GetComponent<ParentConstraint>().weight = 0;
                KeyObject.GetComponent<ParentConstraint>().weight = 1;
                hasKey = true;
            }
        }

    }
    public void Inventory1(int Slot1)
    {
        CurrentInventory1 = Slot1;
        switch (Slot1)
        {
            case 0:
                IContainer1.SetActive(false);
                break;
            case 1:
                IContainer1.GetComponent<RawImage>().texture = Sword;
                IContainer1.SetActive(true);
                break;
            case 2:
                IContainer1.GetComponent<RawImage>().texture = Key;
                IContainer1.SetActive(true);
                break;
        }
    }
    public void Inventory2(int Slot2)
    {
        CurrentInventory2 = Slot2;
        switch (Slot2)
        {
            case 0:
                IContainer2.SetActive(false);
                break;
            case 1:
                IContainer2.GetComponent<RawImage>().texture = Sword;
                IContainer2.SetActive(true);
                break;
            case 2:
                IContainer2.GetComponent<RawImage>().texture = Key;
                IContainer2.SetActive(true);
                break;
        }
    }
    public void Inventory3(int Slot3)
    {
        CurrentInventory3 = Slot3;
        switch (Slot3)
        {
            case 0:
                IContainer3.SetActive(false);
                break;
            case 1:
                IContainer3.GetComponent<RawImage>().texture = Sword;
                IContainer3.SetActive(true);
                break;
            case 2:
                IContainer3.GetComponent<RawImage>().texture = Key;
                IContainer3.SetActive(true);
                break;
        }
    }
    private void SelectSlot(int Slot)
    {
        switch (Slot)
        {
            case 1:
                HudSlot1.GetComponent<RawImage>().texture = SelectedSlot;
                HudSlot2.GetComponent<RawImage>().texture = NormalSlot;
                HudSlot3.GetComponent<RawImage>().texture = NormalSlot;
                if (CurrentInventory1 == 1)
                {

                }
                break;
            case 2:
                HudSlot2.GetComponent<RawImage>().texture = SelectedSlot;
                HudSlot1.GetComponent<RawImage>().texture = NormalSlot;
                HudSlot3.GetComponent<RawImage>().texture = NormalSlot;
                break;
            case 3:
                HudSlot3.GetComponent<RawImage>().texture = SelectedSlot;
                HudSlot1.GetComponent<RawImage>().texture = NormalSlot;
                HudSlot2.GetComponent<RawImage>().texture = NormalSlot;
                break;
        }
    }
    private void ToggleSlot(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        if (value > 0)
        {
            selectedSlot++;
            if (selectedSlot > 3)
            {
                selectedSlot = 1;
            }
        } else if (value < 0)
        {
            selectedSlot--;
            if (selectedSlot < 1)
            {
                selectedSlot = 3;
            }
        }
        SelectSlot(selectedSlot);
        Debug.Log(selectedSlot);
        switch (selectedSlot)
        {
            case 1:
                if (CurrentInventory1 == 1) //Sword
                {
                    Speaker.PlayOneShot(SwordSound);
                    Debug.Log("<color=yellow> Sword Sound");
                    SwordObject.GetComponent<ParentConstraint>().weight = 1;
                    KeyObject.GetComponent<ParentConstraint>().weight = 0;
                }
                else if (CurrentInventory1 == 2) //Key
                {
                    Speaker.PlayOneShot(KeySound);
                    hasKey = true;
                    Debug.Log("<color=yellow> Key Sound");
                    SwordObject.GetComponent<ParentConstraint>().weight = 0;
                    KeyObject.GetComponent<ParentConstraint>().weight = 1;
                }
                else
                {
                    SwordObject.GetComponent<ParentConstraint>().weight = 0;
                    KeyObject.GetComponent<ParentConstraint>().weight = 0;

                }
                break;
            case 2:
                if (CurrentInventory2 == 1) //Sword
                {
                    Speaker.PlayOneShot(SwordSound);
                    Debug.Log("<color=yellow> Sword Sound");
                    SwordObject.GetComponent<ParentConstraint>().weight = 1;
                    KeyObject.GetComponent<ParentConstraint>().weight = 0;
                }
                else if (CurrentInventory2 == 2) //Key
                {
                    Speaker.PlayOneShot(KeySound);
                    hasKey = true;
                    Debug.Log("<color=yellow> Key Sound");
                    SwordObject.GetComponent<ParentConstraint>().weight = 0;
                    KeyObject.GetComponent<ParentConstraint>().weight = 1;
                }

                else
                {
                    SwordObject.GetComponent<ParentConstraint>().weight = 0;
                    KeyObject.GetComponent<ParentConstraint>().weight = 0;

                }
                break;
            case 3:
                if (CurrentInventory3 == 1) //Sword
                {
                    Speaker.PlayOneShot(SwordSound);
                    Debug.Log("<color=yellow> Sword Sound");
                    SwordObject.GetComponent<ParentConstraint>().weight = 1;
                    KeyObject.GetComponent<ParentConstraint>().weight = 0;
                }
                else if (CurrentInventory3 == 2) //Key
                {
                    Speaker.PlayOneShot(KeySound);
                    hasKey = true;
                    Debug.Log("<color=yellow> Key Sound");
                    SwordObject.GetComponent<ParentConstraint>().weight = 0;
                    KeyObject.GetComponent<ParentConstraint>().weight = 1;
                }
                
                else
                {
                    SwordObject.GetComponent<ParentConstraint>().weight = 0;
                    KeyObject.GetComponent<ParentConstraint>().weight = 0;

                }
                break;
        }


        }

    
    private void Pickup()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit Hit, PickUpDistance, ItemLayer))
        {
            if (Hit.collider.gameObject == SwordItem)
            {
                Destroy(Hit.collider.transform.parent.gameObject);
                Debug.Log("<color=yellow> Sword Sound");
                Speaker.PlayOneShot(SwordSound);
                if (selectedSlot == 1)
                {
                    Inventory1(1);

                }
                else if (selectedSlot == 2)
                {
                    Inventory2(1);

                }
                else if (selectedSlot == 3)
                {
                    Inventory3(1);

                }

            }
            else if (Hit.collider.gameObject == KeyItem)
            {
                Destroy(Hit.collider.transform.parent.gameObject);

                Debug.Log("<color=yellow> Key Sound");
                Speaker.PlayOneShot(KeySound);
                if (selectedSlot == 1)
                {
                    Inventory1(2);

                }
                else if (selectedSlot == 2)
                {
                    Inventory2(2);

                }
                else if (selectedSlot == 3)
                {
                    Inventory3(2);

                }
            }
        }
    }
    public void ConsumeKey()
    {
        hasKey = false;
        if (CurrentInventory1 == 2)
        {
            Inventory1(0);
        }
        else if (CurrentInventory2 == 2)
        {
            Inventory2(0);
        }
        else if (CurrentInventory3 == 2)
        {
            Inventory3(0);
        }
    }
    public int CheckHand()
    {
        if (SwordObject.GetComponent<ParentConstraint>().weight == 1)
        {
            return 1; //devuelve Espada
        }
        else if (KeyObject.GetComponent<ParentConstraint>().weight == 1)
        {
            return 2; // Devuelve Llave
        }
        else
        {
            return 0; //Devuelve que no tiene nada
        }
        
        
    }
}
