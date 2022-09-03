using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class GridManager : MonoBehaviour
    {
        public List<Tile> tiles;

        [SerializeField]
        private TextAsset _levelBuild;
        [SerializeField]
        private Tile _tilePrefab;
        [SerializeField]
        private float _distance;
        private int[] _map;

        private void Start()
        {
            GenerateGrid();
            BuildConnections();
        }

        private void GenerateGrid()
        {
            var row = _levelBuild.text.Split(new string[] { "\n" }, StringSplitOptions.None);
            Array.Reverse(row, 0, row.Length);

            for (int x = 0; x < row.Length; x++)
            {
                var column = Array.ConvertAll(row[x].Split(new string[] { "," }, StringSplitOptions.None), int.Parse);
                for (int y = 0; y < column.Length; y++)
                {
                    if (column[y] != 1) continue;

                    var tile = (Instantiate(_tilePrefab, new Vector3(y * _distance, 0, x * _distance), Quaternion.identity));
                    tile.Initit(x, y);
                    tiles.Add(tile);
                }
            }
        }

        private void BuildConnections()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    var x1 = tiles[i].location[0];
                    var y1 = tiles[i].location[1];

                    var x2 = tiles[j].location[0];
                    var y2 = tiles[j].location[1];

                    if (x2 == x1 + 1 && y2 == y1) tiles[i].directions[0] = tiles[j];
                    else if (x2 == x1 && y2 == y1 + 1) tiles[i].directions[1] = tiles[j];
                    else if (x2 == x1 - 1 && y2 == y1) tiles[i].directions[2] = tiles[j];
                    else if (x2 == x1 && y2 == y1 - 1) tiles[i].directions[3] = tiles[j];
                }
            }
        }
    }
}
