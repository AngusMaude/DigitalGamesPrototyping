// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Ship"",
            ""id"": ""7080a3f6-9966-4518-afe5-2b2f604a5f54"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""bfe8ceb8-3019-49c1-b372-b7dae4917d26"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a0efd211-d7ec-4260-b5b0-ec54782df636"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0fe3aac0-062c-47b0-a9c8-fb8f01385a1c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e6e67258-2fbb-46c6-a9a0-f8e98821f6ce"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""269aa020-e6a1-4659-a875-be52f8150e25"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eda8b66f-b96a-4b38-a378-f80e9b4c26ff"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""0d85dd5a-c465-4368-a3c4-623bb7eb4c0a"",
            ""actions"": [
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""c6cf0731-157e-4b36-9e35-f436ce054f1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""c656db2b-3306-48b8-a3ea-fae196360dcd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""93703c0b-367d-4bc4-bbc8-9f0805a6d5eb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72528418-2906-441c-85cb-240c08b75eb9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Ship
        m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
        m_Ship_Fire = m_Ship.FindAction("Fire", throwIfNotFound: true);
        m_Ship_Move = m_Ship.FindAction("Move", throwIfNotFound: true);
        m_Ship_Aim = m_Ship.FindAction("Aim", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Ship
    private readonly InputActionMap m_Ship;
    private IShipActions m_ShipActionsCallbackInterface;
    private readonly InputAction m_Ship_Fire;
    private readonly InputAction m_Ship_Move;
    private readonly InputAction m_Ship_Aim;
    public struct ShipActions
    {
        private @PlayerControls m_Wrapper;
        public ShipActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Ship_Fire;
        public InputAction @Move => m_Wrapper.m_Ship_Move;
        public InputAction @Aim => m_Wrapper.m_Ship_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void SetCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnFire;
                @Move.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnMove;
                @Aim.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_ShipActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public ShipActions @Ship => new ShipActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Cancel;
    private readonly InputAction m_UI_Submit;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IShipActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnCancel(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
