using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CuteNewtTest.Tools
{

    [CustomPropertyDrawer(typeof(SerializeReferenceMenuAttribute))]
    public class SerializeReferenceMenuAttributeDrawer : PropertyDrawer
    {
        private List<Type> _cachedTypes;
        private GUIContent[] _cachedTypeLabels;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + property.Children().Sum(EditorGUI.GetPropertyHeight);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            List<Type> types = GetDerivedTypes(property);
            GUIContent[] typeLabels = GetTypeLabels(types);

            position.height = EditorGUIUtility.singleLineHeight;

            int currentTypeIndex = types.IndexOf(property.managedReferenceValue?.GetType());
            int typeIndex = EditorGUI.Popup(position, new GUIContent(property.displayName), currentTypeIndex, typeLabels);

            if (typeIndex >= 0 && typeIndex != currentTypeIndex)
            {
                property.managedReferenceValue = Activator.CreateInstance(types[typeIndex]);
            }

            position.y += position.height;
            position.x += 5;

            foreach (SerializedProperty child in property.Children())
            {
                position.height = EditorGUI.GetPropertyHeight(child);
                EditorGUI.PropertyField(position, child, true);
                position.y += position.height;
            }
        }

        private GUIContent[] GetTypeLabels(List<Type> types)
        {
            if (_cachedTypeLabels != null)
                return _cachedTypeLabels;

            _cachedTypeLabels = types
           .Select(type => type.Name!)
           .Select(type => new GUIContent(type))
           .ToArray();

            return _cachedTypeLabels;
        }

        List<Type> GetDerivedTypes(SerializedProperty property)
        {
            if (_cachedTypes != null)
                return _cachedTypes;

            Type type = GetReferenceType(property);

            _cachedTypes = TypeCache.GetTypesDerivedFrom(type)
                .Where(type => !type.IsAbstract)
                .Where(type => type.GetConstructor(Array.Empty<Type>()) != null)
                .ToList();

            return _cachedTypes;
        }

        public static Type GetReferenceType(SerializedProperty property)
        {
            string typeName = GetTypeName(property);
            return Type.GetType(typeName);
        }

        public static string GetTypeName(SerializedProperty property)
        {
            string typeName = property.managedReferenceFieldTypename;

            if (string.IsNullOrEmpty(typeName))
                return string.Empty;

            string[] typeSplitString = typeName.Split(char.Parse(" "));
            string typeClassName = typeSplitString[1];
            string typeAssemblyName = typeSplitString[0];
            return $"{typeClassName}, {typeAssemblyName}";
        }
    }

}