using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDGestionDeStock.Data.Entidades
{
    [Index(nameof(CodigoProducto), Name = "CodigoProducto_UQ", IsUnique = true)]
    public class Producto : EntityBase
    {
        [Required]
        [MaxLength(8, ErrorMessage = "El codigo no debe superar los {1} caracteres")]
        public int CodigoProducto { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "El Nombre no debe superar los {1} caracteres")]
        public string NombreProducto { get; set; }  

        public string DescripcionProducto { get; set; } 

        public int Stock { get; set; } 
        //

    }
}
