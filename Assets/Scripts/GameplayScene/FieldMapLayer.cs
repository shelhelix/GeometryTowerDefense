using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameplayScene {
    public class FieldMapLayer : MonoBehaviour {
        public Grid Grid;
        
        List<Transform> _cells = new();
        
        public void Start() {
            _cells = GetComponentsInChildren<Transform>().ToList();
        }

        public T FindTile<T>() where T : Component {
            return _cells.Find(x => x.GetComponent<T>()).GetComponent<T>();
        }
    }
}