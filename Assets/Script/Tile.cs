using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class Tile : MonoBehaviour
    {
        public Light light;

        public void ChangeColor(Color color) => light.color = color;
    }
}