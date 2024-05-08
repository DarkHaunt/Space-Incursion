using Game.Code.Game.Level.BoxArea;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BoxPointsArea))]
    public class BoxPointsAreaEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected)]
        public static void RenderCustomGizmo(BoxPointsArea area, GizmoType gizmoType)
        {
            var ogColor = Gizmos.color;
            
            var leftTopPos = area.LeftBottomPoint.position;
            var rightBottomPos = area.RightBottomPoint.position;

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube((leftTopPos + rightBottomPos) * 0.5f, rightBottomPos - leftTopPos);
            Gizmos.color = ogColor;
        }
    }
}