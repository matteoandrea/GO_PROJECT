using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Assets.Script
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _levelBuild;

        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private float _distance;

        private int[] _map;
        [SerializeField]
        private List<Tile> _tiles;


        private void Start()
        {
            GenerateGrid();
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
                    tile.name = x + "," + y;
                    _tiles.Add(tile);
                }
            }
        }
    }
}
