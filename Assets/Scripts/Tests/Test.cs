using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    TestInputActions inputActions;

    private void Awake()
    {
        inputActions = new TestInputActions();
    }

    private void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;
        inputActions.Test.Test2.performed += Test2;
        inputActions.Test.Test3.performed += Test3;
        inputActions.Test.Test4.performed += Test4;
        inputActions.Test.Test5.performed += Test5;
        inputActions.Test.Test6.performed += Test6;
        inputActions.Test.Test7.performed += Test7;
        inputActions.Test.Test8.performed += Test8;
        inputActions.Test.Test9.performed += Test9;
        inputActions.Test.Test10.performed += Test10;
        inputActions.Test.TestClick.performed += TestClick;
    }

    private void OnDisable()
    {
        inputActions.Test.TestClick.performed -= TestClick;
        inputActions.Test.Test10.performed -= Test10;
        inputActions.Test.Test9.performed -= Test9;
        inputActions.Test.Test8.performed -= Test8;
        inputActions.Test.Test7.performed -= Test7;
        inputActions.Test.Test6.performed -= Test6;
        inputActions.Test.Test5.performed -= Test5;
        inputActions.Test.Test4.performed -= Test4;
        inputActions.Test.Test3.performed -= Test3;
        inputActions.Test.Test2.performed -= Test2;
        inputActions.Test.Test1.performed -= Test1;
        inputActions.Test.Disable();
    }

    private void Test1(InputAction.CallbackContext context) { }
    private void Test2(InputAction.CallbackContext context) { }
    private void Test3(InputAction.CallbackContext context) { }
    private void Test4(InputAction.CallbackContext context) { }
    private void Test5(InputAction.CallbackContext context) { }
    private void Test6(InputAction.CallbackContext context) { }
    private void Test7(InputAction.CallbackContext context) { }
    private void Test8(InputAction.CallbackContext context) { }
    private void Test9(InputAction.CallbackContext context) { }
    private void Test10(InputAction.CallbackContext context) { }
    private void TestClick(InputAction.CallbackContext context) { }
}
