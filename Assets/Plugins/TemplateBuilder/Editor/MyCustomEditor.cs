using System.IO;
using System.Linq;
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
            CreateSCAFromTemplate();
        };
        root.Add(button);
    }

    private const string templatePath
#if DEBUG_COM_LONPEACH_TEMPLATE
        = "Assets/Plugins/TemplateBuilder/Editor/Template/TemplateScript.cs"; // 템플릿 스크립트
#else
        = "Packages/com.lonpeach.template-builder/Editor/Template/TemplateScript.cs"; // 템플릿 스크립트
#endif

    private const string outputPath = "Assets/Scripts/"; // 새로운 스크립트가 생성될 경로

    private void CreateScriptFromTemplate()
    {
        if (!File.Exists(templatePath))
        {
            Debug.LogError("Template file not found!");
            return;
        }

        string templateContent = File.ReadAllText(templatePath);

        // todo: 사용자가 원하는 스크립트 명으로 변경
        var className = "aaa" + Random.Range(0, 100);

        // 템플릿 내용에서 클래스명 변경
        templateContent = templateContent.Replace("TemplateScript", className);

        // 새로운 스크립트 파일 생성
        string newScriptPath = outputPath + className + ".cs";

        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        if (File.Exists(newScriptPath))
        {
            Debug.LogError("Script with the same name already exists!");
            return;
        }

        File.WriteAllText(newScriptPath, templateContent);
        AssetDatabase.Refresh();

        Debug.Log("Script created: " + newScriptPath);
    }

    private void CreateSCAFromTemplate()
    {
        Debug.Log("CreateSCAFromTemplate");

        // todo: 폴더에 있는 모든 파일 변경.
        const string folderPath
#if DEBUG_COM_LONPEACH_TEMPLATE
        = "Assets/Plugins/TemplateBuilder/Editor/Template/SCA/";
#else
        = "Packages/com.lonpeach.template-builder/Editor/Template/SCA/";
#endif

        const string outputPath = "Assets/Scripts/";

        if (!Directory.Exists(folderPath))
        {
            return;
        }

        string[] files = Directory.GetFiles(folderPath, "*.template", SearchOption.AllDirectories)
                            .Where(file => !file.EndsWith(".meta"))
                            .ToArray();

        // todo: 사용자가 원하는 스크립트 명으로 변경
        var inputName = "aaa" + Random.Range(0, 100);

        foreach (string file in files)
        {
            var relativePath = file.Replace(folderPath, "");
            Debug.Log("file: " + relativePath);

            // string templateContent = File.ReadAllText(file);
            // string fileName = Path.GetFileNameWithoutExtension(file).Replace("Template", inputName);
            // templateContent = templateContent.Replace("@Template@", inputName);

            // // 새로운 스크립트 파일 생성
            // string newScriptPath = outputPath + fileName + ".cs";

            // if (!Directory.Exists(outputPath))
            // {
            //     Directory.CreateDirectory(outputPath);
            // }

            // if (File.Exists(newScriptPath))
            // {
            //     Debug.LogError("Script with the same name already exists!");
            //     return;
            // }

            // File.WriteAllText(newScriptPath, templateContent);
            // Debug.Log("Script created: " + newScriptPath);
        }

        AssetDatabase.Refresh();
    }
}