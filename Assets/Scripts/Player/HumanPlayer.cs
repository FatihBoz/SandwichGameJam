//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.0
//     from Assets/Scripts/Player/HumanPlayer.inputactions
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

public partial class @HumanPlayer: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @HumanPlayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HumanPlayer"",
    ""maps"": [
        {
            ""name"": ""Human"",
            ""id"": ""ace32dd9-cbd5-448b-9c71-38ae2d0310dc"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ff335355-2f77-4efb-90e7-bcf2443942a5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Build"",
                    ""type"": ""Value"",
                    ""id"": ""3105389a-b203-4f6b-ad73-1decb5623cb5"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f0675385-42df-446d-b623-ded9dc18bb4a"",
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
                    ""id"": ""534e806d-7be8-48d4-be98-881c22d39864"",
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
                    ""id"": ""740f4b0d-de93-4b12-bef5-1a92d939203e"",
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
                    ""id"": ""365b2580-cfa2-4afb-a278-39f66ea48128"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Human
        m_Human = asset.FindActionMap("Human", throwIfNotFound: true);
        m_Human_Movement = m_Human.FindAction("Movement", throwIfNotFound: true);
        m_Human_Build = m_Human.FindAction("Build", throwIfNotFound: true);
    }

    ~@HumanPlayer()
    {
        UnityEngine.Debug.Assert(!m_Human.enabled, "This will cause a leak and performance issues, HumanPlayer.Human.Disable() has not been called.");
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

    // Human
    private readonly InputActionMap m_Human;
    private List<IHumanActions> m_HumanActionsCallbackInterfaces = new List<IHumanActions>();
    private readonly InputAction m_Human_Movement;
    private readonly InputAction m_Human_Build;
    public struct HumanActions
    {
        private @HumanPlayer m_Wrapper;
        public HumanActions(@HumanPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Human_Movement;
        public InputAction @Build => m_Wrapper.m_Human_Build;
        public InputActionMap Get() { return m_Wrapper.m_Human; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HumanActions set) { return set.Get(); }
        public void AddCallbacks(IHumanActions instance)
        {
            if (instance == null || m_Wrapper.m_HumanActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_HumanActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Build.started += instance.OnBuild;
            @Build.performed += instance.OnBuild;
            @Build.canceled += instance.OnBuild;
        }

        private void UnregisterCallbacks(IHumanActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Build.started -= instance.OnBuild;
            @Build.performed -= instance.OnBuild;
            @Build.canceled -= instance.OnBuild;
        }

        public void RemoveCallbacks(IHumanActions instance)
        {
            if (m_Wrapper.m_HumanActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IHumanActions instance)
        {
            foreach (var item in m_Wrapper.m_HumanActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_HumanActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public HumanActions @Human => new HumanActions(this);
    public interface IHumanActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnBuild(InputAction.CallbackContext context);
    }
}
