using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class Tile : MonoBehaviour
    {
        public Tile[] directions;
        public Light light;

        public int[] location;

        public void ChangeColor(Color color) => light.color = color;

        public void Initit(int x, int y)
        {
            this.name = (x + "," + y);
            directions = new Tile[4];
            location = new int[2];
            location[0] = x;
            location[1] = y;
        }

        public void HighlightDirections()
        {
            foreach (var item in directions)
            {
                item?.Highlight();
            }
        }

        public void Highlight()
        {
            light.enabled = !light.enabled;
        }




    }
}