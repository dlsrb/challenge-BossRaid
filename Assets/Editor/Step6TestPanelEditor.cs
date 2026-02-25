#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using BossRaid.Gameplay.Promotion;

[CustomEditor(typeof(Step6TestPanel))]
public sealed class Step6TestPanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        var t = (Step6TestPanel)target;

        if (GUILayout.Button("Step6: Request Candidates"))
            t.RequestCandidates();

        if (GUILayout.Button("Step6: Select Promotion"))
            t.SelectPromotion();

        if (GUILayout.Button("Step6: Lock Promotion"))
            t.LockPromotion();
    }
}
#endif