using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MyCustomEditor : EditorWindow
{
    [MenuItem("Window/UI Toolkit/MyCustomEditor")]
    public static void ShowExample()
    {
        // 이 메소드는 사용자가 Editor에서 메뉴 항목을 선택할 때 호출됩니다.
        MyCustomEditor wnd = GetWindow<MyCustomEditor>();
        wnd.titleContent = new GUIContent("MyCustomEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        Button button = new Button();
        button.name = "button3";
        button.text = "This is button3.";
        button.clicked += () =>
        {
            Debug.Log("butotn Clicked");
        };
        root.Add(button);
    }

    private const string templatePath = "Assets/Packages/com.lonpeach.template-builder/Editor/Template/TemplateScript.cs"; // 템플릿 스크립트의 경로    
    private const string outputPath = "Assets/Scripts/"; // 새로운 스크립트가 생성될 경로
}