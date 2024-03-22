using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class ParentSwitcher : MonoBehaviour
{
   [SerializeField] private Transform _smallCube;
   [SerializeField] private Transform _bigCube;

   [Button]
   private void SetScale()
   {
       Transform previousParent = _smallCube.parent;

       // _smallCube.parent = _bigCube.parent;
       //  _smallCube.localScale = _bigCube.localScale;
       //  _smallCube.parent = previousParent;

       _smallCube.parent = _bigCube;
       _smallCube.localScale = Vector3.one;
       _smallCube.parent = previousParent;
   }
}
