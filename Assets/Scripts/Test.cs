using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement icon = root.Q("TestIcon");

        icon.AddManipulator(new IconDragger(root));
    }


}
