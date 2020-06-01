using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ARoomInterior.Models.DB
{
    public class ProjectElementObj
    {
        [Key]
        public int ProjectElementId { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public float CoordinateZ { get; set; }

        public int ElementObjElementId { get; set; }
        public ElementObj ElementObj { get; set; }
    }
}