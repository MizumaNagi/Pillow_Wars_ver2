// GENERATED AUTOMATICALLY FROM 'Assets/WorkFolder/Mizuma/Data/Input/InputSystem/KeyboardInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KeyboardInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KeyboardInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""KeyboardInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0083c148-ef5b-455d-a090-410e7d88abcd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""90420890-4079-4953-98a5-1ca3d7a3e85c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ViewMove"",
                    ""type"": ""Value"",
                    ""id"": ""7ff6ecc2-6a8d-40eb-96c2-8c2596c27097"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToADS"",
                    ""type"": ""Button"",
                    ""id"": ""22e2ed7a-df5b-47cf-95a2-fda620034e2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoOption"",
                    ""type"": ""Button"",
                    ""id"": ""c1304900-33ac-4a96-8e07-7ca18f4e159f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""a3ae174f-bc27-4e52-a535-d8d9649382fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2ff9840c-4cf6-4a4f-ab32-fb3b9a527214"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PillowWar"",
                    ""type"": ""Button"",
                    ""id"": ""87e150c7-bec2-4e00-a889-c5d03c90e598"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Squat"",
                    ""type"": ""Button"",
                    ""id"": ""ea80d4b2-f48a-4eb5-aded-cf6080705c5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""7823680d-1e29-4f5f-9d43-4920a1d2c645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""2f4f6da2-f422-495c-97ac-099094b72246"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9867c908-bd82-48e9-a4b9-dd7cd804a690"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7753e147-b634-4c3f-b2c0-c264ab27780f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bcffa50a-947c-40ff-b9b8-f29486c2517f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""64f69f1d-8fa9-48de-99b6-47942ef632de"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""3d92050b-9aa1-4110-8448-1abd9febeb75"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fa56c810-c0f4-4706-9479-a478e37a36bd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6766d340-984c-44e9-840a-65db39948aef"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a173f1b4-b891-4be9-8418-b981c11d9c37"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5ed43aa0-c480-4f25-91ff-a679820a70ee"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c61a503d-e0de-45ee-918e-5300e9b7748d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""ViewMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4eb8a75f-2a73-48ba-8b52-15a71ba0ae98"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard_Mouse"",
                    ""action"": ""ToADS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ef0998e-ff2f-45c8-9218-802fa91b609f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoOption"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60e0c94f-0e80-4fbf-ae67-b1e30352e0d9"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d2a790d-b424-4abb-b3dd-35ffd6594de7"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ccc902d-79d2-4b90-b0da-61b648fef590"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PillowWar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e61e0752-309c-41cb-8b64-50da43c47c70"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Squat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b763b3a-a269-4d95-822b-512a802d5432"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard_Mouse"",
            ""bindingGroup"": ""Keyboard_Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_ViewMove = m_Player.FindAction("ViewMove", throwIfNotFound: true);
        m_Player_ToADS = m_Player.FindAction("ToADS", throwIfNotFound: true);
        m_Player_GoOption = m_Player.FindAction("GoOption", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_PillowWar = m_Player.FindAction("PillowWar", throwIfNotFound: true);
        m_Player_Squat = m_Player.FindAction("Squat", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_ViewMove;
    private readonly InputAction m_Player_ToADS;
    private readonly InputAction m_Player_GoOption;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_PillowWar;
    private readonly InputAction m_Player_Squat;
    private readonly InputAction m_Player_Run;
    public struct PlayerActions
    {
        private @KeyboardInput m_Wrapper;
        public PlayerActions(@KeyboardInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @ViewMove => m_Wrapper.m_Player_ViewMove;
        public InputAction @ToADS => m_Wrapper.m_Player_ToADS;
        public InputAction @GoOption => m_Wrapper.m_Player_GoOption;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @PillowWar => m_Wrapper.m_Player_PillowWar;
        public InputAction @Squat => m_Wrapper.m_Player_Squat;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @ViewMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewMove;
                @ViewMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewMove;
                @ViewMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewMove;
                @ToADS.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToADS;
                @ToADS.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToADS;
                @ToADS.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToADS;
                @GoOption.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoOption;
                @GoOption.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoOption;
                @GoOption.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoOption;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @PillowWar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPillowWar;
                @PillowWar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPillowWar;
                @PillowWar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPillowWar;
                @Squat.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquat;
                @Squat.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquat;
                @Squat.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquat;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ViewMove.started += instance.OnViewMove;
                @ViewMove.performed += instance.OnViewMove;
                @ViewMove.canceled += instance.OnViewMove;
                @ToADS.started += instance.OnToADS;
                @ToADS.performed += instance.OnToADS;
                @ToADS.canceled += instance.OnToADS;
                @GoOption.started += instance.OnGoOption;
                @GoOption.performed += instance.OnGoOption;
                @GoOption.canceled += instance.OnGoOption;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @PillowWar.started += instance.OnPillowWar;
                @PillowWar.performed += instance.OnPillowWar;
                @PillowWar.canceled += instance.OnPillowWar;
                @Squat.started += instance.OnSquat;
                @Squat.performed += instance.OnSquat;
                @Squat.canceled += instance.OnSquat;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_Keyboard_MouseSchemeIndex = -1;
    public InputControlScheme Keyboard_MouseScheme
    {
        get
        {
            if (m_Keyboard_MouseSchemeIndex == -1) m_Keyboard_MouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard_Mouse");
            return asset.controlSchemes[m_Keyboard_MouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnViewMove(InputAction.CallbackContext context);
        void OnToADS(InputAction.CallbackContext context);
        void OnGoOption(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPillowWar(InputAction.CallbackContext context);
        void OnSquat(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
    }
}
