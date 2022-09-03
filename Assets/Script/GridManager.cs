using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class GridManager : MonoBehaviour
    {
        public List<Path> paths;

        [SerializeField]
        private TextAsset _levelBuild;
        [SerializeField]
        private Path _tilePrefab;
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
                    switch (column[y])
                    {
                        case 0:
                            continue;
                        default:
                            break;
                    }

                    var pos = new Vector3(transform.position.x + _distance * -x, transform.position.y, transform.position.z + _distance * y);
                    var tile = Instantiate(_tilePrefab, pos, Quaternion.identity);
                    tile.transform.parent = this.transform;
                    tile.Initit(x, y);
                    paths.Add(tile);
                }
            }
        }

        private void BuildConnections()
        {
            for (int i = 0; i < paths.Count; i++)
            {
                for (int j = 0; j < paths.Count; j++)
                {
                    var x1 = paths[i].location[0];
                    var y1 = paths[i].location[1];

                    var x2 = paths[j].location[0];
                    var y2 = paths[j].location[1];

                    if (x2 == x1 + 1 && y2 == y1) paths[i].directions[0] = paths[j];
                    else if (x2 == x1 && y2 == y1 + 1) paths[i].directions[1] = paths[j];
                    else if (x2 == x1 - 1 && y2 == y1) paths[i].directions[2] = paths[j];
                    else if (x2 == x1 && y2 == y1 - 1) paths[i].directions[3] = paths[j];
                }
            }
        }
    }
}
