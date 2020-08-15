using System.ComponentModel.DataAnnotations;
using UnityEngine;

namespace ARoomInterior.Models.DB
{
    public class SpawnModel
    {
        [Key]
        public int SpawnModelId { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public SpawnModel() { }
        public SpawnModel(int spawnModelId, Vector3 position, Quaternion rotation)
        {
            this.SpawnModelId = spawnModelId;
            this.Position = position;
            this.Rotation = rotation;
        }

        public int ElementObjElementId { get; set; }
        public ElementObj ElementObj { get; set; }
    }
}