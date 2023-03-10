//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem/Input.inputactions
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

public partial class @Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""ResetData"",
            ""id"": ""55d31508-c240-4b74-b66e-33802c6cd2f9"",
            ""actions"": [
                {
                    ""name"": ""reset"",
                    ""type"": ""Button"",
                    ""id"": ""f8d968a8-ee73-4299-8ec1-d48c0b94fd39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a82082b3-b718-438d-8131-626bc62fe8e6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // ResetData
        m_ResetData = asset.FindActionMap("ResetData", throwIfNotFound: true);
        m_ResetData_reset = m_ResetData.FindAction("reset", throwIfNotFound: true);
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

    // ResetData
    private readonly InputActionMap m_ResetData;
    private IResetDataActions m_ResetDataActionsCallbackInterface;
    private readonly InputAction m_ResetData_reset;
    public struct ResetDataActions
    {
        private @Input m_Wrapper;
        public ResetDataActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @reset => m_Wrapper.m_ResetData_reset;
        public InputActionMap Get() { return m_Wrapper.m_ResetData; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ResetDataActions set) { return set.Get(); }
        public void SetCallbacks(IResetDataActions instance)
        {
            if (m_Wrapper.m_ResetDataActionsCallbackInterface != null)
            {
                @reset.started -= m_Wrapper.m_ResetDataActionsCallbackInterface.OnReset;
                @reset.performed -= m_Wrapper.m_ResetDataActionsCallbackInterface.OnReset;
                @reset.canceled -= m_Wrapper.m_ResetDataActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_ResetDataActionsCallbackInterface = instance;
            if (instance != null)
            {
                @reset.started += instance.OnReset;
                @reset.performed += instance.OnReset;
                @reset.canceled += instance.OnReset;
            }
        }
    }
    public ResetDataActions @ResetData => new ResetDataActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IResetDataActions
    {
        void OnReset(InputAction.CallbackContext context);
    }
}
