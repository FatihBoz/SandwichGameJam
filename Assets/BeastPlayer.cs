//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.0
//     from Assets/BeastPlayer.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @BeastPlayer: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @BeastPlayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BeastPlayer"",
    ""maps"": [
        {
            ""name"": ""Beast"",
            ""id"": ""1ac67da2-e079-486c-8c75-92449efeff9d"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""8eedede8-c9e7-4937-bdc3-a9ce74334ce9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Value"",
                    ""id"": ""2f4db863-453a-414c-b09a-588e991c1561"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Value"",
                    ""id"": ""abcb837e-9428-40aa-824f-1d5787ca01cf"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""74cb9f9a-2cda-4907-973c-e63785e8ef2d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7dad7edd-adf0-489d-9335-e3dd15309415"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ef7f14f4-5881-4078-94a5-0e54c7ece76b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2907525f-9151-4fcb-9a15-5c243131543d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9ef2614c-351f-40c5-8452-8b59a2b3a837"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f632d9dc-4752-4970-9f31-20352b1b7444"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6798e652-967b-488f-b8cb-1b278d71a4d0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c324d26b-efd0-4a19-bff9-c642aedefe85"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a1b07e7d-357d-437b-9aa4-2a42ff8d13bc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Beast
        m_Beast = asset.FindActionMap("Beast", throwIfNotFound: true);
        m_Beast_Movement = m_Beast.FindAction("Movement", throwIfNotFound: true);
        m_Beast_Attack = m_Beast.FindAction("Attack", throwIfNotFound: true);
        m_Beast_Dodge = m_Beast.FindAction("Dodge", throwIfNotFound: true);
        m_Beast_Jump = m_Beast.FindAction("Jump", throwIfNotFound: true);
    }

    ~@BeastPlayer()
    {
        UnityEngine.Debug.Assert(!m_Beast.enabled, "This will cause a leak and performance issues, BeastPlayer.Beast.Disable() has not been called.");
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Beast
    private readonly InputActionMap m_Beast;
    private List<IBeastActions> m_BeastActionsCallbackInterfaces = new List<IBeastActions>();
    private readonly InputAction m_Beast_Movement;
    private readonly InputAction m_Beast_Attack;
    private readonly InputAction m_Beast_Dodge;
    private readonly InputAction m_Beast_Jump;
    public struct BeastActions
    {
        private @BeastPlayer m_Wrapper;
        public BeastActions(@BeastPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Beast_Movement;
        public InputAction @Attack => m_Wrapper.m_Beast_Attack;
        public InputAction @Dodge => m_Wrapper.m_Beast_Dodge;
        public InputAction @Jump => m_Wrapper.m_Beast_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Beast; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BeastActions set) { return set.Get(); }
        public void AddCallbacks(IBeastActions instance)
        {
            if (instance == null || m_Wrapper.m_BeastActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BeastActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Dodge.started += instance.OnDodge;
            @Dodge.performed += instance.OnDodge;
            @Dodge.canceled += instance.OnDodge;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IBeastActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Dodge.started -= instance.OnDodge;
            @Dodge.performed -= instance.OnDodge;
            @Dodge.canceled -= instance.OnDodge;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IBeastActions instance)
        {
            if (m_Wrapper.m_BeastActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBeastActions instance)
        {
            foreach (var item in m_Wrapper.m_BeastActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BeastActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BeastActions @Beast => new BeastActions(this);
    public interface IBeastActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
